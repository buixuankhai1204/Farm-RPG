
using UnityEngine;
[System.Serializable]

public class ItemDetails
{
    // Start is called before the first frame update
    public int itemCode;
    public ItemType itemType;
    public string itemDescription;
    public Sprite itemSprite;
    public string itemLongDescription;
    public short itemUseGridRadius;
    public float itemUserRadius;
    public bool isStartingItem;
    public bool canBePickup;
    public bool canBeDroped;
    public bool canBeEaten;
    public bool canBeCarried;

}
