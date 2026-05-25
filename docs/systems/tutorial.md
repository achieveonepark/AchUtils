# 튜토리얼 시스템

게임 내 튜토리얼을 **노드(ScriptableObject) 기반**으로 설계합니다. 하드코딩 없이 기획자가 수정 가능하고, 스킵·재진입·저장·복구를 기본 지원합니다.

## 구조

```
TutorialSystem          — MonoBehaviour 싱글턴. 완료 상태 저장/복원 관리
  └── TutorialRunner    — 실제 스텝 실행 코루틴 담당 (자동 AddComponent)

TutorialSequence        — ScriptableObject. 스텝 목록 + 스킵 허용 여부
  └── TutorialStep[]    — ScriptableObject 베이스. 구체 스텝을 상속해서 제작
```

### 기본 제공 스텝

| 스텝 | 설명 |
|------|------|
| `HighlightButtonStep` | UI 요소를 색상으로 강조하고 특정 액션 대기 |
| `WaitForActionStep` | 키 이름으로 등록된 플레이어 액션 대기 |
| `DialogueStep` | 메시지 패널 표시 + 자동 진행 또는 클릭 대기 |
| `DelayStep` | N초 대기 |

## 빠른 시작

### 1. 씬에 TutorialSystem 추가

```csharp
// TutorialSystem이 있는 GameObject를 씬에 배치하면 끝.
// TutorialRunner는 자동으로 AddComponent됩니다.
```

### 2. TutorialSequence 에셋 만들기

**Project 창 → 우클릭 → Create → AchUtils/Tutorial/Tutorial Sequence**

인스펙터에서 스텝을 추가합니다.

```
SequenceId : "FirstGacha"
CanSkip    : true
Steps:
  [0] HighlightButtonStep  — GachaButton 강조, WaitForAction: "GachaClick"
  [1] DialogueStep         — "캐릭터를 획득했습니다!"
  [2] HighlightButtonStep  — EquipButton 강조
```

### 3. 튜토리얼 시작

```csharp
// 이미 완료한 튜토리얼은 자동으로 건너뜁니다.
TutorialSystem.Instance.StartTutorial(gachaSequence);

// 강제 재시작 (완료 여부 무시)
TutorialSystem.Instance.StartTutorialForce(gachaSequence);
```

### 4. 플레이어 액션 알리기

버튼 OnClick 또는 게임 이벤트 발생 시 호출합니다.

```csharp
// 버튼 클릭 핸들러
public void OnGachaButtonClicked()
{
    TutorialSystem.Instance.TriggerAction("GachaClick");
    // 이후 게임 로직 ...
}
```

## API

### TutorialSystem

```csharp
// 튜토리얼 시작 (완료된 경우 무시)
void StartTutorial(TutorialSequence sequence)

// 강제 시작
void StartTutorialForce(TutorialSequence sequence)

// 현재 튜토리얼 스킵
void Skip()

// 플레이어 액션 발행 (WaitForActionStep이 수신)
void TriggerAction(string key)

// 완료 여부 확인
bool IsCompleted(string sequenceId)

// PlayerPrefs 저장/복원
void Save()
void Load()
void ResetAll()
```

### TutorialRunner

```csharp
bool IsRunning            // 실행 중 여부
int  CurrentStepIndex     // 현재 스텝 인덱스

event Action         OnCompleted
event Action         OnSkipped
event Action<int>    OnStepChanged
```

## 커스텀 스텝 만들기

`TutorialStep`을 상속하고 `Execute`를 구현합니다.

```csharp
[CreateAssetMenu(menuName = "AchUtils/Tutorial/Steps/My Step")]
public class MyCustomStep : TutorialStep
{
    public GameObject TargetPanel;

    public override IEnumerator Execute(TutorialRunner runner)
    {
        TargetPanel.SetActive(true);

        // 특정 액션 대기
        while (!runner.HasTriggeredAction("PanelClosed"))
            yield return null;

        runner.ConsumeAction("PanelClosed");
        TargetPanel.SetActive(false);
    }
}
```

## 저장 방식

`PlayerPrefs`에 완료된 `SequenceId` 목록을 콤마 구분 문자열로 저장합니다.

```
AchUtils_Tutorial_Completed = "FirstGacha,FirstEquip,FirstDungeon"
```

::: tip
`SequenceId`를 비워두면 저장되지 않습니다. `SaveProgress = false`로도 비활성화할 수 있습니다.
:::

## 플로우 예시

```
Start
 ↓
HighlightButtonStep  "가챠 버튼 클릭"
 ↓
WaitForActionStep    "GachaClick"
 ↓
DialogueStep         "캐릭터를 획득했습니다!"
 ↓
HighlightButtonStep  "장비창 열기"
 ↓
End → MarkCompleted("FirstGacha")
```
