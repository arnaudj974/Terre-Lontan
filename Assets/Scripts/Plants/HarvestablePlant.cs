using UnityEngine;

public class HarvestablePlant : MonoBehaviour
{
    [SerializeField] PlantData plantData;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool isNight;
    [SerializeField] int numberDaysWatered;
    [SerializeField] int daysPass;
    float timer;
    int currentState = 0;


    void Start()
    {
        SetSprite(currentState);
        isNight = false;
    }

    void Update()
    {
        Timer();
        GrowingPlant();
        
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

    void GrowingPlant()
    {
       if( GrowthCondition())
        {   
            currentState++;
            currentState =Mathf.Clamp(currentState, 0, plantData.NumberStates-1);
            Timer(true);
            SetSprite(currentState);
        }
    }

    bool GrowthCondition()
    {
        if(numberDaysWatered == plantData.growingTime)
        {
            return true;
        }else if(daysPass == plantData.maxGrowingTime)
        {
            return true;
        }
        return false;
    }
}
