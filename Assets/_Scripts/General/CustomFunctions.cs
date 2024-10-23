using UnityEngine;


public class CustomFunctions
{
    public static float EaseInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

    public static float SmoothLerp(float a, float b, float t)
    {
        t = EaseInOutSine(Mathf.Clamp(t, 0, 1));

        return a + ((b - a) * t);
    }

    /// <summary>
    /// Same as the Map function, but with input x,y,0,1,t
    /// </summary>
    /// <param name="outLow"></param>
    /// <param name="outHigh"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float MapO01(float t, float inMin, float inMax)
    {
        return (t - inMin) / (inMax - inMin);
    }
}
