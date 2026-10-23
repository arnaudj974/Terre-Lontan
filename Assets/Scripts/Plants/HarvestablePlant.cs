using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlant : MonoBehaviour
{
    public string plantName;
    [SerializeField] PlantsCollection plantsCollection;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int numberDaysWatered;
    [SerializeField] int daysPass;
    float timer;
    PlantData plantData;
    int currentState = 0;
    public bool isNight;
    public bool harvest = false;

    void Start()
    {
        SetPlantType();
    }

    void Update()
    {
        IsNight();
    }

    public void SetPlantType(string name)
    {
        plantData = plantsCollection.getPlantByName(plantName);
        SetSprite(currentState);
        isNight = false;
    }

    public List<string> GetPlantsCollectionName()
    {
        return plantsCollection.getListPLant();
    }

    public void SetPlantType()
    {
        plantData = plantsCollection.getPlantByName(plantName);
        SetSprite(currentState);
        isNight = false;
    }

    public void SetNight()
    {
        isNight = true;
    }

    public void SetSprite(int growthState)
    {
        spriteRenderer.sprite = plantData.growthState[growthState];
    }

    public void Harvest()
    {
        if(harvest && plantData.harvestPossible)
        {
            spriteRenderer.sprite = plantData.harvestSprite;
        }
    }

    public void GrowingPlant()
    { 
        currentState++;
        currentState = Mathf.Clamp(currentState, 0, plantData.GetMaxGrowthStatesForArray());
        SetSprite(currentState);
    }

    void IsNight()
    {
        if (isNight)
        {
            GrowingPlant();
            isNight = false;
        }
    }
}
