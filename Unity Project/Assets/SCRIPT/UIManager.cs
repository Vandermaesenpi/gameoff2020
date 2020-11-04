using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BuildingInformation buildingInformation;
    public BuildingMenu buildingMenu;

    public void ClicBuildingMenu(){
        buildingMenu.gameObject.SetActive(!buildingMenu.gameObject.activeInHierarchy);
    }
}
