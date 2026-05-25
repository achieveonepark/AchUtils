# Buff / Debuff System

A general-purpose buff pipeline for duration, stacks, ticks, and priority. Poison, stun, attack-speed boosts, and custom gameplay effects can be implemented by inheriting from `BuffEffect`.

## Structure

```text
BuffSystem (MonoBehaviour singleton)
  - Updates timers, ticks, and expiration for active buffs

BuffDefinition (ScriptableObject)
  - Name, icon, duration, tick, and stack settings
  [SerializeReference] List<BuffEffect>

BuffInstance (runtime)
  - Runtime state for a buff applied to a target

BuffEffect (abstract, [Serializable])
  ├── DamageOverTimeEffect   - Damage on each tick
  ├── StunEffect             - Disable movement or input
  └── StatModifyEffect       - Add/remove stat modifiers automatically
```

## Quick Start

### 1. Create a BuffDefinition

**Create -> AchUtils/Buff/Buff Definition**

```text
BuffId          : "Poison"
DisplayName     : "Poison"
Duration        : 8
TickInterval    : 1
MaxStacks       : 3
RefreshOnReapply: true

Effects:
  [0] DamageOverTimeEffect
        DamagePerTick: 15
```

### 2. Add BuffSystem to the Scene

Place a `BuffSystem` component in the scene.

### 3. Apply and Remove Buffs

```csharp
using AchieveOnePark.AchUtils.Buff;

BuffSystem.Instance.Apply(poisonDef, target);

BuffSystem.Instance.Remove("Poison", target);

bool hasPoisoned = BuffSystem.Instance.Has("Poison", target);

BuffSystem.Instance.RemoveAll(target);
```

## API

### BuffSystem

```csharp
BuffInstance Apply(BuffDefinition definition, GameObject target)
bool         Remove(string buffId, GameObject target)
void         RemoveAll(GameObject target)
bool         Has(string buffId, GameObject target)
List<BuffInstance> GetBuffs(GameObject target)

event Action<BuffInstance> OnBuffApplied
event Action<BuffInstance> OnBuffRemoved
```

### BuffDefinition Inspector

| Field | Description |
|-------|-------------|
| `BuffId` | Unique identifier |
| `Duration` | Duration in seconds. `-1` means permanent |
| `TickInterval` | Tick interval in seconds. `0` disables ticks |
| `MaxStacks` | Maximum stack count |
| `RefreshDurationOnReapply` | Whether reapply refreshes the duration |

### BuffInstance

```csharp
BuffDefinition  Definition
GameObject      Target
int             Stacks
float           RemainingDuration
bool            IsPermanent
bool            IsExpired
```

## Built-In Effects

### DamageOverTimeEffect

```csharp
float DamagePerTick   // Damage per tick, multiplied by stacks
```

The target must implement `IDamageable`.

```csharp
public class Player : MonoBehaviour, IDamageable
{
    public void TakeDamage(float amount)
    {
        hp -= amount;
    }
}
```

### StunEffect

The target must implement `IStunnable`.

```csharp
public class PlayerController : MonoBehaviour, IStunnable
{
    private int _stunCount;

    public void ApplyStun()  => _stunCount++;
    public void RemoveStun() => _stunCount--;

    void Update()
    {
        if (_stunCount > 0) return;
        // Movement...
    }
}
```

### StatModifyEffect

Adds and removes a stat modifier through `StatSheetComponent`.

```text
StatKey      : "Attack"
ModifierType : PercentAdd
Value        : 0.3
```

## Custom Effects

```csharp
[Serializable]
public class FreezeEffect : BuffEffect
{
    public float SlowRatio = 0.5f;

    public override void OnApply(BuffInstance buff, GameObject target)
    {
        var rb = target.GetComponent<Rigidbody>();
        if (rb) rb.velocity *= (1f - SlowRatio);
    }

    public override void OnTick(BuffInstance buff, GameObject target)
    {
        // Keep suppressing speed on each tick.
    }
}
```

## Examples

```text
Poison
  Duration: 8s, TickInterval: 1s, MaxStacks: 3
  DamageOverTimeEffect: 15 x stacks

Freeze
  Duration: 2s, MaxStacks: 1
  StunEffect

Berserk
  Duration: 10s
  StatModifyEffect: Attack PercentAdd +0.5

Curse
  Duration: -1 (permanent), MaxStacks: 1
  StatModifyEffect: Defense Flat -20
```

::: tip Stat integration
`StatModifyEffect` integrates with [Stat Modifier](/en/systems/stat-modifier). The target needs `StatSheetComponent`.
:::
