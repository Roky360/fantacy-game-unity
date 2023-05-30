using System;
using UnityEngine;

namespace Item
{
    public class ConsumableItemData : HotbarItemData
    {
        public Action onConsume { get; }

        public ConsumableItemData(ItemType type, GameObject groundItem, GameObject inventoryItem, Action onConsume)
            : base(type, groundItem, inventoryItem)
        {
            this.onConsume = onConsume;
        }
    }
}