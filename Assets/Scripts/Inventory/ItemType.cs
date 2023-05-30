// using UnityEngine;
// using Item;
//
// namespace Inventory
// {
//     // public enum ItemType
//     // {
//     //     None,
//     //     WoodenLog,
//     //     Mushroom,
//     //     EmptyBottle,
//     //     Bone,
//     //     Stone,
//     // }
//
//     /// <summary>
//     /// Represents a slot in the inventory.
//     /// Contains the type of the item in the slot and the slot's game object in the UI.
//     /// </summary>
//     public class ResourceSlot
//     {
//         private ItemType _type;
//
//         public ItemType Type
//         {
//             get => _type;
//             set
//             {
//                 _type = value;
//                 SlotGameObject.GetComponent<InventoryResourceSlot>().itemType = value;
//             }
//         }
//
//         public GameObject SlotGameObject;
//         public bool IsOccupied;
//
//         public ResourceSlot(ItemType type, GameObject slotGameObject, bool isOccupied = false)
//         {
//             _type = type;
//             SlotGameObject = slotGameObject;
//             IsOccupied = isOccupied;
//         }
//
//         /// <summary>
//         /// Changes the slot's contents in the UI
//         /// </summary>
//         public void UpdateSlotContents()
//         {
//             InventoryResourceSlot slot = SlotGameObject.GetComponent<InventoryResourceSlot>();
//             slot.ChangeContents(InventoryManager.Instance.ItemTypeToItemMap[_type].InventoryItem);
//         }
//
//         public void EmptySlotContents()
//         {
//             InventoryResourceSlot slot = SlotGameObject.GetComponent<InventoryResourceSlot>();
//             slot.EmptySlot();
//         }
//     }
//
//     // /// <summary>
//     // /// Represents a collectable item in the game.
//     // /// Contains its type, its ground representation (3D object) and its representation in the inventory.
//     // /// </summary>
//     // [System.Serializable]
//     // public class Item
//     // {
//     //     public ItemType Type;
//     //     public GameObject GroundItem;
//     //     public GameObject InventoryItem;
//     //
//     //     public Item(ItemType type, GameObject groundItem, GameObject inventoryItem)
//     //     {
//     //         Type = type;
//     //         GroundItem = groundItem;
//     //         InventoryItem = inventoryItem;
//     //     }
//     // }
// }