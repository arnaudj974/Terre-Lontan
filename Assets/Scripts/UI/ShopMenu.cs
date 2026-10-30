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

}
