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
        GM.I.city.ShowBuildingPoints(value);
        foreach (BuildingToggle choice in choices)
        {
            choice.buildingImage.sprite = choice.building.sprite;
            choice.buildingName.text = choice.building.buildingName;
            choice.buildingDescription.text = choice.building.description;
            choice.isOn = false;
        }
        selectedBuilding = null;
    }

    public void SelectBuilding(bool value){
        if(value){
            GM.I.city.ShowBuildingPoints(false);
            GM.I.city.ShowBuildingSpots(true);
            foreach (BuildingToggle choice in choices)
            {
                if(choice.isOn){
                    selectedBuilding = choice.building;
                }
            }
        }else if(gameObject.activeInHierarchy){
            GM.I.city.ShowBuildingPoints(true);
            selectedBuilding = null;
        }else{
            GM.I.city.ShowBuildingSpots(false);
        }
    }
}
