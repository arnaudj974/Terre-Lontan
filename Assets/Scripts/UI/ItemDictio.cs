using System.Collections.Generic;
using UnityEngine;

public class ItemDictio : MonoBehaviour
{
    public List<Item> itemPrefbas;
    private Dictionary<int, GameObject> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();

        for(int i = 0; i < itemPrefbas.Count; i++)
        {
            if (itemPrefbas[i] != null)
            {
                itemPrefbas[i].ID = i + 1;
            }
        }
        foreach(Item item in itemPrefbas)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if(prefab == null)
        {
     Debug.LogWarning($"Item with ID {itemID} not found in the dictionary.");
        }
        return prefab;
    }
}
