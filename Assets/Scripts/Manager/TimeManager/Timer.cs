using System;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI displayTime;
    public TextMeshProUGUI displayDay;

    void Start()
    {
        WorldTime.instance.TimePass += OnTimeChanged;
        WorldTime.instance.Sunrise += SetTextToDay;
        WorldTime.instance.Sunset += SetTextToNight;
    }

    void OnDisable()
    {
        WorldTime.instance.TimePass += OnTimeChanged;
        WorldTime.instance.Sunrise -= SetTextToDay;
        WorldTime.instance.Sunset -= SetTextToNight;
    }

    void SetTextToDay()
    {
        displayDay.SetText("Day");
    }

    void SetTextToNight()
    {
        displayDay.SetText("Night");
    }

    void OnTimeChanged(TimeSpan timer)
    {
        displayTime.SetText(timer.ToString(@"dd\:hh\:mm"));
        Debug.Log(timer.ToString());
    }

}
