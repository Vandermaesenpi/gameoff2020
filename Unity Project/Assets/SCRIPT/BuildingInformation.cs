using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformation : MonoBehaviour
{
    public Pointer pointer;
    public BuildingSpot selectedSpot;
    [Header("Header")]
    public Image outline;
    public Image buildingImage;
    public Text buildingName;
    public Text populationText;
    [Header("Tabs")]
    public Toggle overviewToggle;
    public Toggle productionToggle;
    public Toggle projectToggle;
    public Toggle maintenanceToggle;
    public GameObject overviewMenu, productionMenu, projectMenu, projectDetail, projectCurrent, maintenanceMenu;
    [Header("Overview")]
    public GameObject integrity;
    public Text buildDate;
    public Image integrityMeter;
    public Text integrityMeterText;
    public GameObject startMaintenanceButton;
    public GameObject stopMaintenanceButton;
    public GameObject destroyButton;
    public GameObject destroyWarning;
    [Header("Production")]
    public Text efficiency;
    public RessourceBox monthlyProduction;
    public RessourceBox monthlyCost;
    public GameObject stopProduction;
    public GameObject resumeProduction;
    public Text storage;
    public Image storageBar;
    public Image storageOutline;



    public void ShowBuildingInfo(BuildingSpot spot){
        if(spot != null){
            gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
            pointer.target = spot.transform;
            outline.color = spot.currentBuilding.color;
            buildingImage.color = spot.currentBuilding.color;
            overviewToggle.SetIsOnWithoutNotify(true);
            productionToggle.SetIsOnWithoutNotify(false);
            projectToggle.SetIsOnWithoutNotify(false);
            maintenanceToggle.SetIsOnWithoutNotify(false);
            productionToggle.gameObject.SetActive(spot.currentBuilding.productor);
            projectToggle.gameObject.SetActive(spot.currentBuilding.research);
            DestroyMenu(false);
            selectedSpot = spot;
            UpdateMenuInfo(spot);
        }else{
            gameObject.SetActive(false);
            pointer.gameObject.SetActive(false);
            selectedSpot = null;
        }
    }

    public void UpdateMenuInfo(BuildingSpot spot){
        
        buildingImage.sprite = spot.currentBuilding.sprite;
        buildingName.text = spot.currentBuilding.buildingName + " " + spot.district;
        Color populationColor = spot.population > spot.currentBuilding.populationRequirement ? GM.I.art.red : GM.I.art.white;
        populationText.text = "<color=#"+ColorUtility.ToHtmlStringRGBA(populationColor) + ">" + UIManager.HumanNotation(spot.population) + "</color>" + " / " + UIManager.HumanNotation(spot.currentBuilding.populationRequirement);
        
        overviewMenu.SetActive(overviewToggle.isOn);
        productionMenu.SetActive(productionToggle.isOn);
        projectMenu.SetActive(projectToggle.isOn);
        maintenanceMenu.SetActive(maintenanceToggle.isOn);

        integrity.SetActive(spot.Built && !spot.currentBuilding.control);
        buildDate.text = "Built in "+UIManager.TimeToDate(spot.constructionDate);
        integrityMeterText.text = UIManager.HumanNotation(spot.integrity);
        integrityMeter.fillAmount = spot.integrity;
        
        monthlyProduction.UpdateRessourceBox(spot.currentBuilding.production.GetProduction().Multiply(spot.efficiency));
        monthlyCost.UpdateRessourceBox(spot.currentBuilding.production.GetCost().Multiply(spot.costEfficiency));
        storage.text = (int)spot.ResourcePortion() + " / " + spot.storage;
        storageBar.fillAmount = spot.ResourcePortion() / spot.storage;
        storageBar.color = spot.currentBuilding.color;
        storageOutline.color = spot.currentBuilding.color;

        StartMaintenance(spot.maintenance);
        
        if(spot.currentBuilding.control){
            destroyButton.SetActive(false);
        }
        if(!overviewToggle.isOn){
            DestroyMenu(false);
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
        integrityMeter.color = onOff? GM.I.art.white : GM.I.art.light;
    }

    public void DestroyMenu(bool onOff){
        destroyWarning.SetActive(onOff);
        destroyButton.SetActive(!onOff);
    }

    public void DestroyBuilding(){
        selectedSpot.Destroy();
        ShowBuildingInfo(null);
    }
}
