using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HotBarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 10; // 1 - 0 du clavier

    private ItemDictio itemDictionary;
    private Key[] hotbarKeys;

    public UnityEvent<string> OnNameFound;

    private void Awake()
    {
        itemDictionary = FindFirstObjectByType<ItemDictio>();
    }

    private void Start()
    {
        if (hotbarPanel.transform.childCount == 0)
        {
            for (int i = 0; i < slotCount; i++)
            {
                Instantiate(slotPrefab, hotbarPanel.transform);
            }
        }
    }

    void Update()
    {
        if (FindFirstItem() != -1) OnNameFound?.Invoke(hotbarPanel.transform.GetChild(FindFirstItem()).GetComponent<Slot>().CurrentItem.GetComponent<Item>().UseItem()); //envoyer le nom de la plante en 1ere pos en mains
    }

    void UseItemInSlot(int index)
    {
        if (index < hotbarPanel.transform.childCount)
        {
            Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
            if (slot != null && slot.CurrentItem != null)
            {
                Item item = slot.CurrentItem.GetComponent<Item>();
                if (item != null)
                {
                    item.UseItem();
                }
            }
        }
    }

    public int FindFirstItem()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (hotbarPanel.transform.GetChild(i).GetComponent<Slot>().CurrentItem != null && !hotbarPanel.transform.GetChild(i).GetComponent<Slot>().CurrentItem.GetComponent<Item>().isFruit)
            {
                return i;
            }
        }
        return -1;
    }
    public void DeleteFirstSlotItem()
    {
        if (FindFirstItem() != -1)
        {
            Destroy(hotbarPanel.transform.GetChild(FindFirstItem()).GetComponent<Slot>().CurrentItem.gameObject);
        }
    }
#nullable enable    
    public Item? AddItem(Item itemPrefabs)
    {
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.CurrentItem == null)
            {
                Item newItem = Instantiate(itemPrefabs, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = newItem;
                return newItem;
            }
        }
        Debug.Log("inventory is full");
        return null;
    }
    public List<inventorySaveData> GetHotbarItems()
    {
        List<inventorySaveData> hotbarData = new List<inventorySaveData>();
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.CurrentItem != null)
            {
                Item item = slot.CurrentItem.GetComponent<Item>();
                if (item != null)
                {
                    hotbarData.Add(new inventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
                }
            }
        }
        return hotbarData;
    }

    public void SetHotbarItems(List<inventorySaveData> inventorySaveData)
    {
        for (int i = hotbarPanel.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(hotbarPanel.transform.GetChild(i).gameObject);
        }

        // Créer les nouveaux slots de base
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform);
        }

        // Si on a des données de sauvegarde, on utilise les slots correspondants
        if (inventorySaveData != null)
        {
            foreach (inventorySaveData data in inventorySaveData)
            {
                if (data.slotIndex < slotCount)
                {
                    Slot slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                    Item itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                    if (itemPrefab != null)
                    {
                        Item item = Instantiate(itemPrefab, slot.transform);
                        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        slot.CurrentItem = item;
                    }
                }
            }
        }
    }
}