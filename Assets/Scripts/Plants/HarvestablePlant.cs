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
    public bool harvest = false;
    bool watered = false;

    void Start()
    {
        plantData = plantsCollection.getPlantByName(plantName);
        SetSeedSprite();
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
        SetSprite(currentState);
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
