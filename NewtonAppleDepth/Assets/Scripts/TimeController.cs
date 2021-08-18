using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeController : MonoBehaviour
{
    public static TimeController timeInstance;

    public TimeSpan timeGoing;
    
    public bool timeTag;

    private float playTime;

    private void Awake()
    {
        timeInstance = this;
    }

    private void Start()
    {
        timeTag = false;
    }

    public void BeginTimer()
    {
        timeTag = true;
        playTime = 0.0f;

        StartCoroutine(UpdatePlayTime());
    }

    public void EndTimer()
    {
        timeTag = false;
    }

    public float GetTotalPlayTimeInSeconds()
    {
        return playTime;
    }

    private IEnumerator UpdatePlayTime()
    {
        while (timeTag)
        {
            playTime += Time.deltaTime;
            timeGoing = TimeSpan.FromSeconds(playTime); 
            yield return null;
        }
    }
}
