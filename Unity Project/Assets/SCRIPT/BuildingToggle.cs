using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingToggle : Toggle
{
    public BuildingObject building;
    public Image buildingImage;
    public Text buildingName;
    public Text buildingDescription;
    public RessourceBox cost;

    public void InitializeMiniature(){
        buildingImage.sprite = building.sprite;
        buildingImage.color = building.color;
    }
}
