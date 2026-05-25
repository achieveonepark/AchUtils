# Condition Graph

Represent complex game conditions as **AND / OR / NOT node trees**. This keeps condition logic out of nested `if` blocks and lets designers configure gates in the inspector.

## Structure

```text
ConditionGraph (ScriptableObject)
  └── Root: ConditionNode  [SerializeReference]
              ├── AndConditionNode
              │     ├── CompareConditionNode  Level >= 10
              │     ├── CompareConditionNode  VIP >= 3
              │     └── CompareConditionNode  Tutorial == 1
              └── OrConditionNode
                    ├── CompareConditionNode  HasItem == 1
                    └── ...
```

## Quick Start

### 1. Create a ConditionGraph Asset

**Create -> AchUtils/Condition/Condition Graph**

Add nodes to `Root` in the inspector.

### 2. Evaluate Conditions

```csharp
using AchieveOnePark.AchUtils.Condition;

var ctx = new ConditionContext();
ctx.Set("Level", player.Level);
ctx.Set("VIP", player.VipGrade);
ctx.Set("Tutorial", tutorialDone ? 1f : 0f);

bool canOpen = shopConditionGraph.Evaluate(ctx);
shopPanel.SetActive(canOpen);
```

## Node Types

### CompareConditionNode

Compares numeric values.

| Field | Type | Description |
|-------|------|-------------|
| `Key` | string | Context key |
| `Op` | CompareOp | Comparison operator |
| `Value` | float | Comparison value |

```csharp
public enum CompareOp
{
    Equal, NotEqual,
    Greater, GreaterOrEqual,
    Less, LessOrEqual
}
```

### AndConditionNode

Returns true only when **all** children evaluate to true.

```text
AND
├ Level >= 10   true
├ VIP >= 3      true
└ Tutorial == 1 true
-> true
```

### OrConditionNode

Returns true when **any** child evaluates to true.

```text
OR
├ HasFreeItem == 1   true
└ HasDiscount == 1   false
-> true
```

### NotConditionNode

Inverts the child result.

```text
NOT
└ IsBanned == 1   false
-> true
```

## ConditionContext API

```csharp
var ctx = new ConditionContext();

ctx.Set("Level", 15f);
ctx.Get("Level");       // 15f
ctx.Has("Level");       // true
ctx.Remove("Level");
ctx.Clear();
```

::: tip Reuse contexts
Avoid allocating a new `ConditionContext` every frame. Keep one instance and update values with `Set`.
:::

## Use Cases

```csharp
shopGraph.Evaluate(ctx)        // Shop visibility
eventGraph.Evaluate(ctx)       // Event availability
achievementGraph.Evaluate(ctx) // Achievement completion
adGraph.Evaluate(ctx)          // Ad placement rules
```

## Custom Nodes

```csharp
[Serializable]
public class DateRangeConditionNode : ConditionNode
{
    public string StartDate;  // "2024-12-25"
    public string EndDate;

    public override bool Evaluate(ConditionContext context)
    {
        var now = DateTime.Now;
        return now >= DateTime.Parse(StartDate)
            && now <= DateTime.Parse(EndDate);
    }
}
```

Add `[Serializable]` to make the node available through `[SerializeReference]` fields in the inspector.
