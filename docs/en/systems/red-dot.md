# Red Dot System

Manage UI notification badges with a **path-based hierarchy**. If a child path has a red dot, its parent path automatically evaluates to true.

## Structure

```text
RedDotSystem  (pure C# singleton)
  ├── "Shop"
  │     ├── "Shop/Free"       Provider: () => HasFreeItem()
  │     └── "Shop/Discount"   Provider: () => HasDiscount()
  └── "Mail"
        └── "Mail/Reward"     Provider: () => HasReward()

RedDotView  (MonoBehaviour) - Subscribes to a path and toggles a GameObject.
```

## Quick Start

### 1. Register Providers

```csharp
using AchieveOnePark.AchUtils.RedDot;

void Start()
{
    RedDotSystem.Instance.Register("Shop/Free",     () => shopService.HasFreeItem());
    RedDotSystem.Instance.Register("Shop/Discount", () => shopService.HasDiscount());
    RedDotSystem.Instance.Register("Mail/Reward",   () => mailService.HasReward());
}
```

### 2. Attach RedDotView to UI

Add `RedDotView` to a button and configure `Path` and `Dot Object`.

```text
Button "ShopButton"
  └── RedDotView
        Path       : "Shop"
        Dot Object : RedDot (Image)
```

### 3. Notify Changes

```csharp
await shopService.ClaimFreeItem();
RedDotSystem.Instance.Notify("Shop/Free");

// Or refresh all providers.
RedDotSystem.Instance.NotifyAll();
```

## API

### RedDotSystem

```csharp
void Register(string path, Func<bool> provider)
void Unregister(string path)

bool Evaluate(string path)

void Notify(string path)
void NotifyAll()

void Subscribe(string path, Action<bool> callback)
void Unsubscribe(string path, Action<bool> callback)
```

### RedDotView Inspector

| Field | Description |
|-------|-------------|
| `Path` | Red-dot path to observe |
| `Dot Object` | GameObject toggled on or off |

## Path Rules

```text
"Mail"              - Entire mail menu
"Mail/Reward"       - Mail > Reward
"Shop"              - Entire shop menu
"Shop/Free"         - Shop > Free item
"Shop/Discount"     - Shop > Discount
```

Parent paths do not need explicit providers. They are created automatically when child paths are registered.

## Subscribe in Code

```csharp
void OnEnable()
{
    RedDotSystem.Instance.Subscribe("Shop", OnShopRedDotChanged);
}

void OnDisable()
{
    RedDotSystem.Instance.Unsubscribe("Shop", OnShopRedDotChanged);
}

void OnShopRedDotChanged(bool hasRedDot)
{
    shopIcon.color = hasRedDot ? Color.red : Color.white;
}
```

::: warning Performance
`NotifyAll()` calls every registered provider immediately. Use it when state changes, not every frame.
:::
