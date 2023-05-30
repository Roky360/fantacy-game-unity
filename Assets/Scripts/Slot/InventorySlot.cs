using System;
using Inventory;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Slot
{
    public class InventorySlot : BaseSlot, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _mouseHover;

        private new void Start()
        {
            base.Start();
            
            this.slotType = SlotType.Inventory;
            _mouseHover = false;
        }

        private void Update()
        {
            HotbarManager hotbarManager = HotbarManager.Instance;
            // update selected slot with numbers hotkeys
            for (int i = 0; i < hotbarManager.numOfSlots; i++)
            {
                string keyCodeStr = $"{i + 1}";
                if (Input.GetKeyDown(keyCodeStr) && _mouseHover)
                {
                    InventoryManager.Instance.SwapSlots(this, hotbarManager.slots[i]);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseHover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseHover = false;
        }
    }
}