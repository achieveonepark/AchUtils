import React from 'react';
import Link from '@docusaurus/Link';
import Layout from '@theme/Layout';
import Heading from '@theme/Heading';
import Translate, { translate } from '@docusaurus/Translate';
import styles from './index.module.css';

type Feature = {
  key: string;
  badge: string;
  titleId: string;
  titleDefault: string;
  descId: string;
  descDefault: string;
  href: string;
};

const features: Feature[] = [
  {
    key: 'tutorial',
    badge: 'FLOW',
    titleId: 'home.feature.tutorial.title',
    titleDefault: '튜토리얼 시스템',
    descId: 'home.feature.tutorial.desc',
    descDefault: '노드 기반 튜토리얼. 버튼 강조, 대기, 손가락 애니메이션, 스킵/재진입, 저장/복구까지.',
    href: '/systems/tutorial',
  },
  {
    key: 'condition-graph',
    badge: 'FLOW',
    titleId: 'home.feature.conditionGraph.title',
    titleDefault: '조건 그래프',
    descId: 'home.feature.conditionGraph.desc',
    descDefault: 'AND / OR / NOT 노드로 복잡한 게임 조건을 데이터로 정의. 상점 오픈, 이벤트, 업적에 활용.',
    href: '/systems/condition-graph',
  },
  {
    key: 'stat-modifier',
    badge: 'DATA',
    titleId: 'home.feature.statModifier.title',
    titleDefault: '스탯 모디파이어',
    descId: 'home.feature.statModifier.desc',
    descDefault: 'Flat / PercentAdd / PercentMultiply 3단계 계산. 무기, 버프, 패시브를 소스별로 관리.',
    href: '/systems/stat-modifier',
  },
  {
    key: 'formula',
    badge: 'DATA',
    titleId: 'home.feature.formula.title',
    titleDefault: '포뮬러 시스템',
    descId: 'home.feature.formula.desc',
    descDefault: '기획자가 직접 수정하는 수식 에셋. Attack * SkillMultiplier - Defense + Random(0,10).',
    href: '/systems/formula',
  },
  {
    key: 'curve-data',
    badge: 'DATA',
    titleId: 'home.feature.curveData.title',
    titleDefault: '커브 데이터',
    descId: 'home.feature.curveData.desc',
    descDefault: 'AnimationCurve로 레벨별 HP, 경험치 등 수치 정의. 코드 수정 없이 밸런스 조절.',
    href: '/systems/curve-data',
  },
  {
    key: 'sequence',
    badge: 'FX',
    titleId: 'home.feature.sequence.title',
    titleDefault: '시퀀스 시스템',
    descId: 'home.feature.sequence.desc',
    descDefault: 'Move, Wait, Fade, Scale, Sound. Coroutine 중첩 없이 연출 흐름을 선언적으로 구성.',
    href: '/systems/sequence',
  },
  {
    key: 'camera-director',
    badge: 'FX',
    titleId: 'home.feature.cameraDirector.title',
    titleDefault: '카메라 디렉터',
    descId: 'home.feature.cameraDirector.desc',
    descDefault: 'Zoom, Shake, Move를 체인으로. 게임 이벤트 흐름 중심의 카메라 연출.',
    href: '/systems/camera-director',
  },
  {
    key: 'buff-debuff',
    badge: 'PLAY',
    titleId: 'home.feature.buffDebuff.title',
    titleDefault: '버프 / 디버프',
    descId: 'home.feature.buffDebuff.desc',
    descDefault: 'Duration, Stack, Tick, Priority 설정. ScriptableObject 정의와 런타임 자동 관리.',
    href: '/systems/buff-debuff',
  },
  {
    key: 'quest',
    badge: 'PLAY',
    titleId: 'home.feature.quest.title',
    titleDefault: '퀘스트 시스템',
    descId: 'home.feature.quest.desc',
    descDefault: 'Kill, Talk, Collect 스텝을 조합. RPG부터 캐주얼까지 범용 퀘스트 파이프라인.',
    href: '/systems/quest',
  },
  {
    key: 'spawn',
    badge: 'PLAY',
    titleId: 'home.feature.spawn.title',
    titleDefault: '스폰 시스템',
    descId: 'home.feature.spawn.desc',
    descDefault: '가중치 기반 랜덤 스폰, 최대 생존 수, 반경, 인터벌을 에셋으로 설정.',
    href: '/systems/spawn',
  },
  {
    key: 'action-replay',
    badge: 'ADV',
    titleId: 'home.feature.actionReplay.title',
    titleDefault: '행동 기록 / 재생',
    descId: 'home.feature.actionReplay.desc',
    descDefault: '입력을 타임스탬프로 기록하고 속도 조절 재생. 리플레이와 버그 재현에 활용.',
    href: '/systems/action-replay',
  },
  {
    key: 'time-rewind',
    badge: 'ADV',
    titleId: 'home.feature.timeRewind.title',
    titleDefault: '시간 되감기',
    descId: 'home.feature.timeRewind.desc',
    descDefault: 'IRewindable 인터페이스로 어떤 객체든 히스토리 스냅샷 기반 N초 전 복원.',
    href: '/systems/time-rewind',
  },
];

function Hero() {
  return (
    <header className={styles.hero}>
      <div className={styles.heroInner}>
        <span className={styles.eyebrow}>
          <Translate id="home.eyebrow">Unity Game Systems</Translate>
        </span>
        <Heading as="h1" className={styles.title}>
          AchUtils
        </Heading>
        <p className={styles.tagline}>
          <Translate id="home.tagline">
            게임 개발의 반복 작업을 없애는 12가지 재사용 가능한 시스템. 각각 독립 인스턴스로 관리됩니다.
          </Translate>
        </p>
        <div className={styles.buttons}>
          <Link
            className={`button button--primary button--lg ${styles.cta}`}
            to="/guide/getting-started"
          >
            <Translate id="home.cta.primary">빠른 시작</Translate>
          </Link>
          <Link
            className={`button button--secondary button--lg ${styles.cta}`}
            to="/systems/tutorial"
          >
            <Translate id="home.cta.secondary">시스템 보기</Translate>
          </Link>
        </div>
      </div>
    </header>
  );
}

function FeatureGrid() {
  return (
    <section className={styles.features}>
      <div className={styles.featureGrid}>
        {features.map(feature => (
          <Link key={feature.key} to={feature.href} className={styles.card}>
            <span className={styles.cardBadge}>{feature.badge}</span>
            <Heading as="h3" className={styles.cardTitle}>
              <Translate id={feature.titleId}>{feature.titleDefault}</Translate>
            </Heading>
            <p className={styles.cardDesc}>
              <Translate id={feature.descId}>{feature.descDefault}</Translate>
            </p>
            <span className={styles.cardArrow} aria-hidden="true">→</span>
          </Link>
        ))}
      </div>
    </section>
  );
}

export default function Home() {
  return (
    <Layout
      title="AchUtils"
      description={translate({
        id: 'home.meta.desc',
        message: 'Unity 게임 개발을 위한 재사용 가능한 12가지 시스템',
      })}
    >
      <Hero />
      <FeatureGrid />
    </Layout>
  );
}
