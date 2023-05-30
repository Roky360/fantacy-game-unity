using Item;
using Slot;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HotbarManager : MonoBehaviour
{
    private static HotbarManager _instance;
    public static HotbarManager Instance => _instance;

    [SerializeField] private GameObject hotbarUI;
    public int numOfSlots;
    public int selectedSlotIdx;
    public HotbarSlot[] slots;

    [SerializeField] private Color activeBorderColor = new Color(103, 202, 229);
    [SerializeField] private Color regularBorderColor = new Color(255, 255, 255);
    [SerializeField] private Color activeNumberColor = new Color(103, 202, 229);
    [SerializeField] private Color regularNumberColor = new Color(255, 255, 255);

    private void InitializeSlots()
    {
        slots = new HotbarSlot[numOfSlots];

        for (int i = 0; i < numOfSlots; i++)
        {
            // get the current slot as a game object
            GameObject slotObj = hotbarUI.transform.GetChild(i).gameObject;
            HotbarSlot slot = slotObj.GetComponent<HotbarSlot>();

            slot.slotType = SlotType.HotBar;
            slot.EmptySlot();
            slots[i] = slot;
        }
    }

    /// <summary>
    /// Called when left click is triggered.
    /// If there is a weapon in the active hotbar slot, use it.
    /// </summary>
    private void AttackWithActiveSlot()
    {
    }

    /// <summary>
    /// Called when right click is triggered.
    /// If there is a consumable item in the active hotbar slot, consume it.
    /// </summary>
    private void ConsumeWithActiveSlot()
    {
    }

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
    }

    // Start is called before the first frame update
    void Start()
    {
        numOfSlots = hotbarUI.transform.childCount;
        selectedSlotIdx = 0;
        InitializeSlots();

        ChangeSlotColor(selectedSlotIdx, activeBorderColor, activeNumberColor);
    }

    // Update is called once per frame
    void Update()
    {
        // update selected slot with numbers hotkeys
        for (int i = 0; i < numOfSlots; i++)
        {
            string keyCodeStr = $"{i + 1}";
            if (Input.GetKeyDown(keyCodeStr) && i != selectedSlotIdx)
            {
                ChangeSlotColor(selectedSlotIdx, regularBorderColor, regularNumberColor);
                selectedSlotIdx = i;
                ChangeSlotColor(selectedSlotIdx, activeBorderColor, activeNumberColor);
            }
        }

        // react to items in hotbar with left & right clicks
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            AttackWithActiveSlot();
        }
        else if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            ConsumeWithActiveSlot();
        }
    }

    private void ChangeSlotColor(int slotIdx, Color borderColor, Color numberColor)
    {
        slots[slotIdx].transform.Find("Border").GetComponent<Image>().color = borderColor;
        slots[slotIdx].transform.Find("Slot Number").GetComponent<TextMeshProUGUI>().color = numberColor;
    }
}