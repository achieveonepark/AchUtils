using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.Spawn
{
    public class SpawnSystem
    {
        private readonly List<Spawner> _spawners = new();

        public SpawnSystem()
        {
        }

        public SpawnSystem(IEnumerable<Spawner> spawners)
        {
            if (spawners == null) return;

            foreach (var spawner in spawners)
                spawner?.Bind(this);
        }

        public void RegisterSpawner(Spawner spawner)
        {
            if (spawner != null && !_spawners.Contains(spawner))
                _spawners.Add(spawner);
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
