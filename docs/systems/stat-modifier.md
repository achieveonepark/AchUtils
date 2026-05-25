# 스탯 모디파이어

RPG, 방치형, 전략 장르에 필수인 **스탯 계산 파이프라인**입니다. Flat → PercentAdd → PercentMultiply 3단계 순서로 최종값을 계산하며, 소스별로 모디파이어를 추가/제거합니다.

## 계산 공식

```
최종값 = (기본값 + Flat 합산) × (1 + PercentAdd 합산) × ΠPercentMultiply
```

**예시:**
```
기본 공격력 : 100
무기        : +20  (Flat)
버프        : +30% (PercentAdd)
패시브       : ×1.15 (PercentMultiply)

최종 = (100 + 20) × (1 + 0.30) × 1.15
     = 120 × 1.30 × 1.15
     = 179.4
```

## 구조

```
StatSheet               — 스탯 컨테이너 (순수 C#)
  └── Dictionary<string, StatValue>

StatValue               — 단일 스탯 + 모디파이어 목록
  └── List<StatModifier>

StatSheetComponent      — MonoBehaviour 래퍼 (GameObject에 부착)
```

## 빠른 시작

```csharp
using AchieveOnePark.AchUtils.StatModifier;

// 1. StatSheet 생성 (또는 StatSheetComponent 컴포넌트 사용)
var sheet = new StatSheet();

// 2. 기본값 설정
sheet.SetBase("Attack", 100f);
sheet.SetBase("Defense", 50f);

// 3. 모디파이어 추가
sheet.AddModifier("Attack", new StatModifier("weapon_sword",  ModifierType.Flat,            20f));
sheet.AddModifier("Attack", new StatModifier("buff_rage",     ModifierType.PercentAdd,       0.3f));
sheet.AddModifier("Attack", new StatModifier("passive_might", ModifierType.PercentMultiply,  0.15f));

// 4. 최종값 조회
float finalAtk = sheet.GetFinal("Attack");   // 179.4
```

## StatModifier 생성자

```csharp
new StatModifier(
    source:   "weapon_sword",       // 식별자 (제거 시 사용)
    type:     ModifierType.Flat,    // Flat | PercentAdd | PercentMultiply
    value:    20f,
    priority: 0                     // 선택적. 현재는 정렬에 미사용
)
```

## API

### StatSheet

```csharp
// 기본값 설정
void SetBase(string key, float value)

// 최종값 조회
float GetFinal(string key)

// StatValue 직접 접근 (이벤트 구독 등)
StatValue GetStat(string key)

// 모디파이어 관리
void AddModifier(string key, StatModifier modifier)
void RemoveModifier(string key, string source)
void RemoveAllModifiers(string key)

bool HasStat(string key)
event Action<string> OnStatChanged   // 변경된 스탯 키 전달
```

### StatValue

```csharp
float BaseValue      { get; set; }   // 기본값
float FinalValue     { get; }        // 계산된 최종값 (읽기 전용)

void AddModifier(StatModifier modifier)
bool RemoveModifier(string source)
void RemoveAllModifiers()

event Action OnChanged
```

## MonoBehaviour와 함께 사용

```csharp
// 캐릭터 컴포넌트에서
var sheetComp = GetComponent<StatSheetComponent>();
sheetComp.Sheet.SetBase("HP", 500f);
sheetComp.Sheet.SetBase("Attack", 100f);

// 장비 장착 시
sheetComp.Sheet.AddModifier("Attack",
    new StatModifier("equip_sword", ModifierType.Flat, 35f));

// 장비 해제 시
sheetComp.Sheet.RemoveModifier("Attack", "equip_sword");
```

## 스탯 변경 감지

```csharp
sheet.OnStatChanged += statKey =>
{
    if (statKey == "HP") UpdateHPBar(sheet.GetFinal("HP"));
};
```

## 자주 사용하는 소스 명명 규칙

```
"base"          — 기본값 (SetBase로 관리)
"equip_{id}"    — 장비 (equip_sword, equip_helm)
"buff_{id}"     — 버프 (버프 시스템과 연동)
"passive_{id}"  — 패시브 스킬
"set_{id}"      — 세트 효과
```

::: tip 버프 시스템 연동
`StatModifyEffect`를 사용하면 버프 적용/제거 시 `StatSheet`가 자동으로 업데이트됩니다.  
→ [버프/디버프 시스템 보기](/systems/buff-debuff)
:::
