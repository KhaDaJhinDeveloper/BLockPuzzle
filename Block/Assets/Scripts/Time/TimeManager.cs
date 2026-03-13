using UnityEngine;

public static class TimeManager
{
    public static void TimePause()
    {
        Time.timeScale = 0f;
    }
    public static void TimeResume()
    {
        Time.timeScale = 1f;
    }
    public static void TimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
