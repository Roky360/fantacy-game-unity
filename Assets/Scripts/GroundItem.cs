using Inventory;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public ItemType type;

    /// <summary>
    /// Time before despawn, in seconds
    /// </summary>
    [SerializeField] float timeToLive = 3 * 60;

    public KeyCode pickupKey = KeyCode.F;

    /// <summary>
    /// If the player is in range, the item can be picked
    /// </summary>
    public bool pickable;

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }

        // detect pickup
        if (Input.GetKeyDown(pickupKey) && pickable)
        {
            bool insertionSuccessful = InventoryManager.Instance.AddToInventory(type);
            if (insertionSuccessful)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickable = false;
        }
    }
}