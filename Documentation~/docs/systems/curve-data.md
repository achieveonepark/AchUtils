# 커브 데이터 시스템

`AnimationCurve`로 레벨별 수치, 경험치 테이블, 스탯 성장 곡선을 정의합니다. 코드를 건드리지 않고 인스펙터에서 곡선을 드래그해 밸런스를 조정할 수 있습니다.

## 구조

```
CurveDataAsset (ScriptableObject)
  Curve       : AnimationCurve   — 0~1 → 0~1 정규화 곡선
  MinInput    : float            — 입력 범위 시작
  MaxInput    : float            — 입력 범위 끝
  MinOutput   : float            — 출력 범위 시작
  MaxOutput   : float            — 출력 범위 끝

CurveDataTable (ScriptableObject)
  List<Entry { Key, CurveDataAsset }>
```

## 빠른 시작

### 1. CurveDataAsset 만들기

**Create → AchUtils/Curve/Curve Data Asset**

```
Curve    : [그래프 에디터에서 곡선 편집]
MinInput : 1      MaxInput : 100   (레벨 1~100)
MinOutput: 100    MaxOutput: 50000 (HP 100~50000)
```

### 2. 코드에서 사용

```csharp
using AchUtils.Curve;

[SerializeField] CurveDataAsset hpCurve;
[SerializeField] CurveDataAsset expCurve;

// 레벨에 따른 HP 계산
float hp = hpCurve.Evaluate(player.Level);     // Level → HP

// 레벨 10의 필요 경험치
float exp = expCurve.Evaluate(10f);
```

### 3. CurveDataTable로 여러 커브 묶기

**Create → AchUtils/Curve/Curve Data Table**

```csharp
[SerializeField] CurveDataTable statTable;

float hp     = statTable.Evaluate("HP",     level);
float attack = statTable.Evaluate("Attack", level);
float def    = statTable.Evaluate("Defense", level);
```

## API

### CurveDataAsset

```csharp
// 입력 범위(MinInput~MaxInput)로 보간 후 출력 범위(MinOutput~MaxOutput)로 변환
float Evaluate(float input)

// 이미 0~1 범위의 t값으로 직접 평가
float EvaluateNormalized(float t)
```

### CurveDataTable

```csharp
float Evaluate(string key, float input)
bool  TryGetCurve(string key, out CurveDataAsset curve)
```

## 곡선 형태별 활용

```
선형 성장    HP = level * 500
             → AnimationCurve.Linear(0,0,1,1)

지수 성장    경험치 요구량
             → 우측으로 급격히 상승하는 S 커브

로그 성장    공격력 (초반 빠름, 후반 둔화)
             → 좌측으로 급격한 오목 커브

계단식       등급별 보상
             → 수평-수직 반복 스텝 커브
```

## 기존 코드와 비교

```csharp
// 변경 전 — 하드코딩, 수정할 때마다 빌드
float hp = level * 100 + level * level * 5;

// 변경 후 — 인스펙터에서 실시간 조정
float hp = hpCurve.Evaluate(level);
```

::: tip 에디터 미리보기
인스펙터에서 커브를 편집하면 `CurveDataAsset`의 `Preview` 창에서 결과값을 확인할 수 있습니다 (에디터 스크립트 별도 구현 시).
:::
