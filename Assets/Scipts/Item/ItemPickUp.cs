
using System;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    private InventoryManager inventoryManager;
    private void Awake()
    { 
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        
        if (item != null)
        {
            ItemDetails itemDetails = inventoryManager.getItemDetails(item.ItemCode);
            if (itemDetails.canBePickup == true)
            {
                inventoryManager.AddItem(InventoryLocation.player, item, other.gameObject);
            }
        }
    }
}
