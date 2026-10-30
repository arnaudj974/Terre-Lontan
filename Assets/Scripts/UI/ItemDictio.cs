using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictio : MonoBehaviour
{
    public List<Item> itemPrefbas;
    private Dictionary<string, Item> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<string, Item>();

        for(int i = 0; i < itemPrefbas.Count; i++)
        {
            if (itemPrefbas[i] != null)
            {
                itemPrefbas[i].ID = Guid.NewGuid().ToString();
            }
        }
        foreach(Item item in itemPrefbas)
        {
           itemDictionary[item.ID] = item;
        }
    }

    public Item GetItemPrefab(string itemID)
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
        Debug.Log("item ID in itemDictio remove :"+item.ID+" gameobject id : "+item.GetInstanceID());
        return itemPrefbas.Remove(item) && itemDictionary.Remove(item.ID);   
    }

    public bool isInList(string id)
    {   
        return itemPrefbas.Find(x => x.ID == id ) != null;
    }
    public bool Remove(string itemId)
    {
        itemDictionary.TryGetValue(itemId, out Item item);
        Debug.Log("Sell item id : "+itemId+" item in dic: "+item.ID);
        if(item == null)
        {
            return false;
        }
        else
        {   
            bool isDestroy = itemDictionary.Remove(item.ID) && itemPrefbas.Remove(item);
            if(isDestroy) Destroy(item.gameObject);
            return isDestroy;
        }
    }

    public void Add(Item item)
    {
        Debug.Log("item ID in itemDictio Add :"+item.ID+" gameobject id : "+item.GetInstanceID());
        itemPrefbas.Add(item);
        itemDictionary.Add(item.ID,item);
    }
}
