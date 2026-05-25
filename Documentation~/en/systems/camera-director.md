# Camera Director

Compose **camera presentation chains** around gameplay events. It is intentionally simpler than Cinemachine and focused on event-driven camera moves.

## Structure

```text
CameraDirector (MonoBehaviour runner for coroutines)
  - Holds the Camera reference and runs action sequences

CameraAction (abstract, [Serializable])
  ├── ZoomCameraAction    - Changes field of view
  ├── ShakeCameraAction   - Camera shake
  ├── MoveCameraAction    - Position movement
  └── WaitCameraAction    - Wait
```

## Quick Start

### 1. Add CameraDirector

Add `CameraDirector` to a scene object. If the `Camera` field is empty, it uses `Camera.main`.

### 2. Play One Action

```csharp
using AchUtils.Camera;

[SerializeField] CameraDirector cameraDirector;

cameraDirector.Play(new ShakeCameraAction
{
    Duration  = 0.5f,
    Intensity = 0.3f,
    Frequency = 30f
});
```

### 3. Play a Sequence

```csharp
[SerializeField] CameraDirector cameraDirector;

cameraDirector.PlaySequence(new CameraAction[]
{
    new MoveCameraAction
    {
        FollowTarget = boss.transform,
        Duration     = 1.5f,
        Ease         = AnimationCurve.EaseInOut(0,0,1,1)
    },
    new ZoomCameraAction  { TargetFOV = 35f,   Duration = 0.8f },
    new WaitCameraAction  { Duration  = 2.0f },
    new ShakeCameraAction { Duration  = 0.5f,  Intensity = 0.4f },
    new ZoomCameraAction  { TargetFOV = 60f,   Duration = 1.0f },
});
```

## API

### CameraDirector

```csharp
CameraDirector cameraDirector

Camera Camera

void Play(CameraAction action)
void PlaySequence(IEnumerable<CameraAction> actions)
void Stop()

bool IsPlaying

event Action OnSequenceCompleted
```

### ZoomCameraAction

```csharp
float          TargetFOV
float          Duration
AnimationCurve Ease
```

### ShakeCameraAction

```csharp
float          Duration
float          Intensity
float          Frequency
AnimationCurve AttenuationCurve
```

### MoveCameraAction

```csharp
Vector3        Destination
Transform      FollowTarget
float          Duration
AnimationCurve Ease
```

### WaitCameraAction

```csharp
float Duration
```

## Completion Callback

```csharp
cameraDirector.OnSequenceCompleted += () =>
{
    player.EnableControl();
    UI.ShowBossHealthBar();
};

cameraDirector.PlaySequence(bossIntroActions);
```

## Custom Actions

```csharp
[Serializable]
public class RotateCameraAction : CameraAction
{
    public Vector3 TargetAngle;
    public float   Duration;

    public override IEnumerator Execute(CameraDirector director)
    {
        var cam = director.Camera.transform;
        Quaternion startRot = cam.rotation;
        Quaternion endRot   = Quaternion.Euler(TargetAngle);
        float elapsed = 0f;

        while (elapsed < Duration)
        {
            elapsed += Time.deltaTime;
            cam.rotation = Quaternion.Lerp(startRot, endRot, elapsed / Duration);
            yield return null;
        }

        cam.rotation = endRot;
    }
}
```

## Boss Intro Example

```text
Boss trigger
  ↓
MoveCameraAction   Move toward boss for 1.5s
  ↓
ZoomCameraAction   FOV 60 -> 35 for 0.8s
  ↓
WaitCameraAction   Wait 2s
  ↓
ShakeCameraAction  Shake for 0.5s
  ↓
ZoomCameraAction   FOV 35 -> 60 for 1s
  ↓
OnSequenceCompleted -> Show UI
```

::: tip With SequenceRunner
Use `CallbackStep` in `SequenceRunner` to start a camera sequence at a precise point in a presentation flow.
:::
