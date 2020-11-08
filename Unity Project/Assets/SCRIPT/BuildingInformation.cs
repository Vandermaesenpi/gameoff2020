using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformation : MonoBehaviour
{
    public Pointer pointer;
    public BuildingSpot selectedSpot;
    [Header("Header")]
    public Image buildingImage;
    public Text buildingName;
    public Text statusOverview;
    [Header("Tabs")]
    public Toggle overviewToggle;
    public Toggle productionToggle;
    public Toggle projectToggle;
    public GameObject overviewMenu, productionMenu, projectMenu, projectDetail, projectCurrent;
    [Header("Overview")]
    public Text buildDate;
    public Image integrityMeter;
    public Text integrityMeterText;
    [Header("Production")]
    public Text efficiency;
    public RessourceBox monthlyProduction;
    public RessourceBox monthlyCost;



    public void ShowBuildingInfo(BuildingSpot spot){
        if(spot != null){
            gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
            pointer.target = spot.transform;
            overviewToggle.SetIsOnWithoutNotify(true);
            productionToggle.SetIsOnWithoutNotify(false);
            projectToggle.SetIsOnWithoutNotify(false);
            UpdateMenuInfo(spot);
            selectedSpot = spot;
        }else{
            gameObject.SetActive(false);
            pointer.gameObject.SetActive(false);
            selectedSpot = null;
        }
    }

    public void UpdateMenuInfo(BuildingSpot spot){
        buildingImage.sprite = spot.currentBuilding.sprite;
        buildingName.text = spot.currentBuilding.buildingName + " " + spot.district;
        overviewMenu.SetActive(overviewToggle.isOn);
        productionMenu.SetActive(productionToggle.isOn);
        projectMenu.SetActive(projectToggle.isOn);
        
        buildDate.text = "Built in "+UIManager.TimeToDate(spot.constructionDate);
        integrityMeterText.text = UIManager.HumanNotation(spot.integrity);
        integrityMeter.fillAmount = spot.integrity;
        monthlyProduction.UpdateRessourceBox(spot.currentBuilding.production.GetProduction());
        monthlyCost.UpdateRessourceBox(spot.currentBuilding.production.GetCost());
    }

    public void UpdateMenuInfo(){
        if(selectedSpot != null){
            UpdateMenuInfo(selectedSpot);
        }
    }
}
