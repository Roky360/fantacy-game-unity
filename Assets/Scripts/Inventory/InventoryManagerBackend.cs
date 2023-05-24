using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public partial class InventoryManager
    {
        public InvItem[] Items;
        public bool isFull;

        void InitializeResourceItems()
        {
            for (int i = 0; i < slots.transform.childCount; i++)
            {
                // get the current slot as a game object
                GameObject child = slots.transform.GetChild(i).gameObject;

                InventoryResourceSlot slot = child.GetComponent<InventoryResourceSlot>();
                if (slot)
                {
                    Items[i] = new InvItem(slot.itemType, child);
                }
            }
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    return i;
            }

            return -1;
        }

        public bool AddToInventory(InvItem item)
        {
            int slotIdx = FindEmptySlot();
            if (slotIdx == -1)
            {
                // TODO: inform the user that the inventory is full
                
                return false;
            }
            else
            {
                Items[slotIdx] = item;
                
                return true;
            }
        }

        public GameObject RemoveFromInventory(int idx)
        {
            GameObject obj = Items[idx].GameObject;
            Items[idx] = null;
            return obj;
        }

        void DropItemNearPlayer(InvItemType itemType)
        {
            // TODO
        }
    }
}