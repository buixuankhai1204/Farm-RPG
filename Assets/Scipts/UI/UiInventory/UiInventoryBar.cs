using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private InventoryManager inventoryManager;
    private RectTransform _rectTransform;
    [HideInInspector] public GameObject inventoryTextBoxGameOject;
    private bool _isInventoryBarBottom = true;
    public  bool isInventoryBarBottom
    {
        get => _isInventoryBarBottom;
        set => _isInventoryBarBottom = value;
    }

    [SerializeField] private Sprite blank16x16Sprite = null;
    [SerializeField] private UiInventorySlot[] inventorySlot = null;
    public GameObject inventoryBarDraggedItem;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player).GetComponent<Player>();
        inventoryManager = GameObject.FindWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        SwitchInventoryPositionBar();
    }

    private void OnEnable()
    {
        EventHandler.InventoryUpdateEvent += InventoryUpdate;
    }

    private void OnDisable()
    {
        EventHandler.InventoryUpdateEvent -= InventoryUpdate;
    }
    public void SwitchInventoryPositionBar()
    {
        Vector3 playerViewportPosition = player.GetPlayerViewportPosition();
        if (playerViewportPosition.y > 0.3f && isInventoryBarBottom == false)
        {
            _rectTransform.pivot = new Vector2(0.5f, 0f);
            _rectTransform.anchorMin = new Vector2(0.5f, 0f);
            _rectTransform.anchorMax = new Vector2(0.5f, 0f);
            _rectTransform.anchoredPosition = new Vector2(0f, 2.5f);
            isInventoryBarBottom = true;
        }
        else if(playerViewportPosition.y <= 0.3f && isInventoryBarBottom == true )
        {
            _rectTransform.pivot = new Vector2(0.5f, 1f);
            _rectTransform.anchorMin = new Vector2(0.5f, 1f);
            _rectTransform.anchorMax = new Vector2(0.5f, 1f);
            _rectTransform.anchoredPosition = new Vector2(0f, -2.5f);
            isInventoryBarBottom = false;
        }
    }

    private void ClearInventorySlots()
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].inventorySlotImage.sprite = blank16x16Sprite;
            inventorySlot[i].TextMeshProUGUI.text = "";
            inventorySlot[i].itemQuantity = 0;
            inventorySlot[i].itemDetails = null;
        }
    }
    public void InventoryUpdate(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (inventoryLocation == InventoryLocation.player)
        {
            ClearInventorySlots();

            if (inventorySlot.Length > 0 && inventoryList.Count > 0)
            {
             
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i].ItemCode;

                        ItemDetails itemDetails = inventoryManager.getItemDetails(itemCode);

                        if (itemDetails != null)
                        {
                            inventorySlot[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                            inventorySlot[i].TextMeshProUGUI.text = inventoryList[i].ItemQuantity.ToString();
                            inventorySlot[i].itemDetails = itemDetails;
                            inventorySlot[i].itemQuantity = inventoryList[i].ItemQuantity;
                        }
                    }
                }   
            }
            
        }
    }
}
