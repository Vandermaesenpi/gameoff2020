using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_BuildingIntegrity : EventChecker
{
    public BuildingObject buildingObject;
    public override void Check()
    {
        float minIntegrity = 50f;
        foreach (BuildingSpot building in GM.I.city.buildings)
        {
            if(building.currentBuilding == buildingObject){
                minIntegrity = Mathf.Min(minIntegrity, building.integrity);
            }
        }
        if(minIntegrity < threshold && !triggered){
            TriggerEvent();
        }
        if(triggered && resetValue < minIntegrity){
            Reset();
        }
        base.Check();
    }
}
