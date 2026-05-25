# 레드닷 시스템

UI 알림 뱃지(🔴)를 **경로 기반 계층 구조**로 관리합니다. 자식 경로에 알림이 있으면 부모에도 자동으로 전파됩니다.

## 구조

```
RedDotSystem  (순수 C# 싱글턴)
  ├── "Shop"            ← 자식에 알림 있으면 자동 true
  │     ├── "Shop/Free"       Provider: () => HasFreeItem()
  │     └── "Shop/Discount"   Provider: () => HasDiscount()
  └── "Mail"
        └── "Mail/Reward"     Provider: () => HasReward()

RedDotView  (MonoBehaviour)  — 경로 구독 + GameObject 토글
```

## 빠른 시작

### 1. 프로바이더 등록

```csharp
using AchieveOnePark.AchUtils.RedDot;

void Start()
{
    // 슬래시(/)로 계층 경로 표현
    RedDotSystem.Instance.Register("Shop/Free",     () => shopService.HasFreeItem());
    RedDotSystem.Instance.Register("Shop/Discount", () => shopService.HasDiscount());
    RedDotSystem.Instance.Register("Mail/Reward",   () => mailService.HasReward());
}
```

### 2. UI에 RedDotView 붙이기

`RedDotView` 컴포넌트를 버튼에 추가하고 `Path`와 `Dot Object`를 설정합니다.

```
Button "ShopButton"
  └── RedDotView
        Path       : "Shop"        ← 자식 전체 집계
        Dot Object : RedDot (Image)
```

### 3. 상태 변경 시 알리기

```csharp
// 무료 아이템 수령 후
await shopService.ClaimFreeItem();
RedDotSystem.Instance.Notify("Shop/Free");

// 또는 전체 갱신
RedDotSystem.Instance.NotifyAll();
```

## API

### RedDotSystem

```csharp
// 프로바이더 등록 / 해제
void Register(string path, Func<bool> provider)
void Unregister(string path)

// 즉시 평가
bool Evaluate(string path)

// 강제 재평가 + 리스너 통지
void Notify(string path)
void NotifyAll()

// 변화 구독 / 해제
void Subscribe(string path, Action<bool> callback)
void Unsubscribe(string path, Action<bool> callback)
```

### RedDotView (Inspector)

| 필드 | 설명 |
|------|------|
| `Path` | 구독할 레드닷 경로 |
| `Dot Object` | 표시/숨김을 토글할 GameObject |

## 경로 규칙

```
"Mail"              — 메일 전체
"Mail/Reward"       — 메일 > 보상
"Shop"              — 상점 전체 (자식 집계)
"Shop/Free"         — 상점 > 무료
"Shop/Discount"     — 상점 > 할인
```

부모 경로는 등록하지 않아도 됩니다. 자식 경로가 등록되면 자동으로 부모 노드가 생성되고 자식 값을 집계합니다.

## 코드로 구독

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

::: warning 성능 주의
`NotifyAll()`은 모든 프로바이더를 즉시 호출합니다. 프레임마다 호출하지 말고 상태가 변경된 시점에만 호출하세요.
:::
