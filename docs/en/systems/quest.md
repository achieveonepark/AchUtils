# Quest System

A general quest pipeline built from **Kill -> Talk -> Collect** steps. It tracks progress through gameplay events and works for RPGs, casual games, and mission systems.

## Structure

```text
QuestSystem (MonoBehaviour singleton)
  - Manages active quests and routes events

QuestDefinition (ScriptableObject)
  - Title, description, rewards
  [SerializeReference] List<QuestStep>

QuestInstance (runtime)
  - Current step index and completion state

QuestStep (abstract, [Serializable])
  ├── KillQuestStep     - Kill N enemies of a type
  ├── TalkQuestStep     - Talk to a specific NPC
  └── CollectQuestStep  - Collect N items of a type
```

## Quick Start

### 1. Create a QuestDefinition

**Create -> AchUtils/Quest/Quest Definition**

```text
QuestId : "MainQuest_01"
Title   : "Slime Hunt"

Steps:
  [0] KillQuestStep
        EnemyType     : "Slime"
        RequiredCount : 5
  [1] TalkQuestStep
        NpcId         : "GuardNPC"
  [2] CollectQuestStep
        ItemId        : "SlimeGel"
        RequiredCount : 3

Rewards:
  [0] QuestReward { RewardId: "Gold", Amount: 500 }
  [1] QuestReward { RewardId: "Exp",  Amount: 200 }
```

### 2. Add QuestSystem to the Scene

Place a `QuestSystem` component in the scene.

### 3. Start a Quest

```csharp
using AchieveOnePark.AchUtils.Quest;

var instance = QuestSystem.Instance.StartQuest(mainQuest01);

instance.OnStepCompleted += step =>
    Debug.Log($"Step {step} completed");

instance.OnQuestCompleted += () =>
    GiveRewards(mainQuest01.Rewards);
```

### 4. Publish Events

Call `Progress` from anywhere in the game. Every active quest receives the event.

```csharp
QuestSystem.Instance.Progress("Kill", "Slime");
QuestSystem.Instance.Progress("Talk", "GuardNPC");
QuestSystem.Instance.Progress("Collect", "SlimeGel");
```

## Event Key Rules

| Key | `data` argument | Step |
|-----|-----------------|------|
| `"Kill"` | Enemy type string | `KillQuestStep` |
| `"Talk"` | NPC ID string | `TalkQuestStep` |
| `"Collect"` | Item ID string | `CollectQuestStep` |

## API

### QuestSystem

```csharp
QuestInstance StartQuest(QuestDefinition definition)

void Progress(string eventKey, object data = null)

bool          IsCompleted(string questId)
bool          IsActive(string questId)
QuestInstance GetActive(string questId)
List<QuestInstance> GetAllActive()

event Action<QuestInstance>      OnQuestStarted
event Action<QuestInstance>      OnQuestCompleted
event Action<QuestInstance, int> OnQuestStepCompleted
```

### QuestInstance

```csharp
QuestDefinition Definition
int             CurrentStepIndex
bool            IsCompleted
QuestStep       CurrentStep

event Action<int> OnStepCompleted
event Action      OnQuestCompleted
```

## Custom Steps

```csharp
[Serializable]
public class ReachLocationStep : QuestStep
{
    public string LocationId;
    private bool _reached;

    public override bool IsComplete => _reached;

    public override void OnProgress(string eventKey, object data)
    {
        if (eventKey == "ReachLocation" && data is string loc && loc == LocationId)
            _reached = true;
    }

    public override void OnStart() => _reached = false;
}
```

```csharp
QuestSystem.Instance.Progress("ReachLocation", "DungeonEntrance");
```

## Duplicate Start Guard

The same `QuestId` cannot be started twice. Completed quests are not started again.

```csharp
QuestSystem.Instance.StartQuest(mainQuest01);
QuestSystem.Instance.StartQuest(mainQuest01); // Ignored
```
