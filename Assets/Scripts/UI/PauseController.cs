using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject menuCanvas;

    void Update()
    {
        if (menuCanvas != null)
        {
            // Met le jeu en pause
            Time.timeScale = menuCanvas.activeSelf ? 0f : 1f;
        }
    }

    void OnDisable()
    {
        // Pemet le temps de jeu
        Time.timeScale = 1f;
    }
}