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

    float Timer(bool reset = false)
    {
        if(reset){timer = 0;}
        timer += Time.deltaTime * (1 / Time.timeScale);
        return  timer;
    }

    public void Harvest()
    {
        if(harvest && plantData.harvestPossible)
        {
            spriteRenderer.sprite = plantData.harvestSprite;
        }
    }

    void GrowingPlant()
    { 
        currentState++;
        currentState = Mathf.Clamp(currentState, 0, plantData.GetMaxGrowthStatesForArray());
        SetSprite(currentState);
    }

    public void SetNight()
    {
        isNight = true;
    }

    public void IsNight()
    {
        GrowingPlant();
    }
}
