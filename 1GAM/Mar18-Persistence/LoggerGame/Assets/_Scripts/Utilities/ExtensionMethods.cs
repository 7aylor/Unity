using UnityEngine;
using System;

public static class ExtensionMethods
{
    public static Transform SearchForChild(this Transform target, string name)
    {
        if (target.name == name) return target;

        for (int i = 0; i < target.childCount; ++i)
        {
            var result = SearchForChild(target.GetChild(i), name);

            if (result != null) return result;
        }

        return null;
    }

    public static T GetComponentInAncestry<T>(this Transform transform)
    {
        var grandParent = transform.parent.parent;
        var c = transform.GetComponentInParent<T>();

        if (c != null)
            return c;

        if (c == null & grandParent != null)
            return transform.GetComponentInAncestry<T>();

        return c;
    }
}

public static class Mathc
{
    public static float Percent(this float f1, float f2)
    {
        return f1 / f2 * 100;
    }

    public static float PercentBetween(this float val, float min, float max)
    {
        return ((val - min) * 100) / (max - min);
    }

    public static float ValueFromPercent(this float percent, float num)
    {
        return (percent * num) / 100;
    }

    public static float ValueFromPercentBetween(this float percent, float min, float max)
    {
        return (percent * (max - min) / 100) + min;
    }
}

//example -- yield return new WaitWhile(() => bulletsFired < 4);
class WaitWhile : CustomYieldInstruction
{
    private Func<bool> predicate;

    public override bool keepWaiting
    {
        get
        {
            return predicate();
        }
    }

    public WaitWhile(Func<bool> predicate) { this.predicate = predicate; }
}