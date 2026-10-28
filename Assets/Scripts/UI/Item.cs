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
        sprite.sprite = plantData.seedSprite;
        image.sprite = plantData.seedSprite;
    }
    public virtual void UseItem()
    {
        Debug.Log("Using item" + name);
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

    public void Fruit()
    {
        isFruit = true;
        sprite.sprite = plantData.harvestCrop;
        image.sprite = plantData.harvestCrop;
    }
}

