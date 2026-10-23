using UnityEngine;
using System.IO;
using Unity.Cinemachine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "save.json");

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
            mapBoundary = boundaryName
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
        }
        else
        {
            Savegame();
        }
    }
}