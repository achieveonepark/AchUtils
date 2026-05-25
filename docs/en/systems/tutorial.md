# Tutorial System

Build in-game tutorials as **ScriptableObject-based steps**. Designers can change the flow without hardcoding, and the runtime supports skipping, replaying, saving, and loading completion state.

## Structure

```text
TutorialSystem          - MonoBehaviour singleton. Saves and restores completed tutorials.
  └── TutorialRunner    - Runs tutorial steps as coroutines. Added automatically.

TutorialSequence        - ScriptableObject. Step list and skip settings.
  └── TutorialStep[]    - ScriptableObject base class for concrete tutorial steps.
```

## Built-In Steps

| Step | Description |
|------|-------------|
| `HighlightButtonStep` | Highlights a UI object and optionally waits for an action key |
| `WaitForActionStep` | Waits until a named player action is triggered |
| `DialogueStep` | Shows a message panel, then auto-advances or waits for click input |
| `DelayStep` | Waits for a fixed number of seconds |

## Quick Start

### 1. Add TutorialSystem to the Scene

```csharp
// Place a GameObject with TutorialSystem in the scene.
// TutorialRunner is added automatically.
```

### 2. Create a TutorialSequence Asset

**Project window -> Create -> AchUtils/Tutorial/Tutorial Sequence**

Configure the steps in the inspector:

```text
SequenceId : "FirstGacha"
CanSkip    : true
Steps:
  [0] HighlightButtonStep  - Highlight GachaButton, wait for "GachaClick"
  [1] DialogueStep         - "You obtained a character!"
  [2] HighlightButtonStep  - Highlight EquipButton
```

### 3. Start the Tutorial

```csharp
// Completed tutorials are skipped automatically.
TutorialSystem.Instance.StartTutorial(gachaSequence);

// Force replay, ignoring completion state.
TutorialSystem.Instance.StartTutorialForce(gachaSequence);
```

### 4. Trigger Player Actions

Call `TriggerAction` from button handlers or gameplay events.

```csharp
public void OnGachaButtonClicked()
{
    TutorialSystem.Instance.TriggerAction("GachaClick");
    // Continue normal game logic...
}
```

## API

### TutorialSystem

```csharp
void StartTutorial(TutorialSequence sequence)
void StartTutorialForce(TutorialSequence sequence)
void Skip()
void TriggerAction(string key)
bool IsCompleted(string sequenceId)
void Save()
void Load()
void ResetAll()
```

### TutorialRunner

```csharp
bool IsRunning
int  CurrentStepIndex

event Action      OnCompleted
event Action      OnSkipped
event Action<int> OnStepChanged
```

## Custom Steps

Inherit from `TutorialStep` and implement `Execute`.

```csharp
[CreateAssetMenu(menuName = "AchUtils/Tutorial/Steps/My Step")]
public class MyCustomStep : TutorialStep
{
    public GameObject TargetPanel;

    public override IEnumerator Execute(TutorialRunner runner)
    {
        TargetPanel.SetActive(true);

        while (!runner.HasTriggeredAction("PanelClosed"))
            yield return null;

        runner.ConsumeAction("PanelClosed");
        TargetPanel.SetActive(false);
    }
}
```

## Save Format

Completed `SequenceId` values are saved to `PlayerPrefs` as a comma-separated string.

```text
AchUtils_Tutorial_Completed = "FirstGacha,FirstEquip,FirstDungeon"
```

::: tip
If `SequenceId` is empty, completion is not saved. You can also disable saving with `SaveProgress = false`.
:::

## Flow Example

```text
Start
 ↓
HighlightButtonStep  "Click the gacha button"
 ↓
WaitForActionStep    "GachaClick"
 ↓
DialogueStep         "You obtained a character!"
 ↓
HighlightButtonStep  "Open the equipment screen"
 ↓
End -> MarkCompleted("FirstGacha")
```
