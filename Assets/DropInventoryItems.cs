using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropInventoryItems : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (DragNDrop.DraggedItem && DragNDrop.DraggedItem.CompareTag("Inventory Item"))
        {
            InventoryManager.Instance.RemoveFromInventory(DragNDrop.StartSlotIdx, true);
            Destroy(DragNDrop.DraggedItem);
        }
    }
}