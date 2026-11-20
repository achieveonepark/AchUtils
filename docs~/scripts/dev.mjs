import path from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { prepareDocsRoot } from './prepare.mjs';

const docsDir = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
await prepareDocsRoot(docsDir);
const result = spawnSync('rspress', ['dev'], { cwd: docsDir, stdio: 'inherit' });

if (result.error) {
  throw result.error;
}
process.exit(result.status ?? 1);
