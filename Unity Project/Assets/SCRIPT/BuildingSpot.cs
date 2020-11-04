using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    public string district;
    public BuildingObject currentBuilding;
    public GameObject positionRing;
    public GameObject positionPoint;
    public GameObject statusRing;
    public GameObject selectionRing;
    public GameObject selectedRing;

    public GameObject buildingModel;

    public void Build(){
        currentBuilding = GM.I.ui.buildingMenu.selectedBuilding;
        buildingModel = Instantiate(currentBuilding.prefab, transform);
        positionRing.SetActive(false);
    }

    public void Select(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(this);
        GM.I.city.UnselectAll();
        selectedRing.SetActive(true);
        selectionRing.SetActive(false);
    }

}
