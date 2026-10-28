using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public Item currentItem;
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

    public void SetActivePrice(bool active)
    {
        price.gameObject.SetActive(active);
        priceIcon.gameObject.SetActive(active);
    }
}
