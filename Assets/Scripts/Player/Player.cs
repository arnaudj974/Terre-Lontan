using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float maxWater = 5f;
    [SerializeField] float maxEnergy = 100f;
    public Tool tool { get; private set; } 
    public float energy { get; private set; }
    public float water { get; private set; }
    public int money;
    Vector3 startPos;

    public UnityEvent<float> OnEnergyChange;
    public UnityEvent<float> OnWaterChange;
    public UnityEvent OnMoneyChange;
    void Awake()
    {
        startPos = transform.position;
        water = 0f;
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

    public void RemoweWater(float amount)
    {
        water -= amount;
        OnWaterChange?.Invoke(water);
    }

    public void AddWater()
    {
        if (energy != 0 && water != maxWater)
        {
            RemoveEnergy(5f);
            water = maxWater;
            SetTool(5);
            OnWaterChange?.Invoke(water);
        }
    }

    public void Vendre(int amount)
    {
        Debug.Log("Vendre");
        money += amount;
        OnMoneyChange?.Invoke();
    }
    public bool Acheter(int amount)
    {
        Debug.Log("Acheter");
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
        transform.position = startPos;
        SetTool(4);
        energy = maxEnergy;
        OnEnergyChange?.Invoke(energy);
        RemoweWater(water);
    }
}
