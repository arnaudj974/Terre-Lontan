using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public Vector3 playerposition;
    public string mapBoundary;
    public List<inventorySaveData> inventorySaveData;
}
