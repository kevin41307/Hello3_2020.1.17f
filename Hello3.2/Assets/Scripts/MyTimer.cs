using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer 
{
    public enum STATE
    {
        IDLE, RUN, FINISHED
    }
    public STATE state = STATE.IDLE;

    public float duration = 1f;
    public float elapsedTime = 0f;

    public void Tick()
    {
        if( state == STATE.IDLE)
        {
            
        }
        else if( state == STATE.RUN)
        {
            elapsedTime += Time.deltaTime;
            if( elapsedTime > duration)
            {
                state = STATE.FINISHED;
            }
        }
        else if( state == STATE.FINISHED)
        {

        }
        else
        {
            Debug.Log("mytimer error!!");
        }

    }
    public void Go()
    {
        elapsedTime = 0;
        state = STATE.RUN;
    }


}
