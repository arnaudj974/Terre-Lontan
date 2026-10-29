using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    public Item currentItem;
    public ShopSlotType typeOfslot;
    public TextMeshProUGUI price;
    public TextMeshProUGUI priceIcon;
    public Player p;
    private InventoryController inventoryController;
    private ItemDictio itemDictio;

    public UnityEvent OnChangeInventory;
    
    void Start()
    {
        itemDictio = FindFirstObjectByType<ItemDictio>();
        inventoryController = FindFirstObjectByType<InventoryController>();
        p = FindFirstObjectByType<Player>();
    }
    public void SetItemInfo(PriceStatus status)
    {
        if(currentItem != null)
        {
            switch(status){
                case PriceStatus.BUY:
                    price.SetText(currentItem.GetPrice(status).ToString());
                    break;
                case PriceStatus.SELL:
                    price.SetText(currentItem.GetPrice(status).ToString());
                    break;
            }
        } 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (typeOfslot)
        {
            case ShopSlotType.PLAYER:
                Sell();
                break;
            case ShopSlotType.SHOP:
                Buy();
                break;
        }
    }

    void Buy()
    {
        if (p.Acheter(currentItem.GetPrice(PriceStatus.BUY)))
        {
            Item newitem  = Instantiate(currentItem);
            newitem.ID = (int) DateTimeOffset.Now.ToUnixTimeSeconds()+ (int) DateTimeOffset.Now.ToUnixTimeMilliseconds();
            inventoryController.AddItem(newitem);
            OnChangeInventory?.Invoke();
            Destroy(newitem.gameObject); 
        }

    }

    void Sell()
    {
       bool removeStatus = itemDictio.Remove(currentItem.ID);
        if (removeStatus)
        {
            p.Vendre(currentItem.GetPrice(PriceStatus.SELL));
            Destroy(currentItem.gameObject);
            currentItem = null;
        }
        SetActivePrice(false);

       OnChangeInventory?.Invoke();
       
    }

    public void SetActivePrice(bool active)
    {
        price.gameObject.SetActive(active);
        priceIcon.gameObject.SetActive(active);
    }
}
