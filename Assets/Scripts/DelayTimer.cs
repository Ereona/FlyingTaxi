using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


[ExecuteInEditMode]
public class DelayTimer : MonoBehaviour
{
    private float startTime;
    private float currentTime;
    [HideInInspector]
    public bool Started;
    
    public float Delay; //Delay in seconds
    [HideInInspector]
    public bool Stoped;

    public event EventHandler DrinDrinEvent;

    public DelayTimer(float delay)
    {
        Delay = delay;
        Stoped = false;
    }

    public DelayTimer()
    {
        Delay = 0.0f;
        Stoped = false;
    }

    public void PrintTime()
    {
        //Debug.Log ("Delay = " + Delay.ToString());
        //Debug.Log ("DeltaTime = " + DeltaTime.ToString());
        //Debug.Log ("StartTime = " + startTime.ToString());
        //Debug.Log ("CurrentTime = " + currentTime.ToString());
    }

    public void TimerStart()
    {
        ResetTimer();
        Started = true;
        Stoped = false;
        startTime = Time.fixedTime;
        currentTime = Time.fixedTime;
    }

    public void ResetTimer()
    {
        TimerStop();
        Stoped = false;
    }

    public void TimerStop()
    {
        Started = false;
        Stoped = true;
        startTime = 0.0f;
        currentTime = 0.0f;
    }

    public bool DrinDrin()
    {
        currentTime = Time.fixedTime;
        float DeltaTime = Mathf.Abs(startTime - currentTime);

        if (DeltaTime >= Delay && !Stoped)
        {
            TimerStop();
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (Started)
        {
            if (DrinDrin())
            {
                if (DrinDrinEvent != null)
                    DrinDrinEvent(this, new EventArgs());
                //Debug.Log("Drin-Drin");
            }
        }
    }
}