using System;
using System.Collections;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    public static WorldTime instance;
    [SerializeField] private TimeSetting timeSetting;
    [SerializeField] private float dayLenght; // in seconds

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    private TimeSpan currentTime;
    private float minuteLength => dayLenght / 1440;
    private bool isDay;

    public delegate void DelegateTimePass(TimeSpan currentTime);
    public DelegateTimePass TimePass;

    public delegate void DelegateSunrise();
    public DelegateSunrise Sunrise;

     public delegate void DelegateSunset();
    public DelegateSunrise Sunset;


    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Two or more WorldTimeManager in the scene! Disabling new one", this);
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        sunriseTime = TimeSpan.FromHours(timeSetting.sunriseHour);
        sunsetTime = TimeSpan.FromHours(timeSetting.sunsetHour);
    }

    void Start()
    {
        currentTime = DateTime.Now.TimeOfDay;
        StartCoroutine(AddMinute());
    }

    void Update()
    {
        if(isDay == isDayTime()) return;
        if(isDayTime()){Sunrise?.Invoke();}else{ Sunset?.Invoke();}
        isDay = isDayTime();
    }

    bool isDayTime() => currentTime.Hours > sunriseTime.Hours && currentTime.Hours < sunsetTime.Hours;
    IEnumerator AddMinute()
    {
        currentTime+= TimeSpan.FromMinutes(1);
        TimePass?.Invoke(currentTime);
        yield return new WaitForSeconds(minuteLength);
        StartCoroutine(AddMinute());
    }
}
