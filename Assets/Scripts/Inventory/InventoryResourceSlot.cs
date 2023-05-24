using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryResourceSlot : MonoBehaviour, IDropHandler
{
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

    public InvItemType itemType;

    public void OnDrop(PointerEventData eventData)
    {
        if (!Item)
        {
            DragNDrop.DraggedItem.transform.SetParent(transform);
            DragNDrop.DraggedItem.transform.localPosition = new Vector2(0, 0);
        }
    }
}