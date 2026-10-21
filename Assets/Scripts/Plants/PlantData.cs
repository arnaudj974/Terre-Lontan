using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/Plant Data")]
public class PlantData : ScriptableObject
{
    public string name;
    public float growthPeriod;
    public int NumberStates;
    public List<Sprite> growthState;
    public bool harvestPossible;
    public Sprite harvestSprite;
    public int growingTime;
    public int maxGrowingTime;

}
