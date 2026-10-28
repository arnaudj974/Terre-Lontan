using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    public Item currentItem;
    public ShopSlotType typeOfslot;
    public TextMeshProUGUI price;
    public TextMeshProUGUI priceIcon;

    
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
        Debug.Log("Clicked: " + currentItem?.nomDePlante);
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
        ItemDictio itemDictio = FindFirstObjectByType<ItemDictio>();
    }

    void Sell()
    {
        ItemDictio itemDictio = FindFirstObjectByType<ItemDictio>();
    }

    public void SetActivePrice(bool active)
    {
        price.gameObject.SetActive(active);
        priceIcon.gameObject.SetActive(active);
    }
}
