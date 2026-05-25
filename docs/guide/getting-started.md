# AchUtils란?

**AchUtils** 는 Unity 게임 개발에서 매번 반복되는 시스템들을 독립 인스턴스로 패키징한 컬렉션입니다.

튜토리얼, 조건 그래프, 스탯 모디파이어 등 12가지 시스템이 각각 독립적으로 동작하므로 필요한 것만 골라 사용할 수 있습니다.

## 특징

- **독립 인스턴스** — 대부분의 런타임 시스템은 순수 C# 인스턴스로 생성하고, 코루틴·Transform·Instantiate가 필요한 부분만 컴포넌트 사용
- **ScriptableObject 기반** — 데이터와 로직을 분리해 기획자가 직접 수정
- **`[SerializeReference]` 폴리모피즘** — 노드, 이펙트, 스텝을 인스펙터에서 직접 추가
- **Zero 외부 의존성** — 순수 Unity 런타임만 사용, 별도 패키지 불필요

## 어셈블리 정보

| 항목 | 값 |
|------|-----|
| 패키지 이름 | `com.achieveonepark.achutils` |
| 어셈블리 | `AchieveOnePark.AchUtils.Runtime` |
| 루트 네임스페이스 | `AchieveOnePark.AchUtils` |
| 최소 Unity 버전 | **2021.3 LTS** |

## 다음 단계

- [설치 →](/guide/installation)
- [첫 번째 시스템 — 튜토리얼 →](/systems/tutorial)
