using System.Collections.Generic;
using System.Linq;
using Item;
using Slot;
using UnityEngine;

namespace Inventory
{
    public partial class InventoryManager
    {
        /// <summary>
        /// ALL THE FUCKING GAME ITEMS!!
        /// </summary>
        [SerializeField] public ItemData[] GameItems;

        public Dictionary<ItemType, ItemData> ItemTypeToItemMap { get; } = new();

        /// <summary>
        /// All the contents of the inventory.
        /// </summary>
        public Dictionary<ItemType, int> InventoryContents;

        void InitializeItemsMap()
        {
            foreach (ItemData item in GameItems)
            {
                ItemTypeToItemMap.Add(item.Type, item);
            }
        }

        public bool IsItemMatchSlot(ItemType itemType, SlotType slotType)
        {
            return itemType == ItemType.None || ItemTypeToItemMap[itemType].allowedSlots.Contains(slotType);
        }

        #region Inventory Contents

        public void AddResourceToInventoryContents(ItemType itemType)
        {
            if (itemType == ItemType.None) return;
            if (InventoryContents.ContainsKey(itemType))
            {
                InventoryContents[itemType]++;
            }
            else
            {
                InventoryContents.Add(itemType, 1);
            }
        }

        public void RemoveResourceFromInventoryContents(ItemType itemType)
        {
            if (itemType == ItemType.None) return;
            if (--InventoryContents[itemType] == 0)
            {
                InventoryContents.Remove(itemType);
            }
        }

        /// <summary>
        /// Checks if a list of required items is present in the inventory, in at least the given quantity.
        /// </summary>
        /// <param name="requiredItems">The required items. Key is item type and value is the required amount.</param>
        /// <returns></returns>
        public bool RequireInventoryItems(Dictionary<ItemType, int> requiredItems)
        {
            foreach (KeyValuePair<ItemType, int> item in requiredItems)
            {
                if (!InventoryContents.Keys.Contains(item.Key) || InventoryContents[item.Key] < item.Value)
                    return false;
            }

            return true;
        }

        #endregion
    }
}