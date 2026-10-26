using System;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    TextMeshProUGUI displayTime;

    void Start()
    {
        displayTime = GetComponent<TextMeshProUGUI>();
        WorldTime.instance.TimePass += OnTimeChanged;
    }

    void OnDisable()
    {
        WorldTime.instance.TimePass += OnTimeChanged;
    }


    void OnTimeChanged(TimeSpan timer)
    {
        displayTime.SetText(timer.ToString(@"hh\:mm"));
    }

}
