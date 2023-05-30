using Item;
using Slot;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject DraggedItem;
    public static BaseSlot StartSlot;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _startPosition;
    private Transform _startParent;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = .6f;
        // ray cast will ignore the item itself.
        _canvasGroup.blocksRaycasts = false;
        _startPosition = transform.position;
        _startParent = transform.parent;

        // currently dragged item
        DraggedItem = gameObject;
        StartSlot = GetComponentInParent<BaseSlot>();

        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedItem = null;
        StartSlot = null;

        // if the item was not dropped on a slot - return it to its original slot
        if (transform.parent == _startParent || transform.parent == transform.root)
        {
            transform.position = _startPosition;
            transform.SetParent(_startParent);
        }

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}