using UnityEngine;
using System.Collections.Generic;

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


    void Update()
    {
        IsNight();
    }

    void SetPlantType( string name)
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

    void Harvest()
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

    void SetNight()
    {
        isNight = true;
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
