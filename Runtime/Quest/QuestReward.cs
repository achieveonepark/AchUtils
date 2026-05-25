using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Quest
{
    [Serializable]
    public class QuestReward
    {
        public string RewardId;
        public string Description;
        public int Amount;
        [Tooltip("Optional item/resource prefab or icon")]
        public Sprite Icon;
    }
}
