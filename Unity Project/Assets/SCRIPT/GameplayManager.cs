using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("TIME")]
    public int currentTime = 0;
    public float timeSpeed;
    public float fastTimeSpeed;
    public float currentSpeed;
    public float monthTime;
    public bool timePaused = false;

    [Header("TRAVEL")]
    public int travelLenght;

    private void Start() {
        UpdateTime(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(false);
    }

    public void UpdateTime(bool force){
        if(!timePaused){
            monthTime += Time.deltaTime;
            if(monthTime >= currentSpeed){
                currentTime++;
                monthTime = 0;
                GM.I.people.ProcessAging();
                GM.I.city.UpdateCity();
                GM.I.resource.UpdateResources();
                GM.I.people.ProcessMood();
                GM.I.ui.buildingInformation.UpdateMenuInfo();
            }
            GM.I.ui.timeKeeper.UpdateClock(currentTime, monthTime/currentSpeed);
        }
        if(force){
            GM.I.people.ProcessAging();
            GM.I.city.UpdateCity();
            GM.I.resource.UpdateResources();
            GM.I.people.ProcessMood();
            GM.I.ui.timeKeeper.UpdateClock(currentTime, monthTime/currentSpeed);
        }
    }

    public void PauseTime(bool shouldStop){
        timePaused = shouldStop;
        currentSpeed = timeSpeed;
        GM.I.ui.timeKeeper.UpdatePausedStatus();
    }
    public void FastTime(){
        PauseTime(false);
        currentSpeed = fastTimeSpeed;
        GM.I.ui.timeKeeper.UpdatePausedStatus();
    }
}
