using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    public List<BuildingToggle> choices;
    public BuildingObject selectedBuilding;
    public void ClicBuildingMenu(){
        bool value = !gameObject.activeInHierarchy;
        GM.I.ui.buildingInformation.ShowBuildingInfo(null);
        gameObject.SetActive(value);
        GM.I.city.ShowHoloMoon(value);
        if(value){
            if(GM.I.ui.populationMenu.gameObject.activeInHierarchy){
                GM.I.ui.populationMenu.ClicPopulationMenu();
            }
            GM.I.city.SetBuildingSpotMode(BuildingSpotMode.Building);
        }else{
            GM.I.city.UnselectAll();
        }
        foreach (BuildingToggle choice in choices)
        {
            choice.buildingImage.sprite = choice.building.sprite;
            choice.buildingName.text = choice.building.buildingName;
            choice.buildingDescription.text = choice.building.description;
        }
    }

    public void SelectBuilding(bool value){
        if(value){
            foreach (BuildingToggle choice in choices)
            {
                if(choice.isOn){
                    selectedBuilding = choice.building;
                }
            }
        }
    }
}
