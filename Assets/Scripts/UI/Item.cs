using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public int ID;
    public Image image;
    public string nomDePlante;
    public PlantsCollection plantCollection;
    private PlantData plantData;
    public SpriteRenderer sprite;
    public bool isFruit;
    private void Awake()
    {
        plantData = plantCollection.getPlantByName(nomDePlante);
        if (isFruit)
        {
            Fruit();
        }
        else
        {
            sprite.sprite = plantData.seedSprite;
            image.sprite = plantData.seedSprite;
        }
    }
    public virtual void UseItem()
    {
        Debug.Log($"Using item {ID} {nomDePlante}");
    }
    public string PlantName()
    {
        return plantData.referenceName;
    }

    public void SetPlantType(string name)
    {
        nomDePlante = name;
        plantData = plantCollection.getPlantByName(name);
    }

    public int GetPrice(PriceStatus status)
    {
        switch (status)
        {
            case PriceStatus.BUY:
                return plantData.price;
            case PriceStatus.SELL:
                return plantData.price / 2;
            default:
                return 0;
        }
    }

    public void Fruit()
    {
        sprite.sprite = plantData.harvestCrop;
        image.sprite = plantData.harvestCrop;
    }
}

