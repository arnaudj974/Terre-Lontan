using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] float maxEnergy = 100f;
    public Tool tool { get; private set; } 
    public float energy { get; private set; }

    public UnityEvent<float> OnEnergyChange;
    void Awake()
    {
        energy = maxEnergy;
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

    public void RemoveEnergy(float amount)
    {
        energy -= amount;
        OnEnergyChange?.Invoke(energy);
    }
}
