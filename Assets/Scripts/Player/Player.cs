using UnityEngine;

public class Player : MonoBehaviour
{
    public Tool tool { get; private set; } 
    void Awake()
    {
        if (tool == null) tool = new Tool(4); // mets les mains par defaut
    }


    void Update()
    {
        
    }

    public void SetTool(int ind)
    {
        if (ind > 0 && ind < 6)  tool = new Tool(ind); 
        else Debug.LogWarning("index Outil non valide");
    }
}
