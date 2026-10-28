using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    private ItemDictio itemDictionaryRef;            

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
                 inventoryController.AddItem(collision.gameObject);

                //if (itemAdded)
                {
                    itemDictionaryRef.itemPrefbas.Add(item);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}