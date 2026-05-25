---
layout: home

hero:
  name: "AchUtils"
  text: "Unity Game Systems"
  tagline: 게임 개발의 반복 작업을 없애는 12가지 재사용 가능한 시스템. 각각 독립 인스턴스로 관리됩니다.
  actions:
    - theme: brand
      text: 빠른 시작 →
      link: /guide/getting-started
    - theme: alt
      text: 시스템 보기
      link: /systems/tutorial

features:
  - icon: 🎓
    title: 튜토리얼 시스템
    details: 노드 기반 튜토리얼. 버튼 강조, 대기, 손가락 애니메이션, 스킵/재진입, 저장/복구까지.
    link: /systems/tutorial
    linkText: 자세히 보기

  - icon: 📋
    title: 조건 그래프
    details: AND / OR / NOT 노드로 복잡한 게임 조건을 데이터로 정의. 상점 오픈, 이벤트, 업적에 활용.
    link: /systems/condition-graph
    linkText: 자세히 보기

  - icon: ⚔️
    title: 스탯 모디파이어
    details: Flat / PercentAdd / PercentMultiply 3단계 계산. 무기, 버프, 패시브를 소스별로 관리.
    link: /systems/stat-modifier
    linkText: 자세히 보기

  - icon: 🧮
    title: 포뮬러 시스템
    details: 기획자가 직접 수정하는 수식 에셋. Attack * SkillMultiplier - Defense + Random(0,10).
    link: /systems/formula
    linkText: 자세히 보기

  - icon: 📈
    title: 커브 데이터
    details: AnimationCurve로 레벨별 HP, 경험치 등 수치 정의. 코드 수정 없이 밸런스 조절.
    link: /systems/curve-data
    linkText: 자세히 보기

  - icon: ▶️
    title: 시퀀스 시스템
    details: Move → Wait → Fade → Scale → Sound. Coroutine 지옥 없이 연출 흐름을 선언적으로.
    link: /systems/sequence
    linkText: 자세히 보기

  - icon: 🎬
    title: 카메라 디렉터
    details: Zoom → Shake → Move를 체인으로. 게임 이벤트 흐름 중심의 카메라 연출.
    link: /systems/camera-director
    linkText: 자세히 보기

  - icon: ✨
    title: 버프 / 디버프
    details: Duration, Stack, Tick, Priority 설정. ScriptableObject 정의 + 런타임 자동 관리.
    link: /systems/buff-debuff
    linkText: 자세히 보기

  - icon: 📜
    title: 퀘스트 시스템
    details: Kill → Talk → Collect 스텝을 조합. RPG부터 캐주얼까지 범용 퀘스트 파이프라인.
    link: /systems/quest
    linkText: 자세히 보기

  - icon: 🌀
    title: 스폰 시스템
    details: 가중치 기반 랜덤 스폰, 최대 생존 수, 반경, 인터벌을 에셋으로 설정.
    link: /systems/spawn
    linkText: 자세히 보기

  - icon: 🎥
    title: 행동 기록 / 재생
    details: 입력을 타임스탬프로 기록하고 속도 조절 재생. 고스트 레이싱, 리플레이, 버그 재현.
    link: /systems/action-replay
    linkText: 자세히 보기

  - icon: ⏪
    title: 시간 되감기
    details: IRewindable 인터페이스로 어떤 객체든 히스토리 스냅샷 기반 N초 전 복원.
    link: /systems/time-rewind
    linkText: 자세히 보기
---
