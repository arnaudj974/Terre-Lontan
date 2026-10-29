using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    //public Vector3 playerposition;
    public string mapBoundary;
    public List<inventorySaveData> inventorySaveData;
    public List<inventorySaveData> hotbarSaveData;
}
