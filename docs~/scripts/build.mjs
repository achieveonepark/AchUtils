import { mkdir, writeFile } from 'node:fs/promises';
import path from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { prepareDocsRoot } from './prepare.mjs';

const docsDir = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const locales = ['ko', 'en', 'ja', 'zh'];

for (const locale of locales) {
  const docsRoot = await prepareDocsRoot(docsDir, locale);
  const result = spawnSync('rspress', ['build', docsRoot], {
    cwd: docsDir,
    env: { ...process.env, ACHUTILS_DOCS_LOCALE: locale },
    stdio: 'inherit',
  });

  if (result.error) {
    throw result.error;
  }
  if (result.status !== 0) {
    process.exit(result.status ?? 1);
  }
}

const outputDir = path.join(docsDir, 'out');
await mkdir(outputDir, { recursive: true });
await writeFile(
  path.join(outputDir, 'index.html'),
  '<!doctype html><html lang="ko"><head><meta charset="utf-8"><meta http-equiv="refresh" content="0;url=/AchUtils/ko/"><link rel="canonical" href="/AchUtils/ko/"><title>AchUtils</title></head><body><a href="/AchUtils/ko/">한국어 문서로 이동</a></body></html>\n',
);
