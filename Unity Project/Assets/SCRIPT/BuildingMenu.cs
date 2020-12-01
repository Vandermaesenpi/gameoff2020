using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    public List<BuildingToggle> choices;
    public BuildingObject selectedBuilding;

    public Text title;
    public Text description;
    public Transform buildingHolder;
    public RessourceBox production;
    public RessourceBox cost;
    public RessourceBox constructionCost;
    public Text constructionTime;

    public AudioClip openSfx, closeSfx;

    public void ClicBuildingMenu(bool sfx){
        bool value = !gameObject.activeInHierarchy;
        gameObject.SetActive(value);
        GM.I.city.ShowHoloMoon(value);
        if(value){
            if(GM.I.ui.populationMenu.gameObject.activeInHierarchy){
                GM.I.ui.populationMenu.ClicPopulationMenu(false);
            }
            if(GM.I.ui.populationMenu.moodMenu.activeInHierarchy){
                GM.I.ui.populationMenu.ClicMoodMenu(false);
            }
            GM.I.city.SetBuildingSpotMode(BuildingSpotMode.Building);
        }else{
            GM.I.city.SetBuildingSpotMode(BuildingSpotMode.Normal);
        }
        foreach (BuildingToggle choice in choices)
        {
            choice.InitializeMiniature();
        }
        if(sfx){
            GM.I.sfx.Play(value? openSfx : closeSfx);
        }
    }

    private void Start() {
        SelectBuilding(true);
    }

    public void SelectBuilding(bool value){
        if(value){
            foreach (BuildingToggle choice in choices)
            {
                if(choice.isOn){
                    selectedBuilding = choice.building;
                    title.text = selectedBuilding.buildingName;
                    description.text = selectedBuilding.description;
                    foreach (Transform t in buildingHolder)
                    {
                        Destroy(t.gameObject);
                    }
                    Instantiate(selectedBuilding.prefab, buildingHolder);
                    production.UpdateRessourceBox(selectedBuilding.production.GetProduction());
                    cost.UpdateRessourceBox(selectedBuilding.production.GetCost());
                    constructionCost.UpdateRessourceBox(selectedBuilding.constructionMonthlyCost.Multiply((float)selectedBuilding.constructionTime));
                    constructionTime.text = selectedBuilding.constructionTime + " months";
                }
            }
        }
    }
}
