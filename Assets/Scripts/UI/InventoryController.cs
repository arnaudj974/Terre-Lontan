using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject InventoryPanel; // Panel contenant l'inventaire
    public GameObject slotPrefab; // Prefab de la case "slot"
    public int slotCount;
    public GameObject[] itemPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        for(int i = 0; i < slotCount; i++)
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

}
