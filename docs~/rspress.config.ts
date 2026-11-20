import { defineConfig } from '@rspress/core';

const locales = {
  ko: { label: '한국어', description: 'Unity 게임 개발을 위한 재사용 가능한 시스템 모음' },
  en: { label: 'English', description: 'A collection of reusable systems for Unity game development' },
  ja: { label: '日本語', description: 'Unity ゲーム開発向けの再利用可能なシステム集' },
  zh: { label: '简体中文', description: '用于 Unity 游戏开发的可复用系统集合' },
} as const;

export default defineConfig({
  root: '.build-content',
  outDir: 'out',
  base: '/AchUtils/',
  title: 'AchUtils',
  description: locales.ko.description,
  lang: 'ko-KR',
  locales: Object.entries(locales).map(([lang, info]) => ({ lang, ...info })),
  route: { localeRedirect: 'never' },
  icon: '/icon.svg',
  logo: {
    light: '/logo.svg',
    dark: '/logo.svg',
  },
  themeConfig: {
    darkMode: true,
    socialLinks: [
      {
        icon: 'github',
        mode: 'link',
        content: 'https://github.com/achieveonepark/AchUtils',
      },
    ],
  },
});
