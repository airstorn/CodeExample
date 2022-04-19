using Game.DailyRewards.Api.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.DailyRewards.CustomRewards
{
    [CreateAssetMenu(fileName = "XP", menuName = "Game/DailyRewards/XP", order = 0)]
    public class RewardXP : DailyReward
    {
        [SerializeField] private int _count;
        
        
        public override string Payload()
        {
            return _count.ToString();
        }

        public override string Description()
        {
            return $"{Payload()} XP";
        }

        public override void Init(DailyRewardsModel.Reward data, Sprite icon)
        {
            base.Init(data, icon);
            int amount = JsonConvert.DeserializeObject<int>(data.Value.ToString());

            _count = amount;
        }

        public override void ApplyReward()
        {
        }
    }
}