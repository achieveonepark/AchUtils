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

export async function prepareDocsRoot(docsDir, locale) {
  const docsRoot = path.join(docsDir, '.build-content', locale);
  await rm(docsRoot, { recursive: true, force: true });
  await cp(path.join(docsDir, 'content', locale), docsRoot, { recursive: true });
  await cp(path.join(docsDir, 'public'), path.join(docsRoot, 'public'), {
    recursive: true,
  });

  if (locale === 'ja' || locale === 'zh') {
    await cp(
      path.join(docsDir, 'content', 'ko', 'usage'),
      path.join(docsRoot, 'usage'),
      { recursive: true },
    );
    await cp(
      path.join(docsDir, 'content', 'ko', 'changelog.mdx'),
      path.join(docsRoot, 'changelog.mdx'),
    );
    await writeFile(
      path.join(docsRoot, 'usage', '_meta.json'),
      `${JSON.stringify(usagePages, null, 2)}\n`,
    );
  }

  return path.relative(docsDir, docsRoot);
}
