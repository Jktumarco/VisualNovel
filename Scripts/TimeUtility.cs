using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TimeUtility : MonoBehaviour
{
    public static TimeUtility Instance;

    private void OnDestroy()
    {
        Instance = null;
    }

    public void Awake()
    {
        TimeUtility.Instance = this;
    }

    public void Start()
    {
        Application.targetFrameRate = 60;
        TimeUtility.Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static Timer Delay(float time, Action onTimerComplete)
    {
        Timer timer = new Timer();
        timer.SetCoroutine(timer.TimerProcess(time, onTimerComplete));

        TimeUtility.Instance.StartCoroutine(timer.GetTimer());

        return timer;
    }

    public static Timer LateInterval(float time, Action onInterval)
    {
        Timer timer = new Timer();
        timer.SetCoroutine(timer.LateIntervalProcess(time, onInterval));

        TimeUtility.Instance.StartCoroutine(timer.GetTimer());

        return timer;
    }

    public static Timer Interval(float time, Action onInterval, bool onStart = false)
    {
        Timer timer = new Timer();
        timer.SetCoroutine(timer.IntervalProcess(time, (i) =>
        {
            onInterval.Invoke();
        }, onStart));

        TimeUtility.Instance.StartCoroutine(timer.GetTimer());

        return timer;
    }

    public static Timer Interval(float time, Action<int> onInterval, bool onStart = false)
    {
        Timer timer = new Timer();
        timer.SetCoroutine(timer.IntervalProcess(time, onInterval, onStart));

        TimeUtility.Instance.StartCoroutine(timer.GetTimer());

        return timer;
    }

    public static Timer Interval(float time, int iterations, Action<int> onInterval, bool onStart = false)
    {
        Timer timer = new Timer();
        timer.SetCoroutine(timer.IntervalProcess(time, iterations, onInterval, onStart));

        TimeUtility.Instance.StartCoroutine(timer.GetTimer());

        return timer;
    }

    public static Timer WaitUntil(Func<bool> condition, Action onUntil)
    {
        Timer timer = new Timer();
        timer.SetCoroutine(timer.WaitUntilProcess(condition, onUntil));

        TimeUtility.Instance.StartCoroutine(timer.GetTimer());

        return timer;
    }

    public static void StopAllDelays()
    {
        TimeUtility.Instance.StopAllCoroutines();
    }
}

public class Timer
{
    public IEnumerator timer;

    public void SetCoroutine(IEnumerator coroutine)
    {
        this.timer = coroutine;
    }

    public IEnumerator GetTimer() { return this.timer; }

    public IEnumerator TimerProcess(float time, Action onTimerComplete)
    {
        DateTime startProcessTime = DateTime.Now;

        OnPause(() =>
        {
            Stop();
            float nextIterationTime = time - (float)(DateTime.Now - startProcessTime).TotalSeconds;

            onPauseResume = () =>
            {
                TimeUtility.Delay(nextIterationTime, onTimerComplete);
            };
        });

        yield return new WaitForSeconds(time);
        onTimerComplete.Invoke();
    }

    public IEnumerator LateIntervalProcess(float time, Action onInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            onInterval.Invoke();
        }
    }

    public IEnumerator IntervalProcess(float time, Action<int> onInterval, bool onStart)
    {
        for (int i = 0; true; i++)
        {
            if (i == 0)
            {
                if (!onStart) onInterval.Invoke(++i);
                else
                {
                    onInterval.Invoke(i);
                    continue;
                }
            }

            yield return new WaitForSeconds(time);
            onInterval.Invoke(i);
        }
    }

    public IEnumerator IntervalProcess(float time, int iterations, Action<int> onInterval, bool onStart)
    {
        for (int i = 0; i <= iterations; i++)
        {
            if (i == 0)
            {
                if (!onStart) onInterval.Invoke(++i);
                else
                {
                    onInterval.Invoke(i);
                    continue;
                }
            }

            yield return new WaitForSeconds(time);
            onInterval.Invoke(i);
        }
    }

    public IEnumerator WaitUntilProcess(Func<bool> condition, Action onUntil)
    {

        yield return new WaitUntil(() => condition.Invoke());
        onUntil.Invoke();
    }

    private Action onPause = null;
    private Action onPauseResume = null;

    public void OnPause(Action onPause)
    {
        this.onPause = onPause;
    }

    public Timer Pause()
    {
        onPause?.Invoke();

        return this;
    }

    public Timer Resume()
    {
        onPauseResume?.Invoke();
        return this;
    }

    public void Stop()
    {
        TimeUtility.Instance.StopCoroutine(GetTimer());
    }

    public void Kill()
    {
        if (GetTimer() == null) return;
        TimeUtility.Instance?.StopCoroutine(GetTimer());
        onPause = null;
        onPauseResume = null;
    }
}