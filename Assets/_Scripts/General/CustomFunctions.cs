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
}
