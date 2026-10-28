using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedsCollection", menuName = "Collection/Seeds Collection")]
public class SeedsCollection : ScriptableObject
{
    [SerializeField] public string[] itemName;
    [SerializeField] public Item[] items;
    
    public Item getItemByName(string name)
    {
        int index = Array.IndexOf(itemName,name);
        return items[index];
    }

    public Item getItemByIndex(int index)
    {
        return items[index];
    }

    public List<string> getListItem()
    {
        return itemName.ToList();
    }
}
