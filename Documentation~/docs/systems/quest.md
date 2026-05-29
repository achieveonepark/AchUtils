# 퀘스트 시스템

**Kill → Talk → Collect** 스텝을 조합하는 범용 퀘스트 파이프라인입니다. RPG부터 캐주얼까지 이벤트 기반으로 진행 상황을 추적합니다.

## 구조

```
QuestSystem (순수 C# 인스턴스)
  — 활성 퀘스트 관리, 이벤트 라우팅

QuestDefinition (ScriptableObject)
  — 제목, 설명, 보상 목록
  [SerializeReference] List<QuestStep>

QuestInstance (runtime)
  — 현재 스텝 인덱스, 완료 상태

QuestStep (abstract, [Serializable])
  ├── KillQuestStep     — 특정 적 N마리 처치
  ├── TalkQuestStep     — 특정 NPC와 대화
  └── CollectQuestStep  — 특정 아이템 N개 수집
```

## 빠른 시작

### 1. QuestDefinition 에셋 만들기

**Create → AchUtils/Quest/Quest Definition**

```
QuestId : "MainQuest_01"
Title   : "슬라임 토벌"

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

### 2. QuestSystem 인스턴스 만들기

`QuestSystem`은 상태만 관리하는 순수 C# 클래스입니다. 퀘스트를 소유할 객체에서 직접 생성합니다.

### 3. 퀘스트 시작

```csharp
using AchUtils.Quest;

private readonly QuestSystem questSystem = new();

[SerializeField] QuestDefinition mainQuest01;

var instance = questSystem.StartQuest(mainQuest01);

instance.OnStepCompleted += step =>
    Debug.Log($"스텝 {step} 완료");

instance.OnQuestCompleted += () =>
    GiveRewards(mainQuest01.Rewards);
```

### 4. 이벤트 발행

게임 내 어디서든 Progress를 호출합니다. 현재 활성인 모든 퀘스트가 이벤트를 수신합니다.

```csharp
// 몬스터 사망 시
questSystem.Progress("Kill", "Slime");

// NPC 대화 시
questSystem.Progress("Talk", "GuardNPC");

// 아이템 획득 시
questSystem.Progress("Collect", "SlimeGel");
```

## 이벤트 키 규칙

| 키 | 두 번째 인자 (`data`) | 스텝 |
|----|----------------------|------|
| `"Kill"` | 적 타입 string | `KillQuestStep` |
| `"Talk"` | NPC ID string | `TalkQuestStep` |
| `"Collect"` | 아이템 ID string | `CollectQuestStep` |

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
QuestStep       CurrentStep     // 현재 진행 중인 스텝

event Action<int> OnStepCompleted
event Action      OnQuestCompleted
```

## 커스텀 스텝 만들기

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
// 특정 위치 도달 시
questSystem.Progress("ReachLocation", "DungeonEntrance");
```

## 중복 시작 방지

같은 `QuestId`의 퀘스트는 중복 시작되지 않습니다. 이미 완료된 퀘스트도 다시 시작되지 않습니다.

```csharp
// 두 번 호출해도 인스턴스는 하나
questSystem.StartQuest(mainQuest01);
questSystem.StartQuest(mainQuest01); // 무시됨
```
