using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChecker : MonoBehaviour
{
    public List<Event> events;
    public Vector2 thresholdRange;
    [HideInInspector]
    public float threshold;
    public float resetValue;
    public Vector2Int resetTimeRange;
    [HideInInspector]
    public int resetTime;
    public bool triggered;
    int lastTime;
    private void Start() {
        Reset();
    }
    public void TriggerEvent(){
        lastTime = GM.I.gameplay.currentTime;

        Event randomEvent = events[Random.Range(0,events.Count)];
        GM.I.ui.warningMenu.InitEventMenu(randomEvent);
        if(GM.I.ui.buildingMenu.gameObject.activeInHierarchy){
            GM.I.ui.buildingMenu.ClicBuildingMenu();
        }if(GM.I.ui.populationMenu.gameObject.activeInHierarchy){
            GM.I.ui.populationMenu.ClicPopulationMenu();
        }
        if(GM.I.ui.populationMenu.moodMenu.activeInHierarchy){
            GM.I.ui.populationMenu.ClicMoodMenu();
        }
        GM.I.ui.buildingInformation.ShowBuildingInfo(null);
        triggered = true;
        GM.I.gameplay.PauseTime(true);
    }

    public virtual void Check(){
        if(GM.I.gameplay.currentTime >= lastTime+resetTime && triggered){
            Reset();
        }
    }

    public void Reset(){
        triggered = false;
        lastTime = 0;
        threshold = Random.Range(thresholdRange.x, thresholdRange.y);
        resetTime = Random.Range(resetTimeRange.x, resetTimeRange.y);
    }
}
