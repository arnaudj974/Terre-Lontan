using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Plant/Plant Data")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public List<Sprite> growthState;
    public bool harvestPossible;
    public Sprite harvestSprite;
    public int growingTime;
    public int maxGrowingTime;

    public int GetMaxGrowthStates()
    {
        return growthState.Count;
    }

    public int GetMaxGrowthStatesForArray()
    {
        return growthState.Count - 1;
    }

}
