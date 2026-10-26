using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] float maxEnergy = 100f;
    public Tool tool { get; private set; } 
    public float energy { get; private set; }
    public int money;

    public UnityEvent<float> OnEnergyChange;
    public UnityEvent OnMoneyChange;
    void Awake()
    {
        energy = maxEnergy;
        if (tool == null) tool = new Tool(4); // mets les mains par defaut
    }

    void Start()
    {
        WorldTime.instance.Sunset += IsNight;
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

    public void Vendre(int amount)
    {
        money += amount;
        OnMoneyChange?.Invoke();
    }
    public bool Acheter(int amount)
    {
        if (money - amount < 0) return false;
        else
        {
            money-= amount;
            OnMoneyChange?.Invoke();
            return true;
        }
    }
    void IsNight()
    {
        energy = maxEnergy;
        OnEnergyChange?.Invoke(energy);
    }
}
