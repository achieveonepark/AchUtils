import { themes as prismThemes } from 'prism-react-renderer';
import type { Config } from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

const repositoryUrl = 'https://github.com/achieveonepark/AchUtils';

const config: Config = {
  title: 'AchUtils',
  tagline: 'Unity 게임 개발을 위한 재사용 가능한 12가지 시스템',
  favicon: 'favicon.svg',

  url: 'https://achieveonepark.github.io',
  baseUrl: '/AchUtils/',
  organizationName: 'achieveonepark',
  projectName: 'AchUtils',

  onBrokenLinks: 'warn',

  markdown: {
    mermaid: true,
    format: 'detect',
    hooks: {
      onBrokenMarkdownLinks: 'warn',
    },
  },
  themes: ['@docusaurus/theme-mermaid'],

  clientModules: ['./src/clientModules/themeReset.js'],

  i18n: {
    defaultLocale: 'ko',
    locales: ['ko', 'en'],
    localeConfigs: {
      ko: { label: '한국어', htmlLang: 'ko-KR' },
      en: { label: 'English', htmlLang: 'en-US' },
    },
  },

  presets: [
    [
      'classic',
      {
        docs: {
          path: 'docs',
          routeBasePath: '/',
          sidebarPath: './sidebars.ts',
          showLastUpdateTime: true,
        },
        blog: false,
        theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    image: 'logo-light.svg',
    colorMode: {
      defaultMode: 'dark',
      respectPrefersColorScheme: false,
      disableSwitch: false,
    },
    navbar: {
      title: 'AchUtils',
      logo: {
        alt: 'AchUtils',
        src: 'logo-light.svg',
        srcDark: 'logo-dark.svg',
      },
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'guide',
          position: 'left',
          label: 'Docs',
        },
        { to: '/changelog', label: 'Change Log', position: 'left' },
        {
          type: 'localeDropdown',
          position: 'right',
        },
        {
          href: repositoryUrl,
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [],
      copyright: 'MIT License · Copyright © AchUtils',
    },
    prism: {
      theme: prismThemes.oneDark,
      darkTheme: prismThemes.oneDark,
      additionalLanguages: ['csharp', 'json', 'yaml', 'bash'],
    },
    mermaid: {
      theme: { light: 'base', dark: 'base' },
      options: {
        themeVariables: {
          primaryColor: '#1e3a5f',
          primaryTextColor: '#e2e8f0',
          primaryBorderColor: '#3b82f6',
          lineColor: '#64748b',
          secondaryColor: '#0f2d4a',
          tertiaryColor: '#162032',
          background: '#0d1b2a',
          mainBkg: '#1e3a5f',
          nodeBorder: '#3b82f6',
          clusterBkg: '#0f2d4a',
          titleColor: '#93c5fd',
          edgeLabelBackground: '#162032',
          fontFamily: '"Inter", "Noto Sans KR", sans-serif',
          fontSize: '14px',
        },
      },
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
