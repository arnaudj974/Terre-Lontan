using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantsCollection", menuName = "Plant/Plants Collection")]
public class PlantsCollection :  ScriptableObject
{
    [SerializeField] public string[] plantsName;
    [SerializeField] public PlantData[] plantsData;
    
    public PlantData getPlantByName(string name)
    {
        int index = Array.IndexOf(plantsName,name);
        return plantsData[index];
    }

    public List<string> getListPLant()
    {
        return plantsName.ToList();
    }
}
