# 행동 기록 / 재생 시스템

플레이어의 입력을 **타임스탬프 프레임**으로 기록하고 원하는 속도로 재생합니다. 고스트 레이싱, 리플레이, AI 학습 데이터, 버그 재현에 활용합니다.

## 구조

```
ActionRecorder (순수 C# 인스턴스)
  — 입력 발생 시 InputFrame을 ReplayData에 추가

ActionPlayer (순수 C# 인스턴스)
  — Tick(deltaTime)으로 ReplayData를 재생하고 OnFrame 이벤트 발행

InputFrame (struct)
  — Timestamp, ActionKey, Axis, IsPressed

ReplayData (class)
  — TotalDuration + List<InputFrame>
```

## 빠른 시작

### 1. 기록

```csharp
using AchUtils.ActionReplay;

private readonly ActionRecorder recorder = new();

// 기록 시작
recorder.StartRecording();

// 입력 발생 시 기록
public void OnJumpPressed()
{
    recorder.RecordButton("Jump", pressed: true);
    // 실제 점프 실행 ...
}

public void OnMove(Vector2 axis)
{
    recorder.RecordAxis("Move", axis);
}

// 기록 종료
ReplayData data = recorder.StopRecording();
SaveReplay(data);   // 직렬화해서 파일/서버에 저장
```

### 2. 재생

```csharp
private readonly ActionPlayer player = new();

void Update()
{
    player.Tick(Time.deltaTime);
}

void PlayGhost(ReplayData data)
{
    player.OnFrame += HandleFrame;
    player.Play(data, speed: 1f);
}

void HandleFrame(InputFrame frame)
{
    switch (frame.ActionKey)
    {
        case "Jump":  ghost.Jump();         break;
        case "Move":  ghost.Move(frame.Axis); break;
        case "Attack": ghost.Attack();      break;
    }
}

player.OnCompleted += () =>
{
    Debug.Log("리플레이 완료");
    player.OnFrame -= HandleFrame;
};
```

## API

### ActionRecorder

```csharp
bool        IsRecording
ReplayData  LastRecording

void        StartRecording()
ReplayData  StopRecording()

// 이벤트 기록
void RecordButton(string key, bool pressed)
void RecordAxis(string key, Vector2 axis)
void Record(string key, Vector2 axis = default, bool pressed = true)
```

### ActionPlayer

```csharp
bool IsPlaying

void Play(ReplayData data, float speed = 1f)
void Stop()
void Tick(float deltaTime)

event Action<InputFrame> OnFrame       // 매 프레임 발행
event Action             OnCompleted
event Action             OnStopped
```

### InputFrame

```csharp
float   Timestamp   // 기록 시작부터의 경과 시간 (초)
string  ActionKey   // 액션 이름
Vector2 Axis        // 아날로그 입력 값
bool    IsPressed   // 버튼 누름 여부
```

## 재생 속도 조절

```csharp
player.Play(data, speed: 0.5f);  // 슬로우모션 리플레이
player.Play(data, speed: 2.0f);  // 2배속 요약
```

## 데이터 직렬화 / 저장

`ReplayData`는 `[Serializable]`이므로 JSON으로 변환해 저장할 수 있습니다.

```csharp
// 저장
string json = JsonUtility.ToJson(data);
File.WriteAllText("replay.json", json);

// 불러오기
string json = File.ReadAllText("replay.json");
ReplayData data = JsonUtility.FromJson<ReplayData>(json);
```

## 활용 사례

| 용도 | 설명 |
|------|------|
| 고스트 레이싱 | 최고 기록 플레이를 반투명 오브젝트로 재생 |
| 리플레이 시스템 | 죽음 직전 장면을 다시 보기 |
| AI 학습 데이터 | 인간 플레이 기록을 모방 학습에 사용 |
| 버그 재현 | QA가 발견한 버그를 입력 기록으로 완벽 재현 |
| 유튜브 클립 | 하이라이트 장면 자동 편집 |

::: warning 네트워크 동기화
멀티플레이어에서 리플레이를 동기화하려면 결정론적(Deterministic) 게임 루프가 필요합니다. 물리 기반 게임의 경우 `Physics.simulationMode`를 `Script`로 설정해 프레임 단위로 직접 제어하세요.
:::
