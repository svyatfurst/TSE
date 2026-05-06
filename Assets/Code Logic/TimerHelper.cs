using System;
using System.Collections;
using UnityEngine;

public class TimerHelper : MonoBehaviour
{
    public void Delay(float time, Action action)
    {
        StartCoroutine(DelayCoroutine(time, action));
    }

    private IEnumerator DelayCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}