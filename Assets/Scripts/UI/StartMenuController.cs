using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        // Load the main game scene
        SceneManager.LoadScene("TestSeb");
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
