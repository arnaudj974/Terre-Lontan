using UnityEngine;

public class Puit : MonoBehaviour
{
    AudioSource audioS;
    void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!audioS.isPlaying) audioS.Play();
            collision.GetComponent<Player>().AddWater();
        }
    }
}
