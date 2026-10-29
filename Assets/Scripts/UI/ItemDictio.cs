using System.Collections.Generic;
using UnityEngine;

public class ItemDictio : MonoBehaviour
{
    public List<Item> itemPrefbas;
    private Dictionary<int, Item> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<int, Item>();

        for(int i = 0; i < itemPrefbas.Count; i++)
        {
            if (itemPrefbas[i] != null)
            {
                itemPrefbas[i].ID = i + 1;
            }
        }
        foreach(Item item in itemPrefbas)
        {
           itemDictionary[item.ID] = item;
        }
    }

    public Item GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out Item prefab);
        if(prefab == null)
        {
            Debug.LogWarning($"Item with ID {itemID} not found in the dictionary.");
        }
        return prefab;
    }

    public bool Remove(Item item)
    {
        return itemPrefbas.Remove(item) && itemDictionary.Remove(item.ID);   
    }
    public bool Remove(int itemId)
    {
        itemDictionary.TryGetValue(itemId, out Item item);
        if(item == null)
        {
            return false;
        }
        else
        {
            return itemDictionary.Remove(item.ID) && itemPrefbas.Remove(item);
        }
    }

    void  Add(Item item)
    {
        itemPrefbas.Add(item);
        itemDictionary.Add(item.ID,item);
    }
}
