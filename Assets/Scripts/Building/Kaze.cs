using UnityEngine;
using UnityEngine.UI;

public class Kaze : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    void Start()
    {
        SetCanvas(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetCanvas(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetCanvas(false);
        }
    }

    public void SetCanvas(bool etat)
    {
        canvas.enabled = etat;
    }
}
