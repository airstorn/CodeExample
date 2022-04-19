using System;
using System.Collections.Generic;
using Game.DailyRewards.Api.Data;
using UnityEngine;

namespace Game.DailyRewards.Api
{
    public class ContentBuilder : MonoBehaviour
    {
        [Serializable]
        public class IconLink
        {
            public string Id;
            public Sprite Icon;
        }

        [SerializeField] private IconLink[] _links;

        public IEnumerable<IconLink> Previews => _links;

        public List<DailyReward> ApplyData(DailyRewardsModel model)
        {
            DailyRewardFabric fabric = new DailyRewardFabric(this);
            List<DailyReward> convertedRewards = new List<DailyReward>();

            foreach (var reward in model.Data.Rewards)
            {
                var instance = fabric.CreateInstance(reward);
                
                instance.name = reward.Type + reward.Position;
                convertedRewards.Add(instance);
            }


            return convertedRewards;
        }

        public DailyReward ApplyData(DailyRewardsModel.Reward reward)
        {
            DailyRewardFabric fabric = new DailyRewardFabric(this);

            var instance = fabric.CreateInstance(reward);

            instance.name = reward.Type + reward.Position;

            return instance;
        }
    }
}