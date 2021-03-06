
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    public List<InventoryItem>[] inventoryLists;

    [HideInInspector] public int[] inventoryListsCapacityIntArray;

    [SerializeField] private SO_ItemList itemList;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateInventoryLists();
        CreateItemDetailDictionary();
    }

    private void CreateInventoryLists()
    {
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        for (int i = 0; i < inventoryLists.Length; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        inventoryListsCapacityIntArray = new int[(int)InventoryLocation.count];
        inventoryListsCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;

    }
    // Update is called once per frame
    private void CreateItemDetailDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails itemDetails  in itemList.ItemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);
        }
    }

    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        AddItem(inventoryLocation, item);
        Destroy(gameObjectToDelete);
    }
    
    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, item, itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, item);
        }
        
        EventHandler.CallInventoryUpdateEvent(inventoryLocation, inventoryList);
    }

    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)InventoryLocation.player];
        int itemPosition = FindItemInInventory(InventoryLocation.player, itemCode);
        if (itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        
        EventHandler.CallInventoryUpdateEvent(inventoryLocation, inventoryList);
            
    }

    public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].ItemCode == itemCode)
            {
                return i;
            }
        }

        return -1;
    }

    public void AddItemAtPosition(List<InventoryItem> inventoryList, Item item)
    {
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.ItemCode = item.ItemCode;
        inventoryItem.ItemQuantity = 1;
        inventoryList.Add(inventoryItem);
        DebugPrintInventoryList(inventoryList);
    }

    public void AddItemAtPosition(List<InventoryItem> inventoryList, Item item, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();
        
            int quantity = inventoryList[position].ItemQuantity + 1;
            inventoryItem.ItemQuantity = quantity;
            inventoryItem.ItemCode = item.ItemCode;
            inventoryList[position] = inventoryItem;
            DebugPrintInventoryList(inventoryList);
    }

    public void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemcode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();
        int quantity = inventoryList[position].ItemQuantity - 1;
        if (quantity > 0)
        {
            inventoryItem.ItemQuantity = quantity;
            inventoryItem.ItemCode = itemcode;
            inventoryList[position] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(position);
        }
    }

    public void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem)
    {
        if (fromItem < inventoryLists[(int)InventoryLocation.player].Count &&
            toItem < inventoryLists[(int)InventoryLocation.player].Count && fromItem != toItem && fromItem >= 0 &&
            toItem >= 0)
        {
            InventoryItem fromInventoryItem = inventoryLists[(int)inventoryLocation][fromItem];
            InventoryItem toInventoryItem = inventoryLists[(int)inventoryLocation][toItem];

            inventoryLists[(int)inventoryLocation][toItem] = fromInventoryItem;
            inventoryLists[(int)inventoryLocation][fromItem] = toInventoryItem;

            EventHandler.CallInventoryUpdateEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
        }
    }

    public string GetItemTypeDescription(ItemType itemType)
    {
        string itemTypeDescription;
        switch (itemType)
        {
            case ItemType.Breaking_tool:
                itemTypeDescription = Settings.BreakingTool;
                break;
            case ItemType.Chopping_tool:
                itemTypeDescription = Settings.ChoppingTool;
                break;
            case ItemType.Reaping_tool:
                itemTypeDescription = Settings.ReapingTool;
                break;
            case ItemType.Watering_tool:
                itemTypeDescription = Settings.WateringTool;
                break;
            case ItemType.Collecting_tool:
                itemTypeDescription = Settings.CollectingTool;
                break;
                default:
                    itemTypeDescription = itemType.ToString();
                    break;
        }

        return itemTypeDescription;
    }
    public ItemDetails getItemDetails(int itemCode)
    {
        // Debug.Log(itemCode);
        ItemDetails itemDetails;
        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }

    public void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        foreach ( InventoryItem inventoryItem in inventoryList)
        {
            Debug.Log(getItemDetails(inventoryItem.ItemCode).itemDescription +  inventoryItem.ItemQuantity);
        }
        Debug.Log("*****************************************************************");
    }
}
