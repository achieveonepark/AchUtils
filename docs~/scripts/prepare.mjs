import { cp, rm, writeFile } from 'node:fs/promises';
import path from 'node:path';

const usagePages = [
  'extensions',
  'tutorial',
  'ach',
  'condition-graph',
  'stat-modifier',
  'formula',
  'curve-data',
  'buff-debuff',
  'quest',
  'spawn',
  'sequence',
  'action-replay',
  'time-rewind',
  'camera-director',
];

export async function prepareDocsRoot(docsDir) {
  const docsRoot = path.join(docsDir, '.build-content');
  await rm(docsRoot, { recursive: true, force: true });
  await cp(path.join(docsDir, 'content'), docsRoot, { recursive: true });
  await cp(path.join(docsDir, 'public'), path.join(docsRoot, 'public'), {
    recursive: true,
  });

  for (const locale of ['ja', 'zh']) {
    await cp(
      path.join(docsRoot, 'ko', 'usage'),
      path.join(docsRoot, locale, 'usage'),
      { recursive: true },
    );
    await cp(
      path.join(docsRoot, 'ko', 'changelog.mdx'),
      path.join(docsRoot, locale, 'changelog.mdx'),
    );
    await writeFile(
      path.join(docsRoot, locale, 'usage', '_meta.json'),
      `${JSON.stringify(usagePages, null, 2)}\n`,
    );
  }
}
