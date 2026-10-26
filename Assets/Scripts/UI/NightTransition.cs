using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class NightTransition : MonoBehaviour
{
    Image screen;
    public TextMeshProUGUI text;
    void Start()
    {
        screen = GetComponent<Image>();
        WorldTime.instance.Sunset += NightsTransition;   
    }

    void OnDisable()
    {
        WorldTime.instance.Sunset -= NightsTransition;
    }

    void NightsTransition()
    {
        gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
