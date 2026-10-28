using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public SeedsCollection seedsCollection;
    public Transform papyInventory;
    public Transform playerInventory;

    [SerializeField] Player player;

    private ItemDictio itemDictionary;
    private bool isPlayerInventoryNeedUpdate = true;

    void Start()
    {   
        for(int i = 0; i < seedsCollection.getListItem().Count; i++)
        {
            ShopSlot currentSlot = papyInventory.GetChild(i).GetComponent<ShopSlot>();
            Item cloneItem = Instantiate(seedsCollection.getItemByIndex(i),currentSlot.transform);
            currentSlot.currentItem = cloneItem;
            currentSlot.SetItemInfo(PriceStatus.BUY);
            currentSlot.SetActivePrice(true);
        }
        UpdatePlayerInventory();
    }

    void Update()
    {
        if (isPlayerInventoryNeedUpdate)
        {
            UpdatePlayerInventory();
            isPlayerInventoryNeedUpdate = false;
        }
    }

    void UpdatePlayerInventory()
    {
        itemDictionary = FindFirstObjectByType<ItemDictio>();
        
        for(int i = 0; i < itemDictionary.itemPrefbas.Count; i++)
        {
            ShopSlot currentSlot = playerInventory.GetChild(i).GetComponent<ShopSlot>();
            if(currentSlot.currentItem == null)
            {
                Item cloneItem = Instantiate(itemDictionary.itemPrefbas[i],currentSlot.transform);
                currentSlot.currentItem = cloneItem;
                currentSlot.SetItemInfo(PriceStatus.SELL);
                currentSlot.SetActivePrice(true);
            }

        }
    }

    public void OnChangeInventory()
    {
        isPlayerInventoryNeedUpdate = true;
    }

}
