using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Item;
using Inventory;
using JetBrains.Annotations;

namespace Slot
{
    public enum SlotType
    {
        Inventory,
        HotBar,
    }

    public class BaseSlot : MonoBehaviour, IDropHandler
    {
        public SlotType slotType;
        public ItemType itemType = ItemType.None;
        public bool isOccupied = false;

        private GameObject _contentsObj;

        /// <summary>
        /// The game object of the 2D representation of the item.
        /// </summary>
        [CanBeNull]
        public GameObject Item =>
            _contentsObj.transform.childCount > 0 ? _contentsObj.transform.GetChild(0).gameObject : null;

        private void SetChild(GameObject child)
        {
            EmptySlot();
            child.transform.SetParent(_contentsObj.transform);
            child.transform.localPosition = new Vector2(0, 0);
        }

        /// <summary>
        /// Changes the contents of the slot.
        /// Empties the slot if it was full and sets the new object as the child.
        /// </summary>
        /// <param name="newItemType"></param>
        public void ChangeContents(ItemType newItemType)
        {
            if (newItemType == ItemType.None)
            {
                EmptySlot();
            }
            else
            {
                GameObject inventoryItem = InventoryManager.Instance.ItemTypeToItemMap[newItemType].InventoryItem;
                GameObject child = Instantiate(inventoryItem);
                SetChild(child);

                itemType = newItemType;
                isOccupied = true;
            }
        }

        protected void Start()
        {
            _contentsObj = transform.Find("Contents").gameObject;
        }

        /// <summary>
        /// Empties a slot and returns the type of the item that was in the slot.
        /// Sets the slot as not occupied and destroys the item that was in it.
        /// </summary>
        public ItemType EmptySlot()
        {
            if (isOccupied)
            {
                if (Item != null)
                    Destroy(Item);
                isOccupied = false;
                ItemType prevItemType = itemType;
                itemType = ItemType.None;
                return prevItemType;
            }

            return ItemType.None;
        }

        public void OnDrop(PointerEventData eventData)
        {
            ItemType type = DragNDrop.StartSlot.itemType;
            // if the slot is not occupied and the dropped item can be placed in this slot type
            if (!isOccupied && InventoryManager.Instance.IsItemMatchSlot(type, slotType))
            {
                Destroy(DragNDrop.DraggedItem);
                InventoryManager.Instance.SwapSlots(this, DragNDrop.StartSlot);
            }
        }
    }
}