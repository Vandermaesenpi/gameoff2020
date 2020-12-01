using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformation : MonoBehaviour
{
    public BuildingSpot selectedSpot;
    [Header("Header")]
    public Image outline;
    public Image buildingImage;
    public Text buildingName;
    public Text populationText;
    public Text populationName;
    [Header("Tabs")]
    public Toggle overviewToggle;
    public Toggle productionToggle;
    public Toggle projectToggle;
    public Toggle maintenanceToggle;
    public GameObject overviewMenu, productionMenu, projectMenu, projectDetail, maintenanceMenu;
    public GameObject overviewAlert, productionAlert, projectAlert, maintenanceAlert;
    [Header("Overview")]
    public List<Text> statusTexts;
    public GameObject destroyButton;
    public GameObject destroyWarning;
    [Header("Maintenance")]
    public GameObject integrity;
    public Text buildDate;
    public Text costInfo;
    public Text shortageEffect;
    public Image integrityMeter;
    public Image integrityMeterOutline;
    public Text integrityMeterText;
    public Text integrityRiskText;
    public Text maintenanceEffecText;
    public GameObject startMaintenanceButton;
    public GameObject stopMaintenanceButton;
    [Header("Production")]
    public Text efficiency;
    public RessourceBox monthlyProduction;
    public RessourceBox monthlyCost;
    public GameObject stopProduction;
    public GameObject resumeProduction;
    public Text storage;
    public Image storageBar;
    public Image storageOutline;
    public GameObject increaseStorageButton;
    public GameObject stopStorageButton;
    public RessourceBox increaseStorageCost;
    public Text increaseStorageTime;
    [Header("Project")]
    public List<ProjectChoice> projectChoices;
    public Text projectNameText;
    public Text projectDescriptionText;
    public Text projectEffectText;
    public Text projectLengthText;
    public Image projectImage;
    public RessourceBox projectCost;
    public GameObject startProjectButton, stopProjectButton;
    public Project selectedProject;



    public void ShowBuildingInfo(BuildingSpot spot){
        if(spot != null){
            gameObject.SetActive(true);
            buildingName.color = spot.currentBuilding.color;
            buildingImage.color = spot.currentBuilding.color;
            
            productionToggle.gameObject.SetActive(spot.currentBuilding.productor);
            projectToggle.gameObject.SetActive(spot.currentBuilding.research);
            maintenanceToggle.gameObject.SetActive(true);

            if(productionToggle.isOn && spot.currentBuilding.research){
                productionToggle.SetIsOnWithoutNotify(false);
                projectToggle.SetIsOnWithoutNotify(true);
                projectDetail.SetActive(false);
                projectMenu.SetActive(true);
            }
            if(projectToggle.isOn && spot.currentBuilding.productor){
                productionToggle.SetIsOnWithoutNotify(true);
                projectToggle.SetIsOnWithoutNotify(false);
            }
            if((productionToggle.isOn || projectToggle.isOn)&&!spot.currentBuilding.productor&&!spot.currentBuilding.research){
                overviewToggle.SetIsOnWithoutNotify(true);
                productionToggle.SetIsOnWithoutNotify(false);
                projectToggle.SetIsOnWithoutNotify(false);
                maintenanceToggle.SetIsOnWithoutNotify(false);
            }

            if(!spot.Built){
                overviewToggle.SetIsOnWithoutNotify(true);
                productionToggle.SetIsOnWithoutNotify(false);
                projectToggle.SetIsOnWithoutNotify(false);
                maintenanceToggle.SetIsOnWithoutNotify(false);
                productionToggle.gameObject.SetActive(false);
                projectToggle.gameObject.SetActive(false);
                maintenanceToggle.gameObject.SetActive(false);
            }

            DestroyMenu(false);
            selectedSpot = spot;
            selectedProject = null;
            UpdateMenuInfo(spot);
        }else{
            gameObject.SetActive(false);
            selectedSpot = null;
        }
    }

    public void UpdateMenuInfo(BuildingSpot spot){
        
        buildingImage.sprite = spot.currentBuilding.sprite;
        buildingName.text = spot.currentBuilding.buildingName + " " + spot.district;
        Color populationColor = spot.OverPopulated ? GM.I.art.red : GM.I.art.white;
        populationName.text = spot.currentBuilding.populationName;
        populationText.text = UIManager.ColoredString(UIManager.HumanNotation(spot.population),populationColor) + " / " + UIManager.HumanNotation(spot.currentBuilding.populationRequirement);
        
        overviewMenu.SetActive(overviewToggle.isOn);
        productionMenu.SetActive(productionToggle.isOn);
        maintenanceMenu.SetActive(maintenanceToggle.isOn);
        productionToggle.gameObject.SetActive(spot.currentBuilding.productor);
        projectToggle.gameObject.SetActive(spot.currentBuilding.research);
        maintenanceToggle.gameObject.SetActive(true);

        if(projectToggle.isOn){
                UpdateProject();
        }else{
            projectDetail.SetActive(false);
            projectMenu.SetActive(false);
        }
        

        costInfo.text = spot.currentBuilding.costInfo;
        shortageEffect.text = spot.Cost.Limited(GM.I.resource.resources)? spot.currentBuilding.shortageEffect:"";
        integrity.SetActive(spot.Built && !spot.currentBuilding.control);
        buildDate.text = "Built in "+UIManager.TimeToDate(spot.constructionDate);
        integrityMeterText.text = UIManager.HumanNotation(spot.integrity);
        maintenanceEffecText.text = spot.currentBuilding.maintenanceEffect;
        integrityMeter.fillAmount = spot.integrity;
        if(spot.BadIntegrity){
            integrityMeter.color = GM.I.art.orange;
            integrityRiskText.color = GM.I.art.orange;
            integrityRiskText.text = "FAILURE RISK - MEDIUM";
        }else if(spot.DangerousIntegrity){
            integrityMeter.color = GM.I.art.red;
            integrityRiskText.color = GM.I.art.red;
            integrityRiskText.text = "FAILURE RISK - HIGH";
        }else{
            integrityMeter.color = GM.I.art.light;
            integrityRiskText.color = GM.I.art.light;
            integrityRiskText.text = "FAILURE RISK - LOW";
        }
        
        monthlyProduction.UpdateRessourceBox(spot.Production);
        monthlyCost.UpdateRessourceBox(spot.Cost);
        storage.text = (int)spot.ResourcePortion() + " / " + spot.storage;
        storageBar.fillAmount = spot.ResourcePortion() / spot.storage;
        storageBar.color = spot.currentBuilding.color;
        storageOutline.color = spot.currentBuilding.color;
        increaseStorageButton.SetActive(!(spot.increaseStorage||spot.storage == spot.storageMax));
        stopStorageButton.SetActive(!(!spot.increaseStorage||spot.storage == spot.storageMax));
        increaseStorageCost.gameObject.SetActive(spot.storage != spot.storageMax);
        increaseStorageTime.gameObject.SetActive(spot.storage != spot.storageMax);
        increaseStorageCost.UpdateRessourceBox(spot.currentBuilding.storageIncreaseMonthlyCost);
        increaseStorageTime.text = spot.storageCounter + " months";
        StartMaintenance(spot.maintenance);
        StopProduction(!spot.producing);
        
        ProcessStatus(spot, true);

        for (int i = 0; i < spot.currentBuilding.projects.Count; i++)
        {
            Project project = spot.currentBuilding.projects[i];
            projectChoices[i].Init(project, spot);
        }
        
        if(spot.currentBuilding.control){
            destroyButton.SetActive(false);
        }
        if(!overviewToggle.isOn){
            DestroyMenu(false);
        }
    }
    public int ProcessStatus(BuildingSpot spot, bool updateText){
        List<string> statuses = new List<string>();
        List<Color> colors = new List<Color>();
        return ProcessStatus(spot, updateText, ref statuses, ref colors);
    }
    public int ProcessStatus(BuildingSpot spot, bool updateText, ref List<string> statuses, ref List<Color> colors){
        if(updateText){
            foreach (Text text in statusTexts)
            {
                text.text = "";
            }
            overviewAlert.SetActive(false);
            productionAlert.SetActive(false);
            projectAlert.SetActive(false);
            maintenanceAlert.SetActive(false);
        }

        if(!spot.Built){
            if(spot.constructionHalted){
                ProcessIndividualStatus("Construction stopped",GM.I.art.red,overviewAlert, ref statuses, ref colors, updateText);
                
            }else{
                ProcessIndividualStatus("Under construction",GM.I.art.light,null, ref statuses, ref colors, updateText);
            }
        }else{
            
            if(spot.Cost.Limited(GM.I.resource.resources)){
                ProcessIndividualStatus("Not enough resources!",GM.I.art.red,maintenanceAlert, ref statuses, ref colors, updateText);
            }
            if(spot.maintenance){
                ProcessIndividualStatus("Under maintenance",GM.I.art.light,maintenanceAlert, ref statuses, ref colors, updateText);
            }
            else if(spot.DangerousIntegrity){
                ProcessIndividualStatus("Needs maintenance!",GM.I.art.red,maintenanceAlert, ref statuses, ref colors, updateText);
            }
            else if(spot.BadIntegrity){
                ProcessIndividualStatus("Needs maintenance",GM.I.art.orange,maintenanceAlert, ref statuses, ref colors, updateText);
            }
            if(spot.currentBuilding.housing){
                if(spot.OverPopulated){
                    ProcessIndividualStatus("Overpopulated!",GM.I.art.red,overviewAlert, ref statuses, ref colors, updateText);
                }else if(spot.HighPopulated){
                    ProcessIndividualStatus("Crowded!",GM.I.art.orange,overviewAlert, ref statuses, ref colors, updateText);
                }
            }
            if(spot.currentBuilding.productor){
                if(!spot.producing){
                    ProcessIndividualStatus("Production stopped",GM.I.art.light,productionAlert, ref statuses, ref colors, updateText);
                }else if (!spot.maintenance){
                    if(spot.LowPopulated){
                        ProcessIndividualStatus("Not enough workers",GM.I.art.orange,productionAlert, ref statuses, ref colors, updateText);
                    }
                    if(spot.storage == spot.ResourcePortion()){
                        ProcessIndividualStatus("Storage full",GM.I.art.light,productionAlert, ref statuses, ref colors, updateText);
                    }
                }
                
            }
            if(spot.currentBuilding.research){
                if(spot.currentProject == null){
                    ProcessIndividualStatus("Needs a project",GM.I.art.pink,projectAlert, ref statuses, ref colors, updateText);
                }
            }
        }

        if(statuses.Count == 0){
            ProcessIndividualStatus("Working correctly",GM.I.art.green,null, ref statuses, ref colors, updateText);
        }
        int statusNumber = 0;
        for (int i = 0; i < statusTexts.Count; i++)
        {
            if(i < statuses.Count){
                if(updateText){
                    statusTexts[i].text = statuses[i];
                    statusTexts[i].color = colors[i];
                }
                if(colors[i] == GM.I.art.red){
                    statusNumber = 4;
                }
                if(colors[i] == GM.I.art.orange && statusNumber <= 2){
                    statusNumber = 3;
                }
                if(colors[i] == GM.I.art.pink && statusNumber <= 1){
                    statusNumber = 2;
                }
                if(colors[i] == GM.I.art.light && statusNumber == 0){
                    statusNumber = 1;
                }
            }
        }
        return statusNumber;
    }

    void ProcessIndividualStatus(string txt, Color color, GameObject alert, ref List<string> statuses, ref List<Color> colors, bool updateText){
        statuses.Add(txt);
        colors.Add(color);
        if(updateText && alert != null){
            alert.gameObject.SetActive(true);
        }
    }

    public void UpdateMenuInfo(){
        if(selectedSpot != null){
            UpdateMenuInfo(selectedSpot);
        }
    }

    public void StartMaintenance(bool onOff){
        StartMaintenance(onOff, selectedSpot);
        
    }

    public void StartMaintenance(bool onOff, BuildingSpot spot){
        spot.maintenance = onOff;
        startMaintenanceButton.SetActive(!onOff);
        stopMaintenanceButton.SetActive(onOff);
        integrityMeterOutline.color = onOff? GM.I.art.white : GM.I.art.light;
    }

    public void StopProduction(bool onOff){
        StopProduction(onOff, selectedSpot);
    }

    public void StopProduction(bool onOff, BuildingSpot spot){
        spot.producing = !onOff;
        stopProduction.SetActive(!onOff);
        resumeProduction.SetActive(onOff);
        spot.UpdateVisual();
    }

    public void DestroyMenu(bool onOff){
        destroyWarning.SetActive(onOff);
        destroyButton.SetActive(!onOff);
    }

    public void DestroyBuilding(){
        selectedSpot.Destroy();
        ShowBuildingInfo(null);
        GM.I.sfx.Play(SFX.Build);
    }

    public void IncreaseStorage(bool onOff){
        selectedSpot.increaseStorage = onOff;
        increaseStorageButton.SetActive(!onOff);
        stopStorageButton.SetActive(onOff);
    }

    public void SelectProject(Project project){
        selectedProject = project;
        projectMenu.SetActive(false);
        projectDetail.SetActive(true);
        UpdateProject();
        
    }

    public void UpdateProject(){

        if(selectedProject != null){
            projectDetail.SetActive(true);
            projectMenu.SetActive(false);
            projectNameText.text = selectedProject.projectName;
            projectImage.sprite = selectedProject.sprite;
            projectLengthText.gameObject.SetActive(!GM.I.project.IsConstant(selectedProject));
            projectLengthText.text = (GM.I.project.GetLength(selectedProject) - GM.I.project.GetTime(selectedProject)) + " months";
            startProjectButton.SetActive(selectedProject != selectedSpot.currentProject);
            stopProjectButton.SetActive(selectedProject == selectedSpot.currentProject);
            if(GM.I.project.GetLevel(selectedProject) == 4 && !GM.I.project.IsConstant(selectedProject)){
                startProjectButton.SetActive(false);
                stopProjectButton.SetActive(false);
            }
            projectDescriptionText.text = selectedProject.projectDescription;
            projectEffectText.text = selectedProject.effectDescription;
            projectCost.UpdateRessourceBox(selectedProject.monthlyCost);
        }else{
            projectDetail.SetActive(false);
            projectMenu.SetActive(true);
        }
        
    }

    public void CloseProjectDetail(){
        projectMenu.SetActive(true);
        projectDetail.SetActive(false);
        selectedProject = null;
        if(selectedSpot != null){
            for (int i = 0; i < selectedSpot.currentBuilding.projects.Count; i++)
            {
                Project project = selectedSpot.currentBuilding.projects[i];
                projectChoices[i].Init(project, selectedSpot);
            }
        }
    }

    public void StartProject(){
        selectedSpot.currentProject = selectedProject;
        SelectProject(selectedProject);
    }

    public void StopProject(){
        selectedSpot.currentProject = null;
        CloseProjectDetail();
    }
}
