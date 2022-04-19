using Game.DailyRewards.Api.Data;
using UnityEngine;

namespace Game.DailyRewards
{
    public abstract class DailyReward : ScriptableObject
    {
        [SerializeField] protected int _day;
        [SerializeField] protected Sprite _icon;

        public int Day => _day;
        public Sprite Icon => _icon;
        public virtual Sprite PreviewIcon() => _icon;

        public abstract void ApplyReward();
        public abstract string Payload();
        public abstract string Description();

        public virtual void Init()
        {
            
        }

        public virtual void Init(DailyRewardsModel.Reward data, Sprite icon)
        {
            _day = data.Position;
            _icon = icon;
        }
    }
}