using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    #nullable enable
    public Item? currentItem ;
    public int childId;
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

    void Update()
    {
        if(typeOfslot == ShopSlotType.SHOP) return;
        if(transform.childCount ==2 ) SetActivePrice(false);
        if(typeOfslot == ShopSlotType.PLAYER && currentItem != null)
        {
            if(childId < itemDictio.itemPrefbas.Count)
            {
                if(currentItem.ID != itemDictio.itemPrefbas[childId].ID)
                {   
                    CleanChild();
                    currentItem = Instantiate(itemDictio.itemPrefbas[childId], this.transform);
                    price.text = currentItem.GetPrice(PriceStatus.SELL).ToString();
                    SetActivePrice(true);
                }
            }
            else
            {
                Destroy(currentItem.gameObject);
                currentItem = null;
                CleanChild();
                SetActivePrice(false);
            }
        }
        else if(typeOfslot == ShopSlotType.PLAYER && currentItem == null)
        {
            if(childId < itemDictio.itemPrefbas.Count)
            {
                currentItem = Instantiate(itemDictio.itemPrefbas[childId], this.transform);
                price.text = currentItem.GetPrice(PriceStatus.SELL).ToString();
                SetActivePrice(true);
            }
        }
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
        if(currentItem == null) return;
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
            newitem.ID = Guid.NewGuid().ToString();
            inventoryController.AddItem(newitem);
            OnChangeInventory?.Invoke();
            Destroy(newitem.gameObject); 
        }

    }

    void CleanChild()
    {
        int i = transform.childCount;
        while (i > 2)
        {
            Destroy(transform.GetChild(i-1).gameObject);
            i--;
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
            SetActivePrice(false);
        }

       OnChangeInventory?.Invoke();
    }

    public void SetActivePrice(bool active)
    {
        price.gameObject.SetActive(active);
        priceIcon.gameObject.SetActive(active);
    }
}
