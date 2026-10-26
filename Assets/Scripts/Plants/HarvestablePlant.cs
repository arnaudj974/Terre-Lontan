using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlant : MonoBehaviour
{
    public string plantName;
    [SerializeField] PlantsCollection plantsCollection;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int numberDaysWatered;
    [SerializeField] int daysPass;
  
    PlantData plantData;
    int currentState = -1;
    public bool isNight;
    public bool harvest { get; private set; }
    bool watered = false;

    void Start()
    {
        plantData = plantsCollection.getPlantByName(plantName);
        //SetSeedSprite();
        Planted();
        WorldTime.instance.Sunset += IsNight;
    }

    public void SetSeedSprite()
    {
        spriteRenderer.sprite = plantData.seedSprite;
    }

    void OnDisable()
    {
        WorldTime.instance.Sunset -= IsNight;
    }

    public void SetPlantType(string name)
    {   
        plantName = name;
        plantData = plantsCollection.getPlantByName(plantName);
        //Planted();
        //SetSprite(currentState);
    }

    List<string> GetPlantsNameInCollection()
    {
        return plantsCollection.getListPLant();
    }

    void SetSprite(int growthState)
    {
        spriteRenderer.sprite = plantData.growthState[growthState];
    }

    public void Watered()
    {
        watered = true;
    }

    public void Harvest()
    {
        if(harvest && plantData.harvestPossible)
        {
            spriteRenderer.sprite = plantData.harvestSprite;
        }
    }

    public void Planted()
    {
        spriteRenderer.sprite = plantData.planted;
    }

    void GrowingPlant()
    { 
        currentState++;
        currentState = Mathf.Clamp(currentState, 0, plantData.GetMaxGrowthStatesForArray());
        SetSprite(currentState);
        if(plantData.harvestPossible && plantData.GetMaxGrowthStatesForArray() == currentState)
        {
            harvest = true;
        }
    }

    public void IsNight()
    {
        if (watered)
        {
           GrowingPlant();
           watered = false;
        }
    }
}
