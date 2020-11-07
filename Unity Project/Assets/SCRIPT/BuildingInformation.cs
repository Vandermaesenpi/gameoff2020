using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformation : MonoBehaviour
{
    public Image buildingImage;
    public Text buildingName;
    public Text buildingDescription;
    public Pointer pointer;

    public void ShowBuildingInfo(BuildingSpot spot){
        if(spot != null){
            gameObject.SetActive(true);
            buildingImage.sprite = spot.currentBuilding.sprite;
            buildingName.text = spot.currentBuilding.buildingName + " " + spot.district;
            pointer.gameObject.SetActive(true);
            pointer.target = spot.transform;
        }else{
            gameObject.SetActive(false);
            pointer.gameObject.SetActive(false);
        }
    }
}
