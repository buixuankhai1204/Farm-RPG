using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class UiInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Player player;
    private InventoryManager inventoryManager;
    private Camera mainCamera;
    private Canvas parentCanvas;
    private Transform parentItem;
    private GameObject draggedItem;

    [SerializeField] private GameObject invetoryTextBoxPrefab;

    public Image inventorySlotHighlight;
    public Image inventorySlotImage;
    public TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private UiInventoryBar inventoryBar = null;
    [SerializeField] private GameObject itemPrefab = null;

    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;
    [SerializeField] private int slotNumber = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player).GetComponent<Player>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneloadFadeInEvent += SceneLoaded;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneloadFadeInEvent -= SceneLoaded;
    }
    
    private void Start()
    {
        inventoryManager = GameObject.FindWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
        mainCamera = Camera.main;
    }

    public void DropSelectedItemAtMousePosition()
    {
        inventoryManager = GameObject.FindWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            -mainCamera.transform.position.z));
        Debug.Log(worldPosition);
        GameObject itemDraggedObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
        SpriteRenderer img = itemDraggedObject.GetComponentInChildren<SpriteRenderer>();
        img.sprite = itemDetails.itemSprite;

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
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UiInventorySlot>()
                    .slotNumber;
                inventoryManager.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemQuantity != 0)
        {
            inventoryBar.inventoryTextBoxGameOject = Instantiate(invetoryTextBoxPrefab, transform.position, quaternion.identity);
            inventoryBar.inventoryTextBoxGameOject.transform.SetParent(parentCanvas.transform, false);

            UiInventoryTextBox uiInventoryTextBox = inventoryBar.inventoryTextBoxGameOject.GetComponent<UiInventoryTextBox>();

            string itemTypeDescription = inventoryManager.GetItemTypeDescription(itemDetails.itemType);
            uiInventoryTextBox.SetTextBox(itemDetails.itemDescription, itemTypeDescription, "",
                itemDetails.itemLongDescription, "", "");
            if (inventoryBar.isInventoryBarBottom)
            {
                inventoryBar.inventoryTextBoxGameOject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameOject.transform.position = new Vector3(transform.position.x,
                    transform.position.y + 50, transform.position.z);
            }
            else
            {
                inventoryBar.inventoryTextBoxGameOject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameOject.transform.position = new Vector3(transform.position.x,
                    transform.position.y - 50, transform.position.z);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryBar.inventoryTextBoxGameOject != null)
        {
            Destroy(inventoryBar.inventoryTextBoxGameOject);
        }
    }

    public void SceneLoaded()
    {
        parentItem = GameObject.FindWithTag(Tags.ItemParentTransform).transform;
    }
}