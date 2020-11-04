using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public List<BuildingSpot> buildings;

    public void ShowBuildingSpots(bool onOff){
        foreach (BuildingSpot building in buildings)
        {
            building.selectionRing.SetActive(false);
            if(onOff){
                if(building.currentBuilding == null){
                    building.positionRing.SetActive(true);
                }
            }else{
                building.positionRing.SetActive(false);
            }
        }
    }

    public void ShowBuildingPoints(bool onOff){
        ShowBuildingSpots(false);
        foreach (BuildingSpot building in buildings)
        {
            building.selectionRing.SetActive(false);
            if(onOff){
                if(building.currentBuilding == null){
                    building.positionPoint.SetActive(true);
                }
            }else{
                building.positionPoint.SetActive(false);
                if(building.currentBuilding != null){
                    building.selectionRing.SetActive(true);
                }
            }
        }
    }

    public void UnselectAll(){
        foreach (BuildingSpot building in buildings)
        {
            building.selectedRing.SetActive(false);
            if(building.currentBuilding != null){
                    building.selectionRing.SetActive(true);
            }
        }
    }
}
