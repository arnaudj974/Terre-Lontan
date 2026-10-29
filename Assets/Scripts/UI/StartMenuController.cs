using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        // Load the main game scene
        SceneManager.LoadScene("Game");
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        // Exit the application
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }
}
