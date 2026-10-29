using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictio itemDictionary; // Référence au script ItemDictio pour accéder aux prefabs d'items

    public GameObject InventoryPanel; // Panel contenant l'inventaire
    public GameObject slotPrefab; // Prefab de la case "slot"
    public int slotCount;
    public GameObject[] itemPrefabs;
    public Item itemPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictio>(); // Trouver le script ItemDictio dans la scène

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, InventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                Item item = Instantiate(itemPrefabs[i], slot.transform).GetComponent<Item>();
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = item;
            }

        }

    }

    void Update()
    {
        foreach( Item item in itemDictionary.itemPrefbas)
        {
            if(!itemDictionary.isInList(item.ID)) DeleteClone(item);
        }
    }

    void DeleteClone(Item item)
    {
        foreach (Transform slotTransform in InventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot.CurrentItem.ID == item.ID)
            {
                Item destroyItem = slot.CurrentItem;
                slot.CurrentItem = null;
                Destroy(destroyItem.gameObject);
            }
        }
    }

    #nullable enable
    public Item? AddItem(Item itemPrefabs)
    {
        foreach (Transform slotTransform in InventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.CurrentItem == null)
            {
                Item newItem = Instantiate(itemPrefabs, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = newItem;
                Debug.Log("item ID in inventoryController addItem :"+newItem.ID+" gameobject id : "+newItem.GetInstanceID());
                itemDictionary.Add(newItem);
                return newItem;
            }
        }
        Debug.Log("inventory is full");
        return null;
    }


    public List<inventorySaveData> GetInventoryitems()
    {
        List<inventorySaveData> invData = new List<inventorySaveData>();
        foreach (Transform slotTransform in InventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.CurrentItem != null)
            {
                Item item = slot.CurrentItem.GetComponent<Item>();
                invData.Add(new inventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex(),nomPlante=item.nomDePlante,isFruit=item.isFruit });
            }
            }
        return invData;
    }
    public void SetInventoryItems(List<inventorySaveData> inventorySaveData)
    {
        if (itemDictionary == null)
        {
            itemDictionary = FindFirstObjectByType<ItemDictio>();
        }
        //netoyer l'inventory panel pas de duplicat
        foreach (Transform child in InventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        // cree nouveau slot
       /* for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, InventoryPanel.transform);
        }*/
        // populate slot avec item sauvegarde
        foreach (inventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = InventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                if (itemPrefab != null)
                {
                    Item clone = itemPrefab;
                    clone.nomDePlante = data.nomPlante;
                    clone.ID = data.itemID;
                    clone.isFruit = data.isFruit;

                    Item item = Instantiate(clone, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.CurrentItem = item;
                    itemDictionary.itemPrefbas.Add(item);
                }
            }
        }
    }

}
