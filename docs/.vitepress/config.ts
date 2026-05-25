import { defineConfig } from 'vitepress'

export default defineConfig({
  title: 'AchUtils',
  description: 'AchUtils — Unity 게임 개발을 위한 재사용 가능한 13가지 시스템',
  base: '/AchUtils/',
  lang: 'ko-KR',
  appearance: 'dark',
  cleanUrls: true,

  head: [
    ['link', { rel: 'icon', href: '/favicon.svg', type: 'image/svg+xml' }],
    ['link', { rel: 'preconnect', href: 'https://fonts.googleapis.com' }],
    ['link', { rel: 'preconnect', href: 'https://fonts.gstatic.com', crossorigin: '' }],
    ['link', { href: 'https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&family=JetBrains+Mono:wght@400;500&display=swap', rel: 'stylesheet' }],
  ],

  themeConfig: {
    logo: { light: '/logo-light.svg', dark: '/logo-dark.svg', alt: 'SS' },
    siteTitle: 'AchUtils',

    nav: [
      { text: '가이드', link: '/guide/getting-started', activeMatch: '/guide/' },
      { text: '시스템', link: '/systems/tutorial', activeMatch: '/systems/' },
      {
        text: 'v1.0.0',
        items: [
          { text: '변경 이력', link: '/changelog' },
          { text: 'GitHub', link: 'https://github.com/achieveonepark/AchUtils' },
        ]
      }
    ],

    sidebar: {
      '/guide/': [
        {
          text: '시작하기',
          items: [
            { text: 'SS란?', link: '/guide/getting-started' },
            { text: '설치', link: '/guide/installation' },
          ]
        }
      ],
      '/systems/': [
        {
          text: '게임 플로우',
          collapsed: false,
          items: [
            { text: '🎓 튜토리얼', link: '/systems/tutorial' },
            { text: '📋 조건 그래프', link: '/systems/condition-graph' },
            { text: '🔴 레드닷', link: '/systems/red-dot' },
          ]
        },
        {
          text: '데이터 / 밸런스',
          collapsed: false,
          items: [
            { text: '⚔️ 스탯 모디파이어', link: '/systems/stat-modifier' },
            { text: '🧮 포뮬러', link: '/systems/formula' },
            { text: '📈 커브 데이터', link: '/systems/curve-data' },
          ]
        },
        {
          text: '연출',
          collapsed: false,
          items: [
            { text: '▶️ 시퀀스', link: '/systems/sequence' },
            { text: '🎬 카메라 디렉터', link: '/systems/camera-director' },
          ]
        },
        {
          text: '게임플레이',
          collapsed: false,
          items: [
            { text: '✨ 버프/디버프', link: '/systems/buff-debuff' },
            { text: '📜 퀘스트', link: '/systems/quest' },
            { text: '🌀 스폰', link: '/systems/spawn' },
          ]
        },
        {
          text: '고급',
          collapsed: false,
          items: [
            { text: '🎥 행동 기록/재생', link: '/systems/action-replay' },
            { text: '⏪ 시간 되감기', link: '/systems/time-rewind' },
          ]
        }
      ]
    },

    socialLinks: [
      { icon: 'github', link: 'https://github.com/achieveonepark/AchUtils' }
    ],

    search: {
      provider: 'local',
      options: {
        translations: {
          button: { buttonText: '검색', buttonAriaLabel: '검색' },
          modal: {
            noResultsText: '결과 없음',
            resetButtonTitle: '초기화',
            footer: { selectText: '선택', navigateText: '이동', closeText: '닫기' }
          }
        }
      }
    },

    editLink: {
      pattern: 'https://github.com/achieveonepark/AchUtils/edit/main/docs/:path',
      text: '이 페이지 편집'
    },

    lastUpdated: {
      text: '마지막 업데이트',
      formatOptions: { dateStyle: 'short', timeStyle: 'short' }
    },

    footer: {
      message: 'MIT License',
      copyright: 'Copyright © 2024 AchieveOnePark'
    },

    docFooter: {
      prev: '이전',
      next: '다음'
    },

    outline: {
      label: '이 페이지에서',
      level: [2, 3]
    },

    returnToTopLabel: '맨 위로',
    darkModeSwitchLabel: '테마',
    lightModeSwitchTitle: '라이트 모드로',
    darkModeSwitchTitle: '다크 모드로',
  },

  markdown: {
    theme: { light: 'github-light', dark: 'one-dark-pro' },
    lineNumbers: true,
  }
})
