using Data;
using Game.Customization.CustomItems;
using UnityEngine;

namespace Game.DailyRewards.CustomRewards
{
    [CreateAssetMenu(fileName = "Skin", menuName = "Game/DailyRewards/Skin", order = 0)]

    public class RewardSkin : DailyReward
    {
        [SerializeField] private PreviewableItem[] itemsPool;
        [SerializeField] private int _amount = 1;

        [SerializeField] private InventoryInstance _inventory;
        [SerializeField] private PreviewableItem _selected;

        public override string Description()
        {
            return _selected.LocalizedName;
        }

        public override void Init()
        {
            _selected = itemsPool[Random.Range(0, itemsPool.Length)];
        }


        public override string Payload()
        {
            return $"X{_amount}";
        }

        public override Sprite PreviewIcon()
        {
            return _selected.Icon;
        }
        
        public override void ApplyReward()
        {
            _inventory.Add(_selected.Reference.ID);
            _inventory.CallSaving();
        }
    }
}