using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
    public float timer = 0;

    public MoonRotator rotator;

    private void Start() {
        UpdateTime(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(false);
        
    }

    public void UpdateTime(bool force){
        timer += Time.deltaTime;
        if(!timePaused){
            monthTime += Time.deltaTime;
            if(monthTime >= currentSpeed){
                currentTime++;
                monthTime = 0;
                GM.I.people.ProcessAging();
                GM.I.city.UpdateCity();
                GM.I.resource.UpdateResources();
                GM.I.people.ProcessMood();
                GM.I.project.UpdateProjects();
                GM.I.eventManager.UpdateEvents();
                GM.I.ui.buildingInformation.UpdateMenuInfo();
                CheckLooseConditions();
                CheckWinConditions();
                // Decade Analytics
                // if(monthTime % 120 == 0){
                //     Analytics.CustomEvent("DecadeGame", new Dictionary<string, object>
                //     {
                //         { "day", currentTime },
                //         { "population", GM.I.people.TotalPopulation },
                //         { "needs", GM.I.people.needs },
                //         { "comfort", GM.I.people.comfort },
                //         { "culture", GM.I.people.culture },
                //         { "hope", GM.I.people.hope },
                //         { "unemployement", GM.I.people.Unemployement },
                //         { "energy", GM.I.resource.resources.Energy },
                //         { "water", GM.I.resource.resources.Water },
                //         { "material", GM.I.resource.resources.Material }
                //     });
                // }
            }
            GM.I.ui.timeKeeper.UpdateClock(currentTime, monthTime/currentSpeed);
        }
        if(force){
            GM.I.people.ProcessAging();
            GM.I.city.UpdateCity();
            GM.I.people.ProcessMood();
            GM.I.people.ProcessAging();
            GM.I.city.UpdateCity();
            GM.I.ui.resourceMeters.UpdateResources();
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

    void CheckWinConditions(){
        if(currentTime >= travelLenght){
            rotator.interactable = false;
            rotator.target.eulerAngles = Vector3.zero;
            GM.I.ui.ShowWinScreen();
            GM.I.introManager.myAnimator.enabled = true;
            GM.I.introManager.myAnimator.Play("Rocket");
            timePaused = true;

            PlayerPrefs.SetInt("NewHome", 1);
            if(GM.I.people.TotalPopulation >= 500000){
                PlayerPrefs.SetInt("BabyBoom", 1);
            }
            if(timer < 60f*45f){
                PlayerPrefs.SetInt("LightSpeed", 1);
            }
            if(GM.I.people.hope <= 0.05f){
                PlayerPrefs.SetInt("Hopeless", 1);
            }
            if(GM.I.people.holiday){
                PlayerPrefs.SetInt("Holiday", 1);
            }
            int terraIncognitCount = 0;
            foreach (BuildingSpot spot in GM.I.city.buildings)
            {
                if(spot.terraIncognita){terraIncognitCount++;}
            }
            if(terraIncognitCount == 0){
                PlayerPrefs.SetInt("TerraIncognita", 1);
            }

            Analytics.CustomEvent("WinGame", new Dictionary<string, object>
            {
                { "day", currentTime },
                { "population", GM.I.people.TotalPopulation },
                { "needs", GM.I.people.needs },
                { "comfort", GM.I.people.comfort },
                { "culture", GM.I.people.culture },
                { "hope", GM.I.people.hope },
                { "unemployement", GM.I.people.Unemployement },
                { "energy", GM.I.resource.resources.Energy },
                { "water", GM.I.resource.resources.Water },
                { "material", GM.I.resource.resources.Material }
            });
        }
    }

    void CheckLooseConditions(){
        if(GM.I.people.TotalPopulation < 1){
            GM.I.ui.ShowLooseScreen();
            timePaused = true;
            Analytics.CustomEvent("LooseGame", new Dictionary<string, object>
            {
                { "day", currentTime },
                { "population", GM.I.people.TotalPopulation },
                { "needs", GM.I.people.needs },
                { "comfort", GM.I.people.comfort },
                { "culture", GM.I.people.culture },
                { "hope", GM.I.people.hope },
                { "unemployement", GM.I.people.Unemployement },
                { "energy", GM.I.resource.resources.Energy },
                { "water", GM.I.resource.resources.Water },
                { "material", GM.I.resource.resources.Material }
            });
        }
    }

    public void Restart(){
        Analytics.CustomEvent("RestartGame", new Dictionary<string, object>
        {
            { "day", currentTime },
            { "population", GM.I.people.TotalPopulation },
            { "needs", GM.I.people.needs },
            { "comfort", GM.I.people.comfort },
            { "culture", GM.I.people.culture },
            { "hope", GM.I.people.hope },
            { "unemployement", GM.I.people.Unemployement },
            { "energy", GM.I.resource.resources.Energy },
            { "water", GM.I.resource.resources.Water },
            { "material", GM.I.resource.resources.Material }
        });
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void GoToMenu(){
        Analytics.CustomEvent("RestartGame", new Dictionary<string, object>
        {
            { "day", currentTime },
            { "population", GM.I.people.TotalPopulation },
            { "needs", GM.I.people.needs },
            { "comfort", GM.I.people.comfort },
            { "culture", GM.I.people.culture },
            { "hope", GM.I.people.hope },
            { "unemployement", GM.I.people.Unemployement },
            { "energy", GM.I.resource.resources.Energy },
            { "water", GM.I.resource.resources.Water },
            { "material", GM.I.resource.resources.Material }
        });
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
