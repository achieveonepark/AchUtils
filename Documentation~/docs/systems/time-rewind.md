# 시간 되감기 시스템

`IRewindable` 인터페이스를 구현한 **모든 오브젝트의 상태를 스냅샷으로 기록**하고 N초 전 상태로 복원합니다.

## 구조

```
TimeRewindSystem (순수 C# 인스턴스)
  — 등록된 IRewindable 오브젝트의 스냅샷을 주기적으로 기록
  — RewindTo(float secondsAgo)로 특정 시점 복원

IRewindable (interface)
  — CaptureState()  : 현재 상태를 object로 반환
  — RestoreState()  : 캡처된 상태로 복원

RewindableObject (MonoBehaviour)
  — 기본 구현: Transform(position, rotation, scale)을 기록
```

## 빠른 시작

### 1. TimeRewindSystem 인스턴스 만들기

`TimeRewindSystem`은 순수 C# 클래스입니다. 소유 컴포넌트에서 생성하고 `Update`에서 `Tick`을 호출합니다.

| 생성자 인자 | 기본값 | 설명 |
|----------------|--------|------|
| `maxHistorySeconds` | 5 | 최대 기록 시간 (초) |
| `snapshotsPerSecond` | 10 | 초당 스냅샷 수 |

### 2. RewindableObject 부착

복원할 오브젝트에 `RewindableObject` 컴포넌트를 추가합니다. Transform이 자동으로 기록됩니다.

```csharp
// 플레이어, 적, 발사체 등 되감기할 오브젝트에 부착
gameObject.AddComponent<RewindableObject>();
```

### 3. 되감기 실행

```csharp
using AchUtils.TimeRewind;

[SerializeField] RewindableObject[] rewindables;

private TimeRewindSystem timeRewindSystem;

void Awake()
{
    timeRewindSystem = new TimeRewindSystem(maxHistorySeconds: 5f, snapshotsPerSecond: 10);
    foreach (var rewindable in rewindables)
        rewindable.Bind(timeRewindSystem);
}

void Update()
{
    timeRewindSystem.Tick(Time.deltaTime);
}

// 되감기 시작 (Update에서 계속 호출 가능)
timeRewindSystem.StartRewind();

// 특정 시점으로 복원
timeRewindSystem.RewindTo(secondsAgo: 3f);

// 되감기 종료 (이후 다시 기록 시작)
timeRewindSystem.StopRewind();
```

### 4. 스킬로 사용하는 예시

```csharp
public class TimeRewindSkill : MonoBehaviour
{
    [SerializeField] float rewindSeconds = 3f;
    private readonly TimeRewindSystem timeRewindSystem = new();

    IEnumerator UseSkill()
    {
        var system = timeRewindSystem;
        system.StartRewind();

        float t = 0f;
        while (t < rewindSeconds)
        {
            t += Time.deltaTime;
            system.RewindTo(rewindSeconds - t);  // 점진적 되감기
            yield return null;
        }

        system.StopRewind();
    }
}
```

## API

### TimeRewindSystem

```csharp
TimeRewindSystem(float maxHistorySeconds = 5f, int snapshotsPerSecond = 10)

void Register(IRewindable target)
void Unregister(IRewindable target)

void Tick(float deltaTime)

void StartRewind()
void StopRewind()

// N초 전 상태로 모든 등록 오브젝트를 복원
void RewindTo(float secondsAgo)

// 단계별 되감기 (delta만큼 과거로)
void StepRewind(float deltaSeconds)

bool  IsRewinding
float HistoryDuration    // 현재 기록된 히스토리 길이 (초)

event Action OnRewindStart
event Action OnRewindStop
```

## IRewindable 직접 구현

`RewindableObject` 대신 커스텀 상태를 캡처하려면 인터페이스를 직접 구현합니다.

```csharp
public class EnemyAI : MonoBehaviour, IRewindable
{
    private TimeRewindSystem timeRewindSystem;

    private float hp;
    private Vector3 position;
    private AIState state;

    private void OnEnable()  => timeRewindSystem?.Register(this);
    private void OnDisable() => timeRewindSystem?.Unregister(this);

    public object CaptureState()
    {
        return (hp, transform.position, state);
    }

    public void RestoreState(object snapshot)
    {
        (hp, position, state) = ((float, Vector3, AIState))snapshot;
        transform.position = position;
    }
}
```

## 리소스 최적화

```
스냅샷 개수 = MaxHistorySeconds × SnapshotsPerSecond × 등록 오브젝트 수

예: 5초 × 10fps × 20오브젝트 = 1000 스냅샷
```

오브젝트가 많거나 히스토리가 길면 `SnapshotsPerSecond`를 낮추거나 `MaxHistorySeconds`를 줄이세요.

## 활용 사례

| 용도 | 설명 |
|------|------|
| 퍼즐 되돌리기 | 잘못 놓은 블록을 이전 상태로 복원 |
| 시간 역행 스킬 | Braid, Titanfall 스타일 시간 되감기 |
| 디버깅 | 버그 발생 직전 상태로 되돌아가 재현 |
| 리플레이 미리보기 | 죽기 전 몇 초를 슬로우로 다시 보기 |

::: warning 물리 엔진
Rigidbody를 가진 오브젝트를 되감으면 물리 상태(velocity, angular velocity)도 함께 캡처해야 올바르게 복원됩니다. `RewindableObject`를 상속해 `CaptureState`를 오버라이드하세요.

```csharp
public override object CaptureState()
{
    var rb = GetComponent<Rigidbody>();
    return (base.CaptureState(), rb.velocity, rb.angularVelocity);
}
```
:::
