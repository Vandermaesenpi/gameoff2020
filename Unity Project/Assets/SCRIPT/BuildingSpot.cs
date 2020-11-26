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
    public GameObject statusRingInfo;
    public GameObject statusRingWarning;
    public GameObject statusRingBad;
    public GameObject selectionRing;
    public GameObject selectedRing;
    public GameObject constructionModel;
    public MeshRenderer constructionTipee;
    public Image progressBar;
    public GameObject buildingModel;
    public bool selected = false;
    public int constructionAmount;
    public int constructionDate;
    public bool constructionHalted;
    public float integrity;
    public bool maintenance;
    public float storageMax;
    public float storage;
    public bool increaseStorage;
    public int storageCounter;
    public bool producing;
    public float efficiency;
    public float efficiencyModifier;
    public float costEfficiency;
    public float costEfficiencyModifier;
    public int population;
    public Project currentProject;

    public bool Built{ get{ return constructionAmount >= currentBuilding.constructionTime;}}
    public bool OverPopulated{get{return population > currentBuilding.populationRequirement;}}
    public bool HighPopulated{get{return population > currentBuilding.populationRequirement*0.9f;}}
    public bool LowPopulated{get{return population < currentBuilding.populationRequirement*0.3f;}}
    public bool BadIntegrity{get{return integrity < 0.5f;}}
    public bool DangerousIntegrity{get{return integrity < 0.25f;}}

    public Resource Production {get{return currentBuilding.production.GetProduction().Multiply(efficiency).Multiply(GM.I.project.FX(FXT.Energy),GM.I.project.FX(FXT.Water),GM.I.project.FX(FXT.Material));}}
    public Resource Cost {
        get{
            if(currentProject != null){
                return currentBuilding.production.GetCost().Multiply(costEfficiency).Multiply(GM.I.project.FX(FXT.Cost)).Add(currentProject.monthlyCost);
            }
            return currentBuilding.production.GetCost().Multiply(costEfficiency).Multiply(GM.I.project.FX(FXT.Cost));
        }
    }

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
        GM.I.sfx.Play(SFX.Build);
    }

    public void Destroy(){
        currentBuilding = null;
        Destroy(buildingModel);
        constructionModel.SetActive(false);
        progressBar.fillAmount = 0f;
        storage = 0;
        status = BuildingStatus.Construction;
        selected = false;
        GM.I.city.UpdateCityVisuals();
    }

    public void UpdateBuilding(){
        if(currentBuilding == null){
            return;
        }
        if(constructionAmount < currentBuilding.constructionTime){
            if(currentBuilding.constructionMonthlyCost.Limited(GM.I.resource.resources)){
                constructionHalted = true;
                progressBar.color = GM.I.art.red;
            }else{
                progressBar.color = currentBuilding.color;
                constructionHalted = false;
                constructionAmount++;
            }
            progressBar.enabled = true;
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
            UpdateBuilding();
            GM.I.city.UpdateCityVisuals();
        }else{
            if(increaseStorage){
                if(storageCounter == 0){
                    storage += 200;
                    increaseStorage = false;
                    storageCounter = 12 * (int)storage/200;
                }else if (!currentBuilding.storageIncreaseMonthlyCost.Limited(GM.I.resource.resources)){
                    storageCounter--;
                }
            }
            ProcessIntegrity();
            ProcessEfficiency();
        }
        UpdateVisual();
    }

    void ProcessIntegrity(){
        if(maintenance){
            integrity = Mathf.Min(integrity + 0.01f, 1f);
            if(integrity == 1f){
                GM.I.ui.buildingInformation.StartMaintenance(false, this);
            }
        }else{
            if(Random.value < 0.2f){
                integrity = Mathf.Max(0,integrity - currentBuilding.decay * GM.I.project.FX(FXT.Integrity));
            }
        }
    }

    void ProcessEfficiency(){
        costEfficiencyModifier = 1f;
        efficiencyModifier = 0f;
        
        if(currentBuilding.housing){
            population = (int)((float)GM.I.people.TotalPopulation * ((float)currentBuilding.populationRequirement/(float)GM.I.city.HousingSpace()));
            if(maintenance || integrity == 0){
                costEfficiencyModifier+= 1f;
            }
            costEfficiency = Mathf.Max(Mathf.Clamp((float)population*5f/(float)currentBuilding.populationRequirement,0f,1000f) + costEfficiencyModifier, 0f);
        }else if (currentBuilding.productor){
            if(!producing || maintenance || integrity == 0){
                population = 0;
                efficiency = 0f;
                costEfficiency = 0f;
            }else{
                population = (int)Mathf.Clamp(GM.I.people.WorkingPopulation/(uint)GM.I.city.Workplace(), 0, currentBuilding.populationRequirement);
                efficiency = Mathf.Max(Mathf.Clamp((float)population/(float)currentBuilding.populationRequirement,0f,1f) + efficiencyModifier, 0f);
                costEfficiency = costEfficiencyModifier;
            }
        }else if (currentBuilding.research){
            if(currentProject == null || maintenance || integrity == 0){
                population = 0;
                efficiency = 0f;
                costEfficiency = 0f;
            }else{
                population = (int)Mathf.Clamp(GM.I.people.WorkingPopulation/(uint)GM.I.city.Workplace(), 0, currentBuilding.populationRequirement);
                efficiency = Mathf.Max(Mathf.Clamp((float)population/(float)currentBuilding.populationRequirement,0f,1f) + efficiencyModifier, 0f);
                costEfficiency = costEfficiencyModifier;
            }
        }
    }

    public void Select(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(this);
        GM.I.city.UnselectAll();
        GM.I.moonRotator.ShowBuildingSpot(transform);
        selected = true;
        GM.I.tooltip.HideTooltip();
        UpdateVisual();
    }

    public void Unselect(){
        GM.I.ui.buildingInformation.ShowBuildingInfo(null);
        selected = false;
        UpdateVisual();
    }

    public float ResourcePortion(){
        return GM.I.resource.resources.r[currentBuilding.ressourceType] * (storage/GM.I.city.Storage(currentBuilding.ressourceType));
    }

    private void Update() {
        if(mode == BuildingSpotMode.Building && currentBuilding == null){
            positionRing.GetComponent<ButtonMesh>().highlight.GetComponent<MeshRenderer>().material.color = GM.I.ui.buildingMenu.selectedBuilding.color;
            constructionTipee.material.color = GM.I.ui.buildingMenu.selectedBuilding.color;
        }
    }

    public void ShowTooltip(){
        List<string> statuses = new List<string>();
        List<Color> colors = new List<Color>();
        GM.I.ui.buildingInformation.ProcessStatus(this, false, ref statuses, ref colors);

        string tooltip = "";
        for (var i = 0; i < statuses.Count; i++)
        {
            tooltip += UIManager.ColoredString(statuses[i],colors[i]);
            if(i < statuses.Count-1){
                tooltip += "\n";
            }
        }
        GM.I.tooltip.ShowTooltip(tooltip, Vector2.up * 10);
    }
    public void HideTooltip(){
        GM.I.tooltip.HideTooltip();        
    }

    public void UpdateVisual(){

        if(currentBuilding != null){
            int currentStatus = GM.I.ui.buildingInformation.ProcessStatus(this, false);
            if(currentStatus == 0){
                statusRingBad.SetActive(false);
                statusRingWarning.SetActive(false);
                statusRingInfo.SetActive(false);
            }if(currentStatus == 1){
                statusRingBad.SetActive(false);
                statusRingWarning.SetActive(false);
                statusRingInfo.SetActive(true);
            }if(currentStatus == 2){
                statusRingBad.SetActive(false);
                statusRingWarning.SetActive(true);
                statusRingInfo.SetActive(false);
            }if(currentStatus == 3){
                statusRingBad.SetActive(true);
                statusRingWarning.SetActive(false);
                statusRingInfo.SetActive(false);
            }
        }else{
            statusRingBad.SetActive(false);
            statusRingWarning.SetActive(false);
            statusRingInfo.SetActive(false);
        }
        
        
        switch (mode)
        {
            case BuildingSpotMode.Normal:

                positionRing.SetActive(false);
                positionPoint.SetActive(false);

                if(currentBuilding != null){
                    if(selected){
                        selectionRing.SetActive(false);
                        if(!selectedRing.activeInHierarchy)
                            selectedRing.SetActive(true);
                    }else{
                        selectedRing.SetActive(false);
                        if(!selectionRing.activeInHierarchy)
                            selectionRing.SetActive(true);
                    }
                }else{
                    selectedRing.SetActive(false);
                    selectedRing.SetActive(false);
                }
                
                foreach (Connection connection in connections)
                {
                    connection.gameObject.SetActive(false);
                }
            break;
            case BuildingSpotMode.Building:
                if(discovered){
                    if(currentBuilding != null){
                        if(selected){
                            selectionRing.SetActive(false);
                            if(!selectedRing.activeInHierarchy)
                                selectedRing.SetActive(true);
                        }else{
                            selectedRing.SetActive(false);
                            if(!selectionRing.activeInHierarchy)
                                selectionRing.SetActive(true);
                        }
                        if(positionRing.activeInHierarchy)
                            positionRing.SetActive(false);
                        if(!positionPoint.activeInHierarchy)
                            positionPoint.SetActive(true);
                        if(constructionAmount >= currentBuilding.constructionTime){
                            foreach (Connection connection in connections)
                            {
                                connection.gameObject.SetActive(true);
                            }
                        }
                    }else{
                        selectedRing.SetActive(false);
                        selectedRing.SetActive(false);

                        if(!positionRing.activeInHierarchy)
                            positionRing.SetActive(true);
                        if(positionPoint.activeInHierarchy)
                            positionPoint.SetActive(false);
                        foreach (Connection connection in connections)
                            {
                                if(connection.GetOther(this).discovered && discovered){
                                    connection.gameObject.SetActive(true);
                                }
                            }
                    }
                    
                }
            break;
        }
    }

}

public enum BuildingSpotMode{
    Normal,
    Building,
}

public enum BuildingStatus{
    Construction,
    Operating,
    Stopped,
    Damaged
}
