using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictio itemDictionary; // Référence au script ItemDictio pour accéder aux prefabs d'items

    public GameObject InventoryPanel; // Panel contenant l'inventaire
    public GameObject slotPrefab; // Prefab de la case "slot"
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictio>(); // Trouver le script ItemDictio dans la scène

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, InventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = item;
            }

        }

    }

    public bool AddItem(GameObject itemPrefabs)
    {
        foreach (Transform slotTransform in InventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.CurrentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefabs, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = newItem;
                return true;
            }
        }
        Debug.Log("inventory is full");
        return false;
    }

    //public List<inventorySaveData> GetInventoryitems()
    //{
    //    List<inventorySaveData> invData = new List<inventorySaveData>();
    //    foreach (Transform slotTransform in InventoryPanel.transform)
    //    {
    //        Slot slot = slotTransform.GetComponent<Slot>();
    //        if (slot.CurrentItem != null)
    //        {
    //            Item item = slot.CurrentItem.GetComponent<Item>();
    //            invData.Add(new inventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
    //        }

    //    }
    //    return invData;
    //}
    //public void SetInventoryItems(List<inventorySaveData> inventorySaveData)
    //{
    //    if (itemDictionary == null)
    //    {
    //        itemDictionary = FindFirstObjectByType<ItemDictio>();
    //    }
    //    //netoyer l'inventory panel pas de duplicat
    //    foreach (Transform child in InventoryPanel.transform)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //    // cree nouveau slot
    //    for (int i = 0; i < slotCount; i++)
    //    {
    //        Instantiate(slotPrefab, InventoryPanel.transform);

    //    }
    //    // populate slot avec item sauvegarde
    //    foreach (inventorySaveData data in inventorySaveData)
    //    {
    //        if (data.slotIndex < slotCount)
    //        {
    //            Slot slot = InventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
    //            GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
    //            if (itemPrefab != null)
    //            {
    //                GameObject item = Instantiate(itemPrefab, slot.transform);
    //                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    //                slot.CurrentItem = item;
    //            }
    //        }
    //    }
    //}

}
