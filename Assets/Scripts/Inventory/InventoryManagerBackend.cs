using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public partial class InventoryManager
    {
        /// <summary>
        /// ALL THE FUCKING GAME ITEMS!!
        /// </summary>
        public List<Item> GameItems;

        public Dictionary<ItemType, Item> ItemTypeToItemMap { get; } = new();

        /// <summary>
        /// Inventory resource slots contents
        /// </summary>
        public ResourceSlot[] ResourceSlots;

        public bool IsFull => FindEmptySlot() == -1;

        void InitializeItemsMap()
        {
            foreach (Item item in GameItems)
            {
                ItemTypeToItemMap.Add(item.Type, item);
            }
        }

        void InitializeResourceSlots()
        {
            for (int i = 0; i < ResourceSlots.Length; i++)
            {
                // get the current slot as a game object
                GameObject slotObj = slots.transform.GetChild(i).gameObject;
                InventoryResourceSlot resourceSlot = slotObj.GetComponent<InventoryResourceSlot>();

                ResourceSlots[i] = new ResourceSlot(ItemType.None, slotObj, isOccupied: false);
                resourceSlot.idx = i;
                resourceSlot.itemType = ItemType.None;
                resourceSlot.EmptySlot();
            }
        }

        private void FillSlot(int slotIdx, ItemType type, bool updateUI = true)
        {
            ResourceSlot slot = ResourceSlots[slotIdx];

            slot.Type = type;
            slot.IsOccupied = true;
            if (updateUI)
                slot.UpdateSlotContents();
        }

        /// <summary>
        /// Empties a slot from its contents.
        /// </summary>
        /// <param name="slotIdx"></param>
        /// <returns>The type of the item that was in the slot.</returns>
        private ItemType EmptySlot(int slotIdx)
        {
            ResourceSlot slot = ResourceSlots[slotIdx];
            ItemType type = slot.Type;

            slot.IsOccupied = false;
            slot.Type = ItemType.None;
            slot.EmptySlotContents();

            return type;
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < ResourceSlots.Length; i++)
            {
                if (!ResourceSlots[i].IsOccupied)
                    return i;
            }

            return -1;
        }

        public bool AddToInventory(ItemType itemType)
        {
            int slotIdx = FindEmptySlot();
            if (slotIdx == -1)
            {
                // TODO: inform the user that the inventory is full
                Debug.Log("Inventory full!");

                return false;
            }
            else
            {
                FillSlot(slotIdx, itemType);

                return true;
            }
        }

        public void RemoveFromInventory(int idx, bool dropItem = true)
        {
            if (idx < 0 || idx > ResourceSlots.Length)
                return;
            
            ItemType itemType = EmptySlot(idx);
            if (dropItem)
            {
                DropItemNearPlayer(itemType);
            }
        }

        /// <summary>
        /// Moves an item from one slot to another.
        /// </summary>
        public void MoveItem(int fromSlotIdx, int toSlotIdx)
        {
            ItemType itemType = EmptySlot(fromSlotIdx);
            FillSlot(toSlotIdx, itemType, false);
        }

        void DropItemNearPlayer(ItemType itemType)
        {
            ThrowGroundObject(itemType, player.transform, 100);
        }
    }
}