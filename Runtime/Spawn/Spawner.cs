using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnGroup _group;
        [SerializeField] private bool _autoStart = false;

        private readonly List<GameObject> _alive = new();
        private Coroutine _coroutine;
        private SpawnSystem _spawnSystem;

        public bool IsSpawning { get; private set; }
        public IReadOnlyList<GameObject> AliveObjects => _alive;

        public event Action<GameObject> OnSpawned;
        public event Action<GameObject> OnDespawned;

        private void OnEnable()
        {
            _spawnSystem?.RegisterSpawner(this);
        }

        private void Start()
        {
            if (_autoStart) StartSpawning();
        }

        private void OnDisable()
        {
            _spawnSystem?.UnregisterSpawner(this);
            StopSpawning();
        }

        public void StartSpawning()
        {
            if (IsSpawning || _group == null) return;
            IsSpawning = true;
            _coroutine = StartCoroutine(SpawnLoop());
        }

        public void StopSpawning()
        {
            if (_coroutine != null) { StopCoroutine(_coroutine); _coroutine = null; }
            IsSpawning = false;
        }

        public void DespawnAll()
        {
            foreach (var obj in new List<GameObject>(_alive))
                Despawn(obj);
        }

        private IEnumerator SpawnLoop()
        {
            while (IsSpawning)
            {
                CleanDead();
                if (_alive.Count < _group.MaxAlive)
                    TrySpawn();
                yield return new WaitForSeconds(_group.SpawnInterval);
            }
        }

        private void TrySpawn()
        {
            var entry = _group.PickRandom();
            if (entry?.Prefab == null) return;

            int count = UnityEngine.Random.Range(entry.MinCount, entry.MaxCount + 1);
            count = Mathf.Min(count, _group.MaxAlive - _alive.Count);

            for (int i = 0; i < count; i++)
            {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * _group.SpawnRadius;
                Vector3 pos = transform.position + new Vector3(circle.x, 0, circle.y);
                var obj = Instantiate(entry.Prefab, pos, Quaternion.identity);
                _alive.Add(obj);
                OnSpawned?.Invoke(obj);
            }
        }

        private void Despawn(GameObject obj)
        {
            _alive.Remove(obj);
            if (obj != null) Destroy(obj);
            OnDespawned?.Invoke(obj);
        }

        private void CleanDead()
        {
            _alive.RemoveAll(o => o == null);
        }

        public void Bind(SpawnSystem spawnSystem)
        {
            if (_spawnSystem == spawnSystem) return;

            _spawnSystem?.UnregisterSpawner(this);
            _spawnSystem = spawnSystem;
            if (isActiveAndEnabled)
                _spawnSystem?.RegisterSpawner(this);
        }
    }
}
