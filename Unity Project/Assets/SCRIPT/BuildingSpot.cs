using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSpot : MonoBehaviour
{
    public string district;
    public bool discovered = false;
    public BuildingSpotMode mode;
    public BuildingStatus status;
    public BuildingObject currentBuilding;
    public List<Connection> connections;
    public GameObject positionRing;
    public GameObject positionPoint;
    public GameObject statusRing;
    public GameObject selectionRing;
    public GameObject selectedRing;
    public GameObject constructionModel;
    public Image progressBar;
    public GameObject buildingModel;

    public int constructionAmount;
    public float integrity;
    public bool maintenance;
    public float storageMax;
    public float storage;
    public bool increaseStorage;
    public bool producing;

    public bool Built{ get{ return constructionAmount >= currentBuilding.constructionTime;}}

    private void Start() {
        if(currentBuilding != null){
            buildingModel = Instantiate(currentBuilding.prefab, transform);
        }
        UpdateVisual();
    }

    public void Build(){
        
        currentBuilding = GM.I.ui.buildingMenu.selectedBuilding;
        buildingModel = Instantiate(currentBuilding.prefab, transform);
        buildingModel.SetActive(false);
        constructionModel.SetActive(true);
        progressBar.color = currentBuilding.color;
        GM.I.city.SetBuildingSpotMode(BuildingSpotMode.Building);
        status = BuildingStatus.Construction;
    }

    public void UpdateBuilding(){
        if(currentBuilding == null){
            return;
        }
        if(constructionAmount < currentBuilding.constructionTime){
            progressBar.enabled = true;
            constructionAmount++;
            progressBar.fillAmount = ((float)constructionAmount / (float)currentBuilding.constructionTime);
        }else if (status == BuildingStatus.Construction){
            status = BuildingStatus.Stopped;
            progressBar.enabled = false;
            constructionModel.SetActive(false);
            buildingModel.SetActive(true);
            discovered = true;
            foreach (Connection connection in connections)
            {
                connection.GetOther(this).discovered = true;
            }
            GM.I.city.UpdateCityVisuals();
        }
    }

    public void Select(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(this);
        GM.I.city.UnselectAll();
        mode = BuildingSpotMode.Selected;
        UpdateVisual();
    }

    private void Update() {
        if(mode == BuildingSpotMode.Building && currentBuilding == null){
            positionRing.GetComponent<ButtonMesh>().highlight.GetComponent<MeshRenderer>().material.color = GM.I.ui.buildingMenu.selectedBuilding.color;
        }
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
                        if(constructionAmount >= currentBuilding.constructionTime){
                            foreach (Connection connection in connections)
                            {
                                connection.gameObject.SetActive(true);
                            }
                        }
                        positionPoint.SetActive(true);
                    }
                }
            break;
            case BuildingSpotMode.Selected:
                selectedRing.SetActive(true);
            break;
        }
    }

}

public enum BuildingSpotMode{
    Normal,
    Building,
    Selected
}

public enum BuildingStatus{
    Construction,
    Operating,
    Stopped,
    Damaged
}
