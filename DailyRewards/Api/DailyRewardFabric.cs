using System.Linq;
using Data.Api;
using Game.DailyRewards.Api.Data;
using Game.DailyRewards.CustomRewards;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.DailyRewards.Api
{
    public class DailyRewardFabric
    {
        private ContentBuilder _builder;
    
        public DailyRewardFabric(ContentBuilder contentBuilder)
        {
            _builder = contentBuilder;
        }

        public DailyReward CreateInstance(DailyRewardsModel.Reward data)
        {
            DailyReward reward = null;
            Sprite icon = null;
            
            switch (data.Type)
            {
                case "item":
                    reward = ScriptableObject.CreateInstance<RewardChest>();

                    var value = JsonConvert.DeserializeObject<ItemModel>(data.Value.ToString());

                    icon = _builder.Previews.Single(link => link.Id == value.Id).Icon;
                    var converted = (RewardChest)reward;
                    converted.SetData(value);
                    break;
                case "exp":
                    reward = ScriptableObject.CreateInstance<RewardXP>();
                    icon = _builder.Previews.Single(link => link.Id == data.Type).Icon;
                    break;
                case "coins":
                    reward = ScriptableObject.CreateInstance<RewardMoney>();
                    icon = _builder.Previews.Single(link => link.Id == data.Type).Icon;
                    break;
            }

            if (reward == null) return null;
            
            reward.Init(data, icon);

            return reward;
        }
    }
}