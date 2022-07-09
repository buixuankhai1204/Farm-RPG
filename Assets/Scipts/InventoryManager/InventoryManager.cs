
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    [SerializeField] private SO_ItemList itemList;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateItemDetailDictionary();
    }

    // Update is called once per frame
    private void CreateItemDetailDictionary()
    {
        
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails itemDetails in itemList.ItemDetails)
        {

            Debug.LogError("itemCode: " + itemDetails.itemCode + "- itemDescription: " + itemDetails.itemDescription);
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails); 
        }
    }

    public ItemDetails getItemDetails(int itemCode)
    {
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
}
