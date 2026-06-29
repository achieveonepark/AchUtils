using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>런타임에 공유되는 GameObject 집합(예: 살아 있는 적 목록).</summary>
    [CreateAssetMenu(menuName = "AchUtils/Ach/Collections/GameObject Collection", fileName = "NewGameObjectCollection")]
    public class GameObjectCollection : AchCollection<GameObject>
    {
    }
}
