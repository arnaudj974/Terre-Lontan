using UnityEngine;
using System.IO;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private Vector3 playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // placer la sauvegarde dans le dossier persistentDataPath
        saveLocation = Path.Combine(Application.persistentDataPath, "save.json");
    }
    public void Savegame()
    {
        SaveData saveData = new SaveData();
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }
}
