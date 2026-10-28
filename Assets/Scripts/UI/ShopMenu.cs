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
        UpdateShopInventory();
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

    void UpdateShopInventory()
    {
        for(int i = 0; i < seedsCollection.getListItem().Count; i++)
        {
            ShopSlot currentSlot = papyInventory.GetChild(i).GetComponent<ShopSlot>();
            Item cloneItem = Instantiate(seedsCollection.getItemByIndex(i),currentSlot.transform);
            currentSlot.currentItem = cloneItem;
            currentSlot.typeOfslot = ShopSlotType.SHOP;
            currentSlot.SetItemInfo(PriceStatus.BUY);
            currentSlot.SetActivePrice(true);
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
                currentSlot.typeOfslot = ShopSlotType.PLAYER;
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
