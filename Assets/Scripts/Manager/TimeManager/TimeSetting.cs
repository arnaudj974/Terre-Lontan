using UnityEngine;

[CreateAssetMenu(fileName = "TimeSetting", menuName = "Time/Time Setting")]
public class TimeSetting : ScriptableObject
{
    public float startHour = 12;
    public float sunriseHour = 6;
    public float sunsetHour = 18;
}
