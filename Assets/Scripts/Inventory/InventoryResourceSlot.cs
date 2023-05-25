using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryResourceSlot : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// Slot index in ResourceSlots array in InventoryManager
    /// </summary>
    public int idx;

    public ItemType itemType;

    [SerializeField]
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    private void SetChild(GameObject child)
    {
        child.transform.SetParent(transform);
        child.transform.localPosition = new Vector2(0, 0);
    }

    public void ChangeContents(GameObject invItemObj)
    {
        GameObject child = Instantiate(invItemObj);
        SetChild(child);
    }

    public void EmptySlot()
    {
        GameObject item = Item;
        if (item)
            Destroy(item);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!Item)
        {
            SetChild(DragNDrop.DraggedItem);
            InventoryManager.Instance.MoveItem(DragNDrop.StartSlotIdx, idx);
        }
    }
}