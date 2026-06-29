using AchUtils.Ach;
using UnityEngine;

namespace AchUtils.Samples
{
    /// <summary>
    /// Ach(공유 변수 &amp; 이벤트) 시스템의 최소 사용 예시.
    /// 인스펙터에 Variable / Event / Reference / Collection 에셋을 연결한 뒤 실행하면,
    /// 데미지를 받을 때마다 체력 변수가 갱신되고 사망 이벤트가 발생합니다.
    /// </summary>
    public sealed class AchSampleController : MonoBehaviour
    {
        [Header("Variable — 공유되는 값")]
        [SerializeField] private IntVariable playerHp;

        [Header("Event — 신호")]
        [SerializeField] private IntEvent onHpChanged;
        [SerializeField] private VoidEvent onPlayerDied;

        [Header("Reference — 상수/변수 토글")]
        [SerializeField] private IntReference startingHp = new IntReference(100);

        [Header("Collection — 런타임 공유 목록")]
        [SerializeField] private GameObjectCollection aliveEnemies;

        private void OnEnable()
        {
            playerHp.OnChanged += HandleHpChanged;
        }

        private void OnDisable()
        {
            playerHp.OnChanged -= HandleHpChanged;
        }

        private void Start()
        {
            // Reference 는 상수든 변수든 .Value 로 동일하게 읽습니다.
            playerHp.Value = startingHp.Value;
        }

        /// <summary>외부(적, 함정 등)에서 호출하는 피격 처리.</summary>
        public void TakeDamage(int amount)
        {
            playerHp.Value = Mathf.Max(0, playerHp.Value - amount);
        }

        private void HandleHpChanged(int hp)
        {
            onHpChanged?.Raise(hp);

            if (hp <= 0)
            {
                onPlayerDied?.Raise();
            }
        }

        /// <summary>적이 스폰될 때 호출 — 공유 컬렉션에 등록.</summary>
        public void RegisterEnemy(GameObject enemy)
        {
            aliveEnemies.AddIfMissing(enemy);
        }

        /// <summary>적이 죽을 때 호출 — 컬렉션에서 제거하고 남은 수를 로그로 출력.</summary>
        public void UnregisterEnemy(GameObject enemy)
        {
            aliveEnemies.Remove(enemy);
            Debug.Log($"남은 적: {aliveEnemies.Count}");
        }
    }
}
