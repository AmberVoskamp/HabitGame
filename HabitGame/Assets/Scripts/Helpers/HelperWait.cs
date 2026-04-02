using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// A helperclass for waiting methods
/// </summary>

public class HelperWait
{
    //Coroutine that will invoke an action after [wait] seconds
    public static IEnumerator ActionAfterWait(float wait, Action action)
    {
        yield return new WaitForSeconds(wait);
        action.Invoke();
    }
}
