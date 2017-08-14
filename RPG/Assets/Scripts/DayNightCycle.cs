using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour 
{

    //The ratio real hour to game hour.
    public const float REAL_TO_GAME_HOUR = 24 / 0.166f;
    //The brightest time of the day.
    public const float DAY_PEAK_TIME = 14;
    //The darkest time of the night.
    public const float NIGHT_PEAK_TIME = 0;
    //The time the night starts.
    public const float NIGHT_START = 20;
    //The time the day starts.
    public const float DAY_START = 7;
    //The min value of the sun's intensity.
    private const float MIN_LIGHT = 0.1f;
    //The sun's intensity value when day and night meet up.
    private const float MED_LIGHT = 0.3f;
    //The max value of the sun's intensity.
    private const float MAX_LIGHT = 1f;
    //The time the time updates in seconds.
    private float UPDATE_TIME = 0.033f;

    private static Light sun;

	void Start () 
	{
        sun = GetComponent<Light>();
        StartCoroutine(DayNightCoroutine());
	}
	
	IEnumerator DayNightCoroutine()
    {
        while(true)
        {
            AddTime(UPDATE_TIME);
            yield return new WaitForSeconds(UPDATE_TIME);
        }
    }

    /// <summary>
    /// Adds time to the game timer.
    /// </summary>
    /// <param name="amount">The amount of real time(seconds) added to the game timer.</param>
    public static void AddTime(float amount)
    {
        DataController.data.currentSave.currentTime += amount * REAL_TO_GAME_HOUR / 3600;

        if(DataController.data.currentSave.currentTime >= 24)
        {
            DataController.data.currentSave.currentTime = 0;
        }

        //First part of the day.
        if (DataController.data.currentSave.currentTime >= DAY_START && DataController.data.currentSave.currentTime < DAY_PEAK_TIME)         
        {
            sun.intensity = Map(DataController.data.currentSave.currentTime, DAY_START, DAY_PEAK_TIME, MED_LIGHT, MAX_LIGHT);
        }
        //Second part of the day.
        else if (DataController.data.currentSave.currentTime >= DAY_PEAK_TIME && DataController.data.currentSave.currentTime < NIGHT_START) 
        {
            sun.intensity = Map(DataController.data.currentSave.currentTime, DAY_PEAK_TIME, NIGHT_START, MAX_LIGHT, MED_LIGHT);
        }
        //Second part of the night
        else if (DataController.data.currentSave.currentTime >= NIGHT_PEAK_TIME && DataController.data.currentSave.currentTime < DAY_START)
        {
            sun.intensity = Map(DataController.data.currentSave.currentTime, NIGHT_PEAK_TIME, DAY_START, MIN_LIGHT, MED_LIGHT);
        }
        //First part of the night
        else
        {
            sun.intensity = Map(DataController.data.currentSave.currentTime, NIGHT_START, NIGHT_PEAK_TIME, MED_LIGHT, MIN_LIGHT);
        }
    }

    public static string SetTime(float amount)
    {
        if(amount < 0 && amount > 24)
        {
            return "Amount must be between 0 and 24.";
        }
        else
        {
            DataController.data.currentSave.currentTime = amount;
            return "Time was set to " + amount.ToString() + ".";
        }
    }

    private static float Map(float num, float min1, float max1, float min2, float max2)
    {
        return min2 + (max2 - min2) * ((num - min1) / (max1 - min1));
    }
}
