import { defineConfig } from 'vitepress'

const koSidebar = [
  {
    text: '시작하기',
    items: [
      { text: 'AchUtils란?', link: '/guide/getting-started' },
      { text: '설치', link: '/guide/installation' },
    ],
  },
  {
    text: '게임 플로우',
    collapsed: false,
    items: [
      { text: '튜토리얼', link: '/systems/tutorial' },
      { text: '조건 그래프', link: '/systems/condition-graph' },
      { text: '레드닷', link: '/systems/red-dot' },
    ],
  },
  {
    text: '데이터 / 밸런스',
    collapsed: false,
    items: [
      { text: '스탯 모디파이어', link: '/systems/stat-modifier' },
      { text: '포뮬러', link: '/systems/formula' },
      { text: '커브 데이터', link: '/systems/curve-data' },
    ],
  },
  {
    text: '연출',
    collapsed: false,
    items: [
      { text: '시퀀스', link: '/systems/sequence' },
      { text: '카메라 디렉터', link: '/systems/camera-director' },
    ],
  },
  {
    text: '게임플레이',
    collapsed: false,
    items: [
      { text: '버프/디버프', link: '/systems/buff-debuff' },
      { text: '퀘스트', link: '/systems/quest' },
      { text: '스폰', link: '/systems/spawn' },
    ],
  },
  {
    text: '고급',
    collapsed: false,
    items: [
      { text: '행동 기록/재생', link: '/systems/action-replay' },
      { text: '시간 되감기', link: '/systems/time-rewind' },
    ],
  },
  {
    text: '릴리스',
    items: [
      { text: '변경 이력', link: '/changelog' },
    ],
  },
]

const enSidebar = [
  {
    text: 'Start',
    items: [
      { text: 'What is AchUtils?', link: '/en/guide/getting-started' },
      { text: 'Installation', link: '/en/guide/installation' },
    ],
  },
  {
    text: 'Game Flow',
    collapsed: false,
    items: [
      { text: 'Tutorial', link: '/en/systems/tutorial' },
      { text: 'Condition Graph', link: '/en/systems/condition-graph' },
      { text: 'Red Dot', link: '/en/systems/red-dot' },
    ],
  },
  {
    text: 'Data / Balance',
    collapsed: false,
    items: [
      { text: 'Stat Modifier', link: '/en/systems/stat-modifier' },
      { text: 'Formula', link: '/en/systems/formula' },
      { text: 'Curve Data', link: '/en/systems/curve-data' },
    ],
  },
  {
    text: 'Presentation',
    collapsed: false,
    items: [
      { text: 'Sequence', link: '/en/systems/sequence' },
      { text: 'Camera Director', link: '/en/systems/camera-director' },
    ],
  },
  {
    text: 'Gameplay',
    collapsed: false,
    items: [
      { text: 'Buff / Debuff', link: '/en/systems/buff-debuff' },
      { text: 'Quest', link: '/en/systems/quest' },
      { text: 'Spawn', link: '/en/systems/spawn' },
    ],
  },
  {
    text: 'Advanced',
    collapsed: false,
    items: [
      { text: 'Action Replay', link: '/en/systems/action-replay' },
      { text: 'Time Rewind', link: '/en/systems/time-rewind' },
    ],
  },
  {
    text: 'Release',
    items: [
      { text: 'Changelog', link: '/en/changelog' },
    ],
  },
]

const koSearch = {
  provider: 'local' as const,
  options: {
    translations: {
      button: { buttonText: '검색', buttonAriaLabel: '검색' },
      modal: {
        noResultsText: '결과 없음',
        resetButtonTitle: '초기화',
        footer: { selectText: '선택', navigateText: '이동', closeText: '닫기' },
      },
    },
  },
}

const enSearch = {
  provider: 'local' as const,
}

export default defineConfig({
  title: 'AchUtils',
  description: 'AchUtils — 13 reusable systems for Unity game development',
  base: '/AchUtils/',
  appearance: 'dark',
  cleanUrls: true,

  head: [
    ['link', { rel: 'icon', href: '/favicon.svg', type: 'image/svg+xml' }],
    ['link', { rel: 'preconnect', href: 'https://fonts.googleapis.com' }],
    ['link', { rel: 'preconnect', href: 'https://fonts.gstatic.com', crossorigin: '' }],
    ['link', { href: 'https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&family=JetBrains+Mono:wght@400;500&display=swap', rel: 'stylesheet' }],
  ],

  themeConfig: {
    logo: { light: '/logo-light.svg', dark: '/logo-dark.svg', alt: 'AchUtils' },
    siteTitle: 'AchUtils',

    socialLinks: [
      { icon: 'github', link: 'https://github.com/achieveonepark/AchUtils' },
    ],

    footer: {
      message: 'MIT License',
      copyright: 'Copyright © 2024 AchieveOnePark',
    },
  },

  locales: {
    root: {
      label: '한국어',
      lang: 'ko-KR',
      title: 'AchUtils',
      description: 'AchUtils — Unity 게임 개발을 위한 재사용 가능한 13가지 시스템',
      themeConfig: {
        nav: [
          { text: '빠른 시작', link: '/guide/getting-started', activeMatch: '/guide/' },
          { text: '시스템', link: '/systems/tutorial', activeMatch: '/systems/' },
          { text: '변경 이력', link: '/changelog' },
          {
            text: 'v1.0.0',
            items: [
              { text: 'GitHub', link: 'https://github.com/achieveonepark/AchUtils' },
            ],
          },
        ],

        sidebar: koSidebar,
        search: koSearch,

        editLink: {
          pattern: 'https://github.com/achieveonepark/AchUtils/edit/main/docs/:path',
          text: '이 페이지 편집',
        },

        lastUpdated: {
          text: '마지막 업데이트',
          formatOptions: { dateStyle: 'short', timeStyle: 'short' },
        },

        docFooter: {
          prev: '이전',
          next: '다음',
        },

        outline: {
          label: '이 페이지에서',
          level: [2, 3],
        },

        returnToTopLabel: '맨 위로',
        darkModeSwitchLabel: '테마',
        lightModeSwitchTitle: '라이트 모드로',
        darkModeSwitchTitle: '다크 모드로',
      },
    },

    en: {
      label: 'English',
      lang: 'en-US',
      link: '/en/',
      title: 'AchUtils',
      description: 'AchUtils — 13 reusable systems for Unity game development',
      themeConfig: {
        nav: [
          { text: 'Quick Start', link: '/en/guide/getting-started', activeMatch: '/en/guide/' },
          { text: 'Systems', link: '/en/systems/tutorial', activeMatch: '/en/systems/' },
          { text: 'Changelog', link: '/en/changelog' },
          {
            text: 'v1.0.0',
            items: [
              { text: 'GitHub', link: 'https://github.com/achieveonepark/AchUtils' },
            ],
          },
        ],

        sidebar: enSidebar,
        search: enSearch,

        editLink: {
          pattern: 'https://github.com/achieveonepark/AchUtils/edit/main/docs/:path',
          text: 'Edit this page',
        },

        lastUpdated: {
          text: 'Last updated',
          formatOptions: { dateStyle: 'medium', timeStyle: 'short' },
        },

        docFooter: {
          prev: 'Previous',
          next: 'Next',
        },

        outline: {
          label: 'On this page',
          level: [2, 3],
        },

        returnToTopLabel: 'Return to top',
        darkModeSwitchLabel: 'Theme',
        lightModeSwitchTitle: 'Switch to light mode',
        darkModeSwitchTitle: 'Switch to dark mode',
      },
    },
  },

  markdown: {
    theme: { light: 'github-light', dark: 'one-dark-pro' },
    lineNumbers: true,
  },
})
