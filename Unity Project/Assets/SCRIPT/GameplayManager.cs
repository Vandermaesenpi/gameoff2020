using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("TIME")]
    public int currentTime = 0;
    public float timeSpeed;
    public float monthTime;
    public bool timePaused = false;

    [Header("TRAVEL")]
    public int travelLenght;

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    public void UpdateTime(){
        if(!timePaused){
            monthTime += Time.deltaTime;
            if(monthTime >= timeSpeed){
                currentTime++;
                monthTime = 0;
            }
            GM.I.ui.timeKeeper.UpdateClock(currentTime, monthTime/timeSpeed);
        }
    }

    public void PauseTime(bool shouldStop){
        timePaused = shouldStop;
        GM.I.ui.timeKeeper.UpdatePausedStatus();
    }
}

public enum GameplayMode{
    Idle,
    Building,
    Info,
    Warning
}
