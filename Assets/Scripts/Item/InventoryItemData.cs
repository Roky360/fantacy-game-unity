using System.Collections.Generic;
using Slot;
using UnityEngine;

namespace Item
{
    public class InventoryItemData : ItemData
    {
        public InventoryItemData(ItemType type, GameObject groundItem, GameObject inventoryItem,
            List<SlotType> allowedSlots = null) : base(type, groundItem, inventoryItem, allowedSlots ??
            new List<SlotType>
            {
                SlotType.Inventory
            })
        {
        }

        private static InventoryItemData _noneItemData = new InventoryItemData(ItemType.None, null, null);
        public static InventoryItemData None => _noneItemData;
    }
}