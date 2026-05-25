using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Spawn
{
    [CreateAssetMenu(menuName = "AchUtils/Spawn/Spawn Group", fileName = "NewSpawnGroup")]
    public class SpawnGroup : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public GameObject Prefab;
            [Range(0f, 100f)] public float Weight = 1f;
            public int MinCount = 1;
            public int MaxCount = 1;
        }

        public List<Entry> Entries = new();
        public float SpawnInterval = 5f;
        public int MaxAlive = 10;
        public float SpawnRadius = 5f;

        public Entry PickRandom()
        {
            float total = 0f;
            foreach (var e in Entries) total += e.Weight;
            float roll = UnityEngine.Random.Range(0f, total);
            float acc = 0f;
            foreach (var e in Entries)
            {
                acc += e.Weight;
                if (roll <= acc) return e;
            }
            return Entries.Count > 0 ? Entries[^1] : null;
        }
    }
}
