# 포뮬러 시스템

수식을 **ScriptableObject 문자열**로 정의해 기획자가 코드 없이 스킬 데미지, 경험치, 밸런스 수치를 직접 수정합니다.

## 개요

```
FormulaAsset (ScriptableObject)
  Expression: "Attack * SkillMultiplier - Defense + Random(0, 10)"

FormulaContext  — 변수 컨테이너 (key → float)
FormulaEvaluator — 재귀 하강 파서, 런타임 계산
```

## 빠른 시작

### 1. FormulaAsset 만들기

**Create → AchUtils/Formula/Formula Asset**

인스펙터 `Expression` 필드에 수식을 입력합니다.

```
Attack * SkillMultiplier - Defense + Random(0, 10)
```

### 2. 런타임에서 계산

```csharp
using AchieveOnePark.AchUtils.Formula;

// 컨텍스트에 변수 설정
var ctx = new FormulaContext();
ctx.Set("Attack",          sheet.GetFinal("Attack"));
ctx.Set("SkillMultiplier", skill.Multiplier);
ctx.Set("Defense",         target.GetFinal("Defense"));

// 평가
float damage = damageFormula.Evaluate(ctx);
target.TakeDamage(damage);
```

## 지원 문법

### 연산자

| 연산자 | 설명 | 예시 |
|--------|------|------|
| `+` `-` | 덧셈/뺄셈 | `Attack + 50` |
| `*` `/` | 곱셈/나눗셈 | `Attack * 1.5` |
| `( )` | 우선순위 그룹 | `(Attack + Defense) * 0.5` |
| `-` (단항) | 부호 반전 | `-Defense` |

### 내장 함수

| 함수 | 설명 |
|------|------|
| `Random(min, max)` | 랜덤 float |
| `Min(a, b)` | 최솟값 |
| `Max(a, b)` | 최댓값 |
| `Abs(x)` | 절댓값 |
| `Clamp(x, min, max)` | 범위 제한 |
| `Floor(x)` | 내림 |
| `Ceil(x)` | 올림 |
| `Round(x)` | 반올림 |
| `Sqrt(x)` | 제곱근 |
| `Pow(x, exp)` | 거듭제곱 |
| `Sign(x)` | 부호 (-1, 0, 1) |

## 수식 예시

```
# 스킬 데미지
Attack * SkillMultiplier - Max(Defense - Penetration, 0)

# 힐 량
HealPower * 0.3 + Max(0, TargetMissingHP * 0.1)

# 방치형 골드 계산
BaseGold * Pow(1.15, Floor(Level / 10)) + Random(0, 50)

# 크리티컬 데미지
Attack * Clamp(CritMultiplier, 1.5, 3.0)
```

## API

### FormulaContext

```csharp
void  Set(string key, float value)
float Get(string key)         // 없으면 Exception
bool  Has(string key)
void  Clear()
```

### FormulaAsset

```csharp
// ScriptableObject 필드
[TextArea] string Expression

// 계산
float Evaluate(FormulaContext context)
```

### FormulaEvaluator (직접 사용 시)

```csharp
var evaluator = new FormulaEvaluator();
float result = evaluator.Evaluate("Attack * 2 + 10", ctx);
```

## 에러 처리

변수가 없거나 구문 오류가 있으면 `System.Exception`이 발생합니다.

```csharp
try
{
    float damage = formula.Evaluate(ctx);
}
catch (Exception e)
{
    Debug.LogError($"[Formula] {formula.name}: {e.Message}");
    damage = 0f;
}
```

::: warning 스레드 안전성
`FormulaEvaluator`는 파서 상태를 인스턴스 필드로 보유하므로 동시에 여러 스레드에서 같은 인스턴스를 사용하지 마세요. `FormulaAsset`은 내부에 단일 evaluator를 캐싱합니다.
:::
