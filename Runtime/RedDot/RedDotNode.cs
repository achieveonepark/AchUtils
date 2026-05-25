using System;
using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.RedDot
{
    internal class RedDotNode
    {
        public readonly string Path;
        public Func<bool> Provider;
        public bool CachedValue { get; private set; }

        private readonly List<RedDotNode> _children = new();
        private readonly List<Action<bool>> _listeners = new();

        public RedDotNode(string path)
        {
            Path = path;
        }

        public void AddChild(RedDotNode child) => _children.Add(child);

        public void Subscribe(Action<bool> listener) => _listeners.Add(listener);

        public void Unsubscribe(Action<bool> listener) => _listeners.Remove(listener);

        public bool Evaluate()
        {
            bool result = Provider?.Invoke() ?? false;
            if (!result)
                foreach (var child in _children)
                    if (child.Evaluate()) { result = true; break; }

            if (result != CachedValue)
            {
                CachedValue = result;
                foreach (var listener in _listeners)
                    listener(result);
            }
            return result;
        }
    }
}
