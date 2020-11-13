using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public GameObject standardMoon, holoMoon;
    public List<BuildingSpot> buildings;
    public List<Connection> connections;
    private void Awake() {
        foreach (BuildingSpot spot in buildings)
        {
            spot.connections.Clear();
        }
        foreach (Connection item in connections)
        {
            item.spotStart.connections.Add(item);
            item.spotEnd.connections.Add(item);
        }
    }

    public int HousingSpace(){
        int space = 0;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && building.Built && building.currentBuilding.housing){
                space += building.currentBuilding.populationRequirement;
            }
        }
        return space;
    }

    public int Housing(){
        int housing = 0;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && building.Built && building.currentBuilding.housing){
                housing ++;
            }
        }
        return housing;
    }

    public int WorkplaceSpace(){
        int space = 0;
        foreach (BuildingSpot b in buildings)
        {
            if(b.currentBuilding != null && b.Built && 
            !b.currentBuilding.housing && !(b.currentBuilding.productor && (!b.producing || b.maintenance)) && 
            !(b.currentBuilding.research && (b.currentProject == null || b.maintenance)))
            {
                space += b.currentBuilding.populationRequirement;
            }
        }
        return space;
    }

    public int Workplace(){
        int workplace = 0;
        foreach (BuildingSpot b in buildings)
        {
            if(b.currentBuilding != null && b.Built && 
            !b.currentBuilding.housing && !(b.currentBuilding.productor && (!b.producing || b.maintenance)) && 
            !(b.currentBuilding.research && (b.currentProject == null || b.maintenance))){
                workplace ++;
            }
        }
        return workplace;
    }

    public float Storage(int type){
        float storage = 0;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && ((building.Built && !building.currentBuilding.housing && building.currentBuilding.ressourceType == type)||building.currentBuilding.control)){
                storage += building.storage;
            }
        }
        return storage;
    }

    public bool ResourceShortage(){
        return buildings[0].Cost.Limited(GM.I.resource.resources);
    }
    public void SetBuildingSpotMode(BuildingSpotMode mode){
        foreach (BuildingSpot building in buildings)
        {
            building.mode = mode;
            building.UpdateVisual();
        }
    }

    public void UnselectAll(){
        foreach (BuildingSpot building in buildings)
        {
            building.mode = BuildingSpotMode.Normal;
            building.UpdateVisual();
        }
    }

    public void UpdateCity(){
        foreach (BuildingSpot building in buildings)
        {
            building.UpdateBuilding();
        }
    }

    public void UpdateCityVisuals(){
        foreach (BuildingSpot building in buildings)
        {
            building.UpdateVisual();
        }
    }

    public void ShowHoloMoon(bool holo){
        standardMoon.SetActive(!holo);
        holoMoon.SetActive(holo);
    }
}
