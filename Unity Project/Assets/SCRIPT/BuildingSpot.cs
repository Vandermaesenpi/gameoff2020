using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    public string district;
    public bool discovered = false;
    public BuildingSpotMode mode;
    public BuildingObject currentBuilding;
    public List<Connection> connections;
    public GameObject positionRing;
    public GameObject positionPoint;
    public GameObject statusRing;
    public GameObject selectionRing;
    public GameObject selectedRing;
    public GameObject buildingModel;


    private void Start() {
        if(currentBuilding != null){
            buildingModel = Instantiate(currentBuilding.prefab, transform);
        }
    }

    public void Build(){
        discovered = true;
        foreach (Connection connection in connections)
        {
            connection.GetOther(this).discovered = true;
        }
        currentBuilding = GM.I.ui.buildingMenu.selectedBuilding;
        buildingModel = Instantiate(currentBuilding.prefab, transform);
        GM.I.city.SetBuildingSpotMode(BuildingSpotMode.Building);
    }

    public void Select(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(this);
        GM.I.city.UnselectAll();
        mode = BuildingSpotMode.Selected;
        UpdateVisual();
    }

    public void UpdateVisual(){

        positionRing.SetActive(false);
        positionPoint.SetActive(false);
        statusRing.SetActive(false);
        selectionRing.SetActive(false);
        selectedRing.SetActive(false);

        
        switch (mode)
        {
            case BuildingSpotMode.Normal:
                if(currentBuilding != null){
                    selectionRing.SetActive(true);
                }
                foreach (Connection connection in connections)
                {
                    connection.gameObject.SetActive(false);
                }
            break;
            case BuildingSpotMode.Building:
                if(discovered){
                    if(currentBuilding == null){
                        positionRing.SetActive(true);
                    }else{
                        foreach (Connection connection in connections)
                        {
                            connection.gameObject.SetActive(true);
                        }
                        positionPoint.SetActive(true);
                    }
                }
            break;
            case BuildingSpotMode.Selected:
                selectedRing.SetActive(true);
                foreach (Connection connection in connections)
                {
                    if(connection.GetOther(this).currentBuilding != null){
                        connection.gameObject.SetActive(true);
                        connection.GetOther(this).positionPoint.SetActive(true);
                    }
                }
            break;
        }
    }

}

public enum BuildingSpotMode{
    Normal,
    Building,
    Selected
}
