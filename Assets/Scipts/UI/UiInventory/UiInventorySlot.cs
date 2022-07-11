
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UiInventorySlot : MonoBehaviour , IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private Player player;
    private InventoryManager inventoryManager;
    private Camera mainCamera;
    private Transform parentItem;
    private GameObject draggedItem;
    
    public Image inventorySlotHighlight;
    public Image inventorySlotImage;
    public TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private UiInventoryBar inventoryBar = null;
    [SerializeField] private GameObject itemPrefab = null;

    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player).GetComponent<Player>();
        
    }

    private void Start()
    {
        inventoryManager = GameObject.FindWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
        mainCamera = Camera.main;
        parentItem = GameObject.FindWithTag(Tags.ItemParentTransform).transform;
    }

    public void DropSelectedItemAtMousePosition()
    {
        Debug.Log(itemDetails.itemCode);
        inventoryManager = GameObject.FindWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        GameObject itemDraggedObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
        Item item = itemDraggedObject.GetComponent<Item>();
        inventoryManager.RemoveItem(InventoryLocation.player, itemDetails.itemCode);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {
            player.DisablePlayerInputAndResetMovement();
            draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventorySlotImage.sprite;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            Destroy(draggedItem);

            if (eventData.pointerCurrentRaycast.gameObject != null &&
                eventData.pointerCurrentRaycast.gameObject.GetComponent<UiInventorySlot>() != null)
            {
                
            }
            else
            {
                if (itemDetails.canBeDroped)
                {
                    DropSelectedItemAtMousePosition();
                }
                
            }

            player.EnablePlayerInput();
        }
    }
}
