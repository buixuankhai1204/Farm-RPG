
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ItemList", menuName = "Scriptable Objects/Item/Item List")]
public class SO_ItemList : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] public List<ItemDetails> ItemDetails;
}
