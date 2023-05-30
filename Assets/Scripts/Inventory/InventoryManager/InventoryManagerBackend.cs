using System.Collections.Generic;
using UnityEngine;
using Item;
using Slot;
using UnityEngine.Serialization;

namespace Inventory
{
    public partial class InventoryManager
    {
        /// <summary>
        /// Inventory resource slots contents
        /// </summary>
        // public ResourceSlot[] ResourceSlots;
        [FormerlySerializedAs("ResourceSlots")]
        public BaseSlot[] slots;

        public bool IsFull => FindEmptySlotIdx() == -1;

        /// <summary>
        /// Initialises the inventory slots.
        /// * Right now the inventory starts empty, later it will load the contents from a local save.
        /// </summary>
        void InitializeSlots()
        {
            InventoryContents = new();

            for (int i = 0; i < slots.Length; i++)
            {
                // get the current slot as a game object
                GameObject slotObj = slotsParentGameObject.transform.GetChild(i).gameObject;
                BaseSlot resourceSlot = slotObj.GetComponent<BaseSlot>();

                resourceSlot.slotType = SlotType.Inventory;
                resourceSlot.EmptySlot();
                slots[i] = resourceSlot;
            }
        }

        private int FindEmptySlotIdx()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].isOccupied)
                    return i;
            }

            return -1;
        }

        private BaseSlot FindEmptySlot()
        {
            foreach (BaseSlot s in slots)
            {
                if (!s.isOccupied)
                    return s;
            }

            return null;
        }


        private bool FillSlot(int slotIdx, ItemType itemType)
        {
            if (IsItemMatchSlot(itemType, slots[slotIdx].slotType))
            {
                slots[slotIdx].ChangeContents(itemType);
                return true;
            }

            return false;
        }

        private bool FillSlot(BaseSlot slot, ItemType itemType)
        {
            if (IsItemMatchSlot(itemType, slot.slotType))
            {
                slot.ChangeContents(itemType);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Empties a slot from its contents.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>The type of the item that was in the slot.</returns>
        private ItemType EmptySlot(BaseSlot slot)
        {
            return slot.EmptySlot();
        }

        /// <summary>
        /// Adds an item to the inventory, if there is a space for it.
        /// </summary>
        /// <param name="itemType">The type of the item to add.</param>
        /// <returns>If there was enough space for the item.</returns>
        public bool AddToInventory(ItemType itemType)
        {
            BaseSlot slot = FindEmptySlot();
            if (slot is null)
            {
                // TODO: inform the user that the inventory is full
                Debug.Log("Inventory full!");

                return false;
            }
            else
            {
                FillSlot(slot, itemType);
                AddResourceToInventoryContents(itemType);

                return true;
            }
        }

        public void RemoveFromInventory(BaseSlot slot, bool dropItem = true)
        {
            ItemType itemType = EmptySlot(slot);
            
            if (slot.slotType == SlotType.Inventory)
                RemoveResourceFromInventoryContents(itemType);

            if (dropItem)
            {
                DropItemNearPlayer(itemType);
            }
        }

        /// <summary>
        /// Swaps the contents of two slots.
        /// Checks if each of the two items can be in the oppose slot.
        /// </summary>
        /// <param name="slotA"></param>
        /// <param name="slotB"></param>
        /// <returns>If the swap was successful.</returns>
        public bool SwapSlots(BaseSlot slotA, BaseSlot slotB)
        {
            if (IsItemMatchSlot(slotA.itemType, slotB.slotType) && IsItemMatchSlot(slotB.itemType, slotA.slotType))
            {
                ItemType itemA = EmptySlot(slotA);
                ItemType itemB = EmptySlot(slotB);
                FillSlot(slotA, itemB);
                FillSlot(slotB, itemA);

                if (slotA.slotType == SlotType.Inventory && slotB.slotType != SlotType.Inventory)
                {
                    RemoveResourceFromInventoryContents(slotB.itemType);
                    AddResourceToInventoryContents(slotA.itemType);
                } else if (slotB.slotType == SlotType.Inventory && slotA.slotType != SlotType.Inventory)
                {
                    RemoveResourceFromInventoryContents(slotA.itemType);
                    AddResourceToInventoryContents(slotB.itemType);
                }

                return true;
            }

            return false;
        }

        void DropItemNearPlayer(ItemType itemType)
        {
            ThrowGroundObject(itemType, player.transform, 100);
        }
    }
}