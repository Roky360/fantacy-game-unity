using System;
using UnityEngine;

namespace Item
{
    public class UsableItemData : HotbarItemData
    {
        public Action onUse { get; }
        
        public UsableItemData(ItemType type, GameObject groundItem, GameObject inventoryItem, Action onUse)
            : base(type, groundItem, inventoryItem)
        {
            this.onUse = onUse;
        }
    }
}