using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private Gradient gradient;
    [SerializeField] private TimeSetting timeSetting;


    void OnDisable()
    {
        WorldTime.instance.TimePass -= OnWorldTimeChanged;
    }
    void Start()
    {
         WorldTime.instance.TimePass += OnWorldTimeChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWorldTimeChanged(TimeSpan timer)
    {
        light.color = gradient.Evaluate(PercentOfDay(timer));
    }

   float PercentOfDay(TimeSpan timer)
    {
        return (float) timer.TotalMinutes % 1440 / 1440;
    }
}
