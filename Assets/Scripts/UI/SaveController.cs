using System.Collections.Generic;
using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController InventoryController;
    private HotBarController hotbarController;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "save.json");
        InventoryController = FindFirstObjectByType<InventoryController>();
        hotbarController = FindFirstObjectByType<HotBarController>();
        Debug.Log("Chemin de la sauvegarde : " + Application.persistentDataPath);
        LoadGame();
    }

    public void Savegame()
    {
        CinemachineConfiner2D confiner = GameObject.FindFirstObjectByType<CinemachineConfiner2D>();
        string boundaryName = "";

        if (confiner != null && confiner.BoundingShape2D != null)
        {
            boundaryName = confiner.BoundingShape2D.gameObject.name;
        }

        SaveData saveData = new SaveData
        {
            playerposition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = boundaryName,
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

            // Replacer le joueur
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = saveData.playerposition;
            }

            // Gérer les limites de la caméra
            CinemachineConfiner2D confiner = GameObject.FindFirstObjectByType<CinemachineConfiner2D>();
            if (confiner != null && !string.IsNullOrEmpty(saveData.mapBoundary))
            {
                GameObject boundaryObj = GameObject.Find(saveData.mapBoundary);
                if (boundaryObj != null)
                {
                    PolygonCollider2D polyCollider = boundaryObj.GetComponent<PolygonCollider2D>();
                    if (polyCollider != null)
                    {
                        confiner.BoundingShape2D = polyCollider;
                    }
                }
            }

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
        else
        {
            Savegame();
        }
    }
}