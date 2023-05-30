using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Item;
using Slot;

namespace Inventory
{
    public partial class InventoryManager : MonoBehaviour
    {
        private static InventoryManager _instance;

        public static InventoryManager Instance => _instance;

        [SerializeField] GameObject inventoryScreen;
        [SerializeField] GameObject slotsParentGameObject;
        [SerializeField] GameObject hotbarParentGameObject;
        [SerializeField] KeyCode trigger = KeyCode.E;

        [SerializeField] GameObject player;

        private CanvasGroup _canvasGroup;
        private bool _isOpen;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }

            InitializeItemsMap();
            slots = new BaseSlot[slotsParentGameObject.transform.childCount];
            InitializeSlots();
        }

        // Start is called before the first frame update
        void Start()
        {
            _canvasGroup = inventoryScreen.GetComponent<CanvasGroup>();
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
            // inventoryScreen.SetActive(true);
            _canvasGroup.alpha = 1f;
            _isOpen = true;
        }

        void CloseInventory()
        {
            // inventoryScreen.SetActive(false);
            _canvasGroup.alpha = 0f;
            _isOpen = false;
        }

        public void ThrowGroundObject(ItemType type, Transform sourceTransform, float throwForce = 150)
        {
            // Instantiate the object to throw
            Rigidbody thrownObject =
                Instantiate(ItemTypeToItemMap[type].GroundItem, sourceTransform.position, Quaternion.identity)
                    .GetComponent<Rigidbody>();

            // Calculate a random direction around the parent object
            Quaternion randomRotation = Random.rotation;

            // Set the position of the thrown object slightly above the parent object
            Vector3 throwPosition = sourceTransform.position + Vector3.up;

            // Set the position and rotation of the thrown object
            thrownObject.transform.rotation = randomRotation;
            thrownObject.transform.position = throwPosition + thrownObject.transform.forward;

            // Apply force to the object in the forward direction of its local space
            thrownObject.AddForce(thrownObject.transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}