using UnityEngine;
using UnityEngine.Events;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    private HotBarController hotBarController;
    private ItemDictio itemDictionaryRef;

    public UnityEvent OnChangeInventory; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hotBarController = FindFirstObjectByType<HotBarController>();
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
                
                Item cloneItem = hotBarController.AddItem(item); //ajout hotbar 1st
                if(cloneItem==null) cloneItem= inventoryController.AddItem(item); // Ajouter Item dans l'inventaire
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