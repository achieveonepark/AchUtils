import { defineConfig } from '@rspress/core';
import path from 'node:path';

const locales = {
  ko: { label: '한국어', description: 'Unity 게임 개발을 위한 재사용 가능한 시스템 모음' },
  en: { label: 'English', description: 'A collection of reusable systems for Unity game development' },
  ja: { label: '日本語', description: 'Unity ゲーム開発向けの再利用可能なシステム集' },
  zh: { label: '简体中文', description: '用于 Unity 游戏开发的可复用系统集合' },
} as const;

const locale = process.env.ACHUTILS_DOCS_LOCALE ?? 'ko';
const localeInfo = locales[locale as keyof typeof locales];

if (!localeInfo) {
  throw new Error(`Unsupported docs locale: ${locale}`);
}

const base = `/AchUtils/${locale}/`;

export default defineConfig({
  outDir: path.resolve('out', locale),
  base,
  title: 'AchUtils',
  description: localeInfo.description,
  lang: locale,
  icon: '/icon.svg',
  logo: {
    light: '/logo.svg',
    dark: '/logo.svg',
  },
  themeConfig: {
    darkMode: true,
    nav: [
      {
        text: localeInfo.label,
        items: Object.entries(locales).map(([code, info]) => ({
          text: info.label,
          link: `/AchUtils/${code}/`,
        })),
      },
    ],
    socialLinks: [
      {
        icon: 'github',
        mode: 'link',
        content: 'https://github.com/achieveonepark/AchUtils',
      },
    ],
  },
});
