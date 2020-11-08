using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformation : MonoBehaviour
{
    public Pointer pointer;
    public Image buildingImage;
    public Text buildingName;
    public Text statusOverview;
    [Header("Tabs")]
    public Toggle overviewToggle;
    public Toggle productionToggle;
    public Toggle projectToggle;
    public Toggle societyToggle;
    public GameObject overviewMenu, productionMenu, projectMenu, societyMenu;

    public void ShowBuildingInfo(BuildingSpot spot){
        if(spot != null){
            gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
            pointer.target = spot.transform;
            UpdateMenuInfo(spot);
        }else{
            gameObject.SetActive(false);
            pointer.gameObject.SetActive(false);
        }
    }

    public void UpdateMenuInfo(BuildingSpot spot){
        buildingImage.sprite = spot.currentBuilding.sprite;
        buildingName.text = spot.currentBuilding.buildingName + " " + spot.district;
    }
}
