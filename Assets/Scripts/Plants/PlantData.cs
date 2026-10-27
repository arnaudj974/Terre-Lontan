using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Plant/Plant Data")]
public class PlantData : ScriptableObject
{
    public string displayName;
    public string referenceName;
    public List<Sprite> growthState;
    public bool harvestPossible;
    public Sprite harvestSprite;
    public Sprite seedSprite;
    public Sprite harvestCrop;
    public Sprite planted;
    public int price;

    public int GetMaxGrowthStates()
    {
        return growthState.Count;
    }

    public int GetMaxGrowthStatesForArray()
    {
        return growthState.Count - 1;
    }

}
