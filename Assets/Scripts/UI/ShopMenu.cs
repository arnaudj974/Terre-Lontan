using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public SeedsCollection seedsCollection;
    public Transform papyInventory;
    public Transform playerInventory;

    [SerializeField] Player player;

    private ItemDictio itemDictionary;

    void Start()
    {   
        UpdateShopPapy();        
    }

    void Update()
    {
        itemDictionary = FindAnyObjectByType<ItemDictio>();
        if(itemDictionary != null)
        {
            UpdatePlayerShop();
        }

    }

    void UpdateShopPapy()
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

    void UpdatePlayerShop()
    {
        // int i = 0;
        // foreach(Transform slot in playerInventory.transform)
        // {   
        //     if(itemDictionary.itemPrefbas.Count == 0) return;
        //     ShopSlot currentSlot = slot.GetComponent<ShopSlot>();
        //     Debug.Log("shop menu update player inv slot item :"+currentSlot.currentItem);
        //     if(i < itemDictionary.itemPrefbas.Count && currentSlot.currentItem == null )
        //     {
        //         Item cloneItem = Instantiate(itemDictionary.itemPrefbas[i],currentSlot.transform);
        //         currentSlot.currentItem = cloneItem;
        //         currentSlot.typeOfslot = ShopSlotType.PLAYER;
        //         currentSlot.SetItemInfo(PriceStatus.SELL);
        //         currentSlot.SetActivePrice(true);
        //         i++;
        //     }
        //     else if(i > itemDictionary.itemPrefbas.Count)
        //     {
        //         currentSlot.currentItem = null;
        //         currentSlot.typeOfslot = ShopSlotType.NULL;
        //         currentSlot.SetActivePrice(false);
        //     }

        // }    
    }
}
