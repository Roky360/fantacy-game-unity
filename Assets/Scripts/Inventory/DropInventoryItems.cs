using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DropInventoryItems : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (DragNDrop.DraggedItem && DragNDrop.DraggedItem.CompareTag("Inventory Item"))
            {
                InventoryManager.Instance.RemoveFromInventory(DragNDrop.StartSlot, true);
                Destroy(DragNDrop.DraggedItem);
            }
        }
    }
}