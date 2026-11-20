import { mkdir, writeFile } from 'node:fs/promises';
import path from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { prepareDocsRoot } from './prepare.mjs';

const docsDir = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
await prepareDocsRoot(docsDir);
const result = spawnSync('rspress', ['build'], { cwd: docsDir, stdio: 'inherit' });

if (result.error) {
  throw result.error;
}
if (result.status !== 0) {
  process.exit(result.status ?? 1);
}

const outputDir = path.join(docsDir, 'out');
await mkdir(outputDir, { recursive: true });
await writeFile(
  path.join(outputDir, 'index.html'),
  '<!doctype html><html lang="ko"><head><meta charset="utf-8"><meta http-equiv="refresh" content="0;url=/AchUtils/ko/"><link rel="canonical" href="/AchUtils/ko/"><title>AchUtils</title></head><body><a href="/AchUtils/ko/">한국어 문서로 이동</a></body></html>\n',
);
