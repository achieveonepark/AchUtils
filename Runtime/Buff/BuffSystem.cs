using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Buff
{
    public class BuffSystem
    {
        private readonly Dictionary<GameObject, List<BuffInstance>> _buffs = new();

        public event Action<BuffInstance> OnBuffApplied;
        public event Action<BuffInstance> OnBuffRemoved;

        public void Tick(float deltaTime)
        {
            foreach (var kvp in _buffs)
            {
                var list = kvp.Value;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var buff = list[i];

                    if (!buff.IsPermanent)
                        buff.RemainingDuration -= deltaTime;

                    if (buff.Definition.TickInterval > 0f)
                    {
                        buff.TickTimer += deltaTime;
                        if (buff.TickTimer >= buff.Definition.TickInterval)
                        {
                            buff.TickTimer -= buff.Definition.TickInterval;
                            buff.Tick();
                        }
                    }

                    if (buff.IsExpired)
                        RemoveAt(kvp.Key, list, i);
                }
            }
        }

        public BuffInstance Apply(BuffDefinition definition, GameObject target)
        {
            if (!_buffs.TryGetValue(target, out var list))
            {
                list = new List<BuffInstance>();
                _buffs[target] = list;
            }

            var existing = list.Find(b => b.Definition.BuffId == definition.BuffId);
            if (existing != null)
            {
                if (existing.Stacks < definition.MaxStacks)
                {
                    existing.Stacks++;
                    foreach (var e in definition.Effects)
                        e.OnStackAdded(existing, target, existing.Stacks);
                }

                if (definition.RefreshDurationOnReapply)
                    existing.RemainingDuration = definition.Duration;
                return existing;
            }

            var buff = new BuffInstance(definition, target);
            list.Add(buff);
            buff.ApplyEffects();
            OnBuffApplied?.Invoke(buff);
            return buff;
        }

        public bool Remove(string buffId, GameObject target)
        {
            if (!_buffs.TryGetValue(target, out var list)) return false;
            int idx = list.FindIndex(b => b.Definition.BuffId == buffId);
            if (idx < 0) return false;
            RemoveAt(target, list, idx);
            return true;
        }

        public void RemoveAll(GameObject target)
        {
            if (!_buffs.TryGetValue(target, out var list)) return;
            for (int i = list.Count - 1; i >= 0; i--)
                RemoveAt(target, list, i);
        }

        public bool Has(string buffId, GameObject target)
        {
            return _buffs.TryGetValue(target, out var list) &&
                   list.Exists(b => b.Definition.BuffId == buffId);
        }

        public List<BuffInstance> GetBuffs(GameObject target)
        {
            return _buffs.TryGetValue(target, out var list)
                ? new List<BuffInstance>(list)
                : new List<BuffInstance>();
        }

        private void RemoveAt(GameObject target, List<BuffInstance> list, int index)
        {
            var buff = list[index];
            buff.RemoveEffects();
            list.RemoveAt(index);
            OnBuffRemoved?.Invoke(buff);
        }
    }
}
