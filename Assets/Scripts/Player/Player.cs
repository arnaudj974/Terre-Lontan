using UnityEngine;

public class Player : MonoBehaviour
{
    public int tool { get; private set; } // 1=pioche, 2=hache, 3=houe, 4=mains, 5=arros
    void Awake()
    {
        if (tool == 0) tool = 4; // mets les mains par defaut
    }


    void Update()
    {
        
    }

    public void SetTool(int ind)
    {
        if (ind > 0 && ind < 6)  tool = ind; 
        else Debug.LogWarning("index Outil non valide");
    }
}
