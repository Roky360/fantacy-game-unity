using Slot;
using UnityEngine;

namespace Item
{
    public class HotbarItemData : InventoryItemData
    {
        public HotbarItemData(ItemType type, GameObject groundItem, GameObject inventoryItem) 
            : base(type, groundItem, inventoryItem)
        {
            this.allowedSlots.Add(SlotType.HotBar);
        }
    }
}