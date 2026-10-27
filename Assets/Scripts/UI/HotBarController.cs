using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotBarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 10; // 1 - 0 du clavier

    private ItemDictio itemDictionary;
    private Key[] hotbarKeys;

    private void Awake()
    {
        itemDictionary = FindFirstObjectByType<ItemDictio>();

        hotbarKeys = new Key[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }
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
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }
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
                    GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                    if (itemPrefab != null)
                    {
                        GameObject item = Instantiate(itemPrefab, slot.transform);
                        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        slot.CurrentItem = item;
                    }
                }
            }
        }
    }
}