using System.Collections.Generic;
using UnityEngine;
using Slot;

namespace Item
{
    public enum ItemType
    {
        None,
        WoodenLog,
        Mushroom,
        EmptyBottle,
        Bone,
        Stone,
    }

    /// <summary>
    /// Represents a collectable item in the game.
    /// Contains its type, its ground representation (3D object) and its representation in the inventory.
    /// </summary>
    [System.Serializable]
    public class ItemData
    {
        public ItemType Type;
        public GameObject GroundItem;
        public GameObject InventoryItem;

        /// <summary>
        /// All the slot types that the item can be in.
        /// </summary>
        public List<SlotType> allowedSlots;

        public ItemData(ItemType type, GameObject groundItem, GameObject inventoryItem, List<SlotType> allowedSlots)
        {
            Type = type;
            GroundItem = groundItem;
            InventoryItem = inventoryItem;
            this.allowedSlots = allowedSlots;
        }
    }
}