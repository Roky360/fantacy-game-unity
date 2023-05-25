using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] public bool playerInRange;
    [SerializeField] public GameObject objectToThrow;
    public float throwForce = 150;

    void ThrowWood()
    {
        InventoryManager.Instance.ThrowGroundObject(ItemType.WoodenLog, transform, throwForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            ThrowWood();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}