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
    public int constructionDate;
    public float integrity;
    public bool maintenance;
    public float storageMax;
    public float storage;
    public bool increaseStorage;
    public bool producing;
    public float efficiency;
    public float efficiencyModifier;
    public float costEfficiency;
    public float costEfficiencyModifier;
    public int population;

    public bool Built{ get{ return constructionAmount >= currentBuilding.constructionTime;}}

    private void Start() {
        if(currentBuilding != null){
            buildingModel = Instantiate(currentBuilding.prefab, transform);
        }
        UpdateVisual();
    }

    public void Build(){
        currentBuilding = GM.I.ui.buildingMenu.selectedBuilding;
        integrity = 1f;
        constructionAmount = 0;
        buildingModel = Instantiate(currentBuilding.prefab, transform);
        buildingModel.SetActive(false);
        constructionModel.SetActive(true);
        progressBar.color = currentBuilding.color;
        GM.I.city.SetBuildingSpotMode(BuildingSpotMode.Building);
        status = BuildingStatus.Construction;
    }

    public void Destroy(){
        currentBuilding = null;
        Destroy(buildingModel);
        constructionModel.SetActive(false);
        progressBar.fillAmount = 0f;
        storage = 0;
        status = BuildingStatus.Construction;
        mode = BuildingSpotMode.Normal;
        UpdateVisual();
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
            constructionDate = GM.I.gameplay.currentTime;
            progressBar.enabled = false;
            constructionModel.SetActive(false);
            buildingModel.SetActive(true);
            discovered = true;
            storage = currentBuilding.baseStorage;
            foreach (Connection connection in connections)
            {
                connection.GetOther(this).discovered = true;
            }
            if(currentBuilding.productor){
                producing = true;
            }
            GM.I.city.UpdateCityVisuals();
        }else{
            ProcessIntegrity();
            ProcessEfficiency();
            
        }
    }

    void ProcessIntegrity(){
        if(maintenance){
            integrity = Mathf.Min(integrity + 0.01f, 1f);
            if(integrity == 1f){
                GM.I.ui.buildingInformation.StartMaintenance(false, this);
            }
        }else{
            if(Random.value < 0.2f){
                integrity = Mathf.Max(0,integrity - currentBuilding.decay);
            }
        }
    }

    void ProcessEfficiency(){
        costEfficiencyModifier = 1f;
        if(maintenance){
            costEfficiencyModifier+= 2f;
        }
        if(currentBuilding.housing){
            population = GM.I.people.TotalPopulation/GM.I.city.Housing();
            costEfficiency = Mathf.Max(Mathf.Clamp((float)population/(float)currentBuilding.populationRequirement,0f,3f) + costEfficiencyModifier, 0f);
        }else if (currentBuilding.productor){
            population = Mathf.Clamp(GM.I.people.WorkingPopulation/GM.I.city.Workplace(), 0, currentBuilding.populationRequirement);
            efficiency = Mathf.Max(Mathf.Clamp((float)population/(float)currentBuilding.populationRequirement,0f,1f) + efficiencyModifier, 0f);
            costEfficiency = costEfficiencyModifier;
        }
    }

    public void Select(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(this);
        GM.I.city.UnselectAll();
        mode = BuildingSpotMode.Selected;
        UpdateVisual();
    }

    public void Unselect(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(null);
        GM.I.city.UnselectAll();
        mode = BuildingSpotMode.Normal;
        UpdateVisual();
    }

    public float ResourcePortion(){
        return GM.I.resource.resources.r[currentBuilding.ressourceType] * (storage/GM.I.city.Storage(currentBuilding.ressourceType));
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
