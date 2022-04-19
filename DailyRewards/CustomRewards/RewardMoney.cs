using Game.DailyRewards.Api.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.DailyRewards.CustomRewards
{
    [CreateAssetMenu(fileName = "Money", menuName = "Game/DailyRewards/Money", order = 0)]
    public class RewardMoney : DailyReward
    {
        [SerializeField] private int _count;
        
        public override string Payload()
        {
            return _count.ToString();
        }

        public override string Description()
        {
            return $"{Payload()} COIN DOGE";
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