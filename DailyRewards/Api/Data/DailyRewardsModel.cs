using System.Collections.Generic;
using System.Runtime.Serialization;
using Data.Api;

namespace Game.DailyRewards.Api.Data
{
    [DataContract]
    public class DailyRewardsModel
    {
        [DataContract]
        public class Reward
        {
            [DataMember(Name = "type")]
            public string Type;
            [DataMember(Name = "value")]
            public object Value;
            [DataMember(Name = "position")]
            public int Position;
        }

        [DataContract]
        public class DataStorage
        {
            [DataMember(Name = "position")]
            public int Position;
            [DataMember(Name = "player")]
            public AuthDetailsData Player;
            [DataMember(Name = "rewards")]
            public List<Reward> Rewards;
            [DataMember(Name = "reward")]
            public Reward Reward;
        }

        [DataMember(Name = "status")]
        public bool Status;
        [DataMember(Name = "data")]
        public DataStorage Data;
    }
}