
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemCode;
    private SpriteRenderer spriteRenderer;
    private InventoryManager inventoryManager;
    
    public int ItemCode { get  { return itemCode; } set  { itemCode = value; } }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        inventoryManager = GameObject.FindWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (itemCode != null)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCodeParam)
    {
        if (itemCodeParam != 0)
        {
            ItemDetails itemDetails;
            itemDetails = inventoryManager.getItemDetails(itemCode);
            spriteRenderer.sprite = itemDetails.itemSprite;
            if (itemDetails.itemType == ItemType.Reapable_tool)
            {
                spriteRenderer.AddComponent<ItemNudge>();
            }
        }
    }

    
}
