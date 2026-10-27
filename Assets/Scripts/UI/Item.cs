using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public int ID;
    public Image image;
    public string NomDePlante;
    public PlantsCollection plantcollection;
    private PlantData PlantData;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        PlantData = plantcollection.getPlantByName(NomDePlante);
        sprite.sprite = PlantData.seedSprite;
        image.sprite = PlantData.seedSprite;
    }
    public virtual void UseItem()
    {
        Debug.Log("Using item" + name);
    }
    public string PlantName()
    {
        return PlantData.referenceName;
    }
}

