# Curve Data System

Use `AnimationCurve` assets to define level-based values, experience tables, stat growth, rewards, and other balance data. Designers can adjust the curve in the inspector without code changes.

## Structure

```text
CurveDataAsset (ScriptableObject)
  Curve       : AnimationCurve   - Normalized 0..1 -> 0..1 curve
  MinInput    : float            - Input range start
  MaxInput    : float            - Input range end
  MinOutput   : float            - Output range start
  MaxOutput   : float            - Output range end

CurveDataTable (ScriptableObject)
  List<Entry { Key, CurveDataAsset }>
```

## Quick Start

### 1. Create a CurveDataAsset

**Create -> AchUtils/Curve/Curve Data Asset**

```text
Curve    : Edit in the curve editor
MinInput : 1      MaxInput : 100
MinOutput: 100    MaxOutput: 50000
```

### 2. Use It in Code

```csharp
using AchUtils.Curve;

[SerializeField] CurveDataAsset hpCurve;
[SerializeField] CurveDataAsset expCurve;

float hp = hpCurve.Evaluate(player.Level);
float exp = expCurve.Evaluate(10f);
```

### 3. Group Curves With CurveDataTable

**Create -> AchUtils/Curve/Curve Data Table**

```csharp
[SerializeField] CurveDataTable statTable;

float hp     = statTable.Evaluate("HP",     level);
float attack = statTable.Evaluate("Attack", level);
float def    = statTable.Evaluate("Defense", level);
```

## API

### CurveDataAsset

```csharp
float Evaluate(float input)
float EvaluateNormalized(float t)
```

`Evaluate` maps `MinInput..MaxInput` into normalized curve space, then maps the result into `MinOutput..MaxOutput`.

### CurveDataTable

```csharp
float Evaluate(string key, float input)
bool  TryGetCurve(string key, out CurveDataAsset curve)
```

## Curve Shapes

```text
Linear growth       HP scaling
                    -> AnimationCurve.Linear(0,0,1,1)

Exponential growth  EXP requirements
                    -> Curve rises sharply near the end

Logarithmic growth  Early-fast, late-slow attack growth
                    -> Curve rises sharply near the start

Step curve          Grade-based rewards
                    -> Repeating horizontal and vertical segments
```

## Compared to Hardcoding

```csharp
// Before: hardcoded, requires a code change
float hp = level * 100 + level * level * 5;

// After: editable in the inspector
float hp = hpCurve.Evaluate(level);
```

::: tip Editor preview
You can add an editor script that previews evaluated values while editing `CurveDataAsset` in the inspector.
:::
