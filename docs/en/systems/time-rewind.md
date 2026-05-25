# Time Rewind System

Record snapshots from every object that implements `IRewindable` and restore the world to a previous point in time.

## Structure

```text
TimeRewindSystem (MonoBehaviour singleton)
  - Records snapshots for registered IRewindable objects
  - Restores a target time with RewindTo(float secondsAgo)

IRewindable (interface)
  - CaptureState() : returns current state as object
  - RestoreState() : restores a captured state

RewindableObject (MonoBehaviour)
  - Default implementation for Transform position, rotation, and scale
```

## Quick Start

### 1. Add TimeRewindSystem

Add `TimeRewindSystem` to a scene object.

| Inspector field | Default | Description |
|-----------------|---------|-------------|
| `Max History Seconds` | 5 | Maximum recorded history in seconds |
| `Snapshots Per Second` | 10 | Snapshot frequency |

### 2. Add RewindableObject

Attach `RewindableObject` to objects you want to restore. Transform state is recorded automatically.

```csharp
gameObject.AddComponent<RewindableObject>();
```

### 3. Rewind

```csharp
using AchieveOnePark.AchUtils.TimeRewind;

TimeRewindSystem.Instance.StartRewind();

TimeRewindSystem.Instance.RewindTo(secondsAgo: 3f);

TimeRewindSystem.Instance.StopRewind();
```

### 4. Skill Example

```csharp
public class TimeRewindSkill : MonoBehaviour
{
    [SerializeField] float rewindSeconds = 3f;

    IEnumerator UseSkill()
    {
        var system = TimeRewindSystem.Instance;
        system.StartRewind();

        float t = 0f;
        while (t < rewindSeconds)
        {
            t += Time.deltaTime;
            system.RewindTo(rewindSeconds - t);
            yield return null;
        }

        system.StopRewind();
    }
}
```

## API

### TimeRewindSystem

```csharp
void Register(IRewindable target)
void Unregister(IRewindable target)

void StartRewind()
void StopRewind()

void RewindTo(float secondsAgo)
void StepRewind(float deltaSeconds)

bool  IsRewinding
float HistoryDuration

event Action OnRewindStart
event Action OnRewindStop
```

## Implement IRewindable

Use `IRewindable` directly when you need to capture custom state.

```csharp
public class EnemyAI : MonoBehaviour, IRewindable
{
    private float hp;
    private Vector3 position;
    private AIState state;

    private void OnEnable()  => TimeRewindSystem.Instance?.Register(this);
    private void OnDisable() => TimeRewindSystem.Instance?.Unregister(this);

    public object CaptureState()
    {
        return (hp, transform.position, state);
    }

    public void RestoreState(object snapshot)
    {
        (hp, position, state) = ((float, Vector3, AIState))snapshot;
        transform.position = position;
    }
}
```

## Resource Cost

```text
Snapshot count = MaxHistorySeconds * SnapshotsPerSecond * registered objects

Example: 5s * 10fps * 20 objects = 1000 snapshots
```

If you have many objects or a long history, reduce `SnapshotsPerSecond` or `MaxHistorySeconds`.

## Use Cases

| Use case | Description |
|----------|-------------|
| Puzzle undo | Restore blocks to a previous state |
| Time-rewind skill | Braid / Titanfall-style rewind effects |
| Debugging | Return to the state before a bug |
| Replay preview | Show the last few seconds before death |

::: warning Physics
Objects with Rigidbody need velocity and angular velocity captured too. Extend `RewindableObject` and override `CaptureState`.

```csharp
public override object CaptureState()
{
    var rb = GetComponent<Rigidbody>();
    return (base.CaptureState(), rb.velocity, rb.angularVelocity);
}
```
:::
