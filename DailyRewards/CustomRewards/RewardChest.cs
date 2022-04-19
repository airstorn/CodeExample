using Data;
using Data.Api;
using UnityEngine;

namespace Game.DailyRewards.CustomRewards
{
    [CreateAssetMenu(fileName = "LootBox", menuName = "Game/DailyRewards/LootBox", order = 0)]
    public class RewardChest : DailyReward
    {
        [SerializeField] private InventoryInstance _inventory;

        private ItemModel _data;
        
        public override void ApplyReward()
        {
        }
        

        public override string Payload()
        {
            return "X1";
        }

        public void SetData(ItemModel data)
        {
            _data = data;
        }
      
        public override string Description()
        {
            return _data.Title;
        }
    }
}