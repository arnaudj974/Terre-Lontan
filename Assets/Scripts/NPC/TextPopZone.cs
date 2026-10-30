using TMPro;
using UnityEngine;

public class TextPopZone : MonoBehaviour
{
    public TextMeshProUGUI textPopUp;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) textPopUp.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) textPopUp.gameObject.SetActive(false);
    }
}
