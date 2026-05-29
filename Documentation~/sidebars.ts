import type { SidebarsConfig } from '@docusaurus/plugin-content-docs';

const sidebars: SidebarsConfig = {
  guide: [
    {
      type: 'category',
      label: '시작하기',
      collapsed: false,
      items: [
        'guide/getting-started',
        'guide/installation',
        'guide/extensions/index',
      ],
    },
    {
      type: 'category',
      label: '게임 플로우',
      collapsed: false,
      items: [
        'systems/tutorial',
        'systems/condition-graph',
      ],
    },
    {
      type: 'category',
      label: '데이터 / 밸런스',
      collapsed: false,
      items: [
        'systems/stat-modifier',
        'systems/formula',
        'systems/curve-data',
      ],
    },
    {
      type: 'category',
      label: '연출',
      collapsed: false,
      items: [
        'systems/sequence',
        'systems/camera-director',
      ],
    },
    {
      type: 'category',
      label: '게임플레이',
      collapsed: false,
      items: [
        'systems/buff-debuff',
        'systems/quest',
        'systems/spawn',
      ],
    },
    {
      type: 'category',
      label: '고급',
      collapsed: false,
      items: [
        'systems/action-replay',
        'systems/time-rewind',
      ],
    },
    {
      type: 'category',
      label: '릴리스',
      items: [
        'changelog',
      ],
    },
  ],
};

export default sidebars;
