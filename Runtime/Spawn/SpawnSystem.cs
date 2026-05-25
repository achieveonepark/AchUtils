using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Spawn
{
    public class SpawnSystem : MonoBehaviour
    {
        public static SpawnSystem Instance { get; private set; }

        private readonly List<Spawner> _spawners = new();

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void RegisterSpawner(Spawner spawner)
        {
            if (!_spawners.Contains(spawner)) _spawners.Add(spawner);
        }

        public void UnregisterSpawner(Spawner spawner) => _spawners.Remove(spawner);

        public void StartAll()
        {
            foreach (var s in _spawners) s.StartSpawning();
        }

        public void StopAll()
        {
            foreach (var s in _spawners) s.StopSpawning();
        }

        public void DespawnAll()
        {
            foreach (var s in _spawners) s.DespawnAll();
        }

        public IReadOnlyList<Spawner> Spawners => _spawners;
    }
}
