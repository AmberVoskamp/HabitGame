using System;

/// <summary>
/// A helperclass for all the math methods
/// </summary>

public class MathHelper
{
    //Is between bound1 and bound2
    public static bool IsBetween(double testValue, double bound1, double bound2)
    {
        return testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2);
    }

    //Is between bound1 and bound2
    public static bool IsBetween(float testValue, float bound1, float bound2)
    {
        return testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2);
    }
}
