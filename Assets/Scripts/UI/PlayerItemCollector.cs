using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    private ItemDictio itemDictionaryRef;

    public UnityEvent OnChangeInventory; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
        itemDictionaryRef = FindFirstObjectByType<ItemDictio>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                // Ajouter Item dans l'inventaire
                Item cloneItem = inventoryController.AddItem(item);

                if (cloneItem != null)
                {
                    OnChangeInventory?.Invoke();
                    itemDictionaryRef.itemPrefbas.Add(cloneItem);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}