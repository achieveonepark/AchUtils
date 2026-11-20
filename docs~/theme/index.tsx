import './index.css';
import {
  Callout as RspressCallout,
  getCustomMDXComponent as getDefaultMDXComponents,
} from '@rspress/core/theme-original';
import type { ComponentProps } from 'react';

type CalloutType = ComponentProps<typeof RspressCallout>['type'] | 'warn';
type CalloutProps = Omit<ComponentProps<typeof RspressCallout>, 'type'> & {
  type?: CalloutType;
};

function Callout({ type = 'info', ...props }: CalloutProps) {
  return (
    <RspressCallout
      {...props}
      type={type === 'warn' ? 'warning' : type}
    />
  );
}

function getCustomMDXComponent() {
  return {
    ...getDefaultMDXComponents(),
    Callout,
  };
}

export { Callout, getCustomMDXComponent };
export * from '@rspress/core/theme-original';
