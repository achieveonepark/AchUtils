using System;
using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.RedDot
{
    public class RedDotSystem
    {
        private static RedDotSystem _instance;
        public static RedDotSystem Instance => _instance ??= new RedDotSystem();

        private readonly Dictionary<string, RedDotNode> _nodes = new();

        public void Register(string path, Func<bool> provider)
        {
            var node = GetOrCreate(path);
            node.Provider = provider;
            BuildParentChain(path);
        }

        public void Unregister(string path)
        {
            if (_nodes.TryGetValue(path, out var node))
                node.Provider = null;
        }

        public bool Evaluate(string path)
        {
            return _nodes.TryGetValue(path, out var node) && node.Evaluate();
        }

        public void Notify(string path)
        {
            if (_nodes.TryGetValue(path, out var node))
                node.Evaluate();
        }

        public void NotifyAll()
        {
            foreach (var node in _nodes.Values)
                node.Evaluate();
        }

        public void Subscribe(string path, Action<bool> callback)
        {
            GetOrCreate(path).Subscribe(callback);
        }

        public void Unsubscribe(string path, Action<bool> callback)
        {
            if (_nodes.TryGetValue(path, out var node))
                node.Unsubscribe(callback);
        }

        private RedDotNode GetOrCreate(string path)
        {
            if (!_nodes.TryGetValue(path, out var node))
            {
                node = new RedDotNode(path);
                _nodes[path] = node;
            }
            return node;
        }

        private void BuildParentChain(string path)
        {
            int lastSlash = path.LastIndexOf('/');
            if (lastSlash <= 0) return;

            string parentPath = path.Substring(0, lastSlash);
            var parent = GetOrCreate(parentPath);
            var child = GetOrCreate(path);

            BuildParentChain(parentPath);
            parent.AddChild(child);
        }
    }
}
