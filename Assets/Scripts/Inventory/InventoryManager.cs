using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }

    [SerializeField] GameObject inventoryScreen;
    [SerializeField] KeyCode trigger = KeyCode.E;

    private bool _isOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(trigger))
        {
            if (_isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isOpen)
        {
            CloseInventory();
        }
    }

    void OpenInventory()
    {
        inventoryScreen.SetActive(true);
        _isOpen = true;
    }
    
    void CloseInventory()
    {
        inventoryScreen.SetActive(false);
        _isOpen = false;
    }
}