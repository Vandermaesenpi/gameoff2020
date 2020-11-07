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
