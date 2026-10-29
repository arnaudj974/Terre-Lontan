
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController InventoryController;
    private HotBarController hotbarController;

    void Start()
    {
        WorldTime.instance.Sunset += Savegame;
        saveLocation = Path.Combine(Application.persistentDataPath, "save.json");
        InventoryController = FindFirstObjectByType<InventoryController>();
        hotbarController = FindFirstObjectByType<HotBarController>();
        Debug.Log("Chemin de la sauvegarde : " + Application.persistentDataPath);
        LoadGame();
    }
    
    public void Savegame()
    {
        SaveData saveData = new SaveData
        {
            inventorySaveData = InventoryController != null ? InventoryController.GetInventoryitems() : null,
            hotbarSaveData = hotbarController != null ? hotbarController.GetHotbarItems() : null
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            // Recup l'inventaire si les données existent
            if (InventoryController != null && saveData.inventorySaveData != null)
            {
                InventoryController.SetInventoryItems(saveData.inventorySaveData);
            }

            // Recup la hotbar si les données existent
            if (hotbarController != null && saveData.hotbarSaveData != null)
            {
                hotbarController.SetHotbarItems(saveData.hotbarSaveData);
            }
        }
    } 
}