using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;
    public float extendingDuration = 0.12f;
    public float delayingDuration = 0.12f;

    private bool curState = false;
    private bool lastState = false;

    public MyTimer extTimer = new MyTimer();
    public MyTimer delayTimer = new MyTimer();


    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();
        curState = input;

        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        IsDelaying = false;
        IsExtending = false;

        if (lastState != curState)
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }

        IsExtending = (extTimer.state == MyTimer.STATE.RUN) ? true : false;
        IsDelaying = (delayTimer.state == MyTimer.STATE.RUN) ? true : false;
        lastState = curState;
    }

    private void StartTimer(MyTimer timer, float _duration)
    {
        timer.duration = _duration;
        timer.Go();
    }
}
