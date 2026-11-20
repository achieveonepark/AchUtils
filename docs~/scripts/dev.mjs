import path from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { prepareDocsRoot } from './prepare.mjs';

const docsDir = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const docsRoot = await prepareDocsRoot(docsDir, 'ko');
const result = spawnSync('rspress', ['dev', docsRoot], {
  cwd: docsDir,
  env: { ...process.env, ACHUTILS_DOCS_LOCALE: 'ko' },
  stdio: 'inherit',
});

if (result.error) {
  throw result.error;
}
process.exit(result.status ?? 1);
