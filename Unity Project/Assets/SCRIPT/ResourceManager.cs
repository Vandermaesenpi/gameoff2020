using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Resource resources;
    public Resource resourcesLimit;

    public void UpdateResources(){
        Resource delta = new Resource();
        foreach (BuildingSpot building in GM.I.city.buildings)
        {
            if(building.currentBuilding != null){
                if(building.constructionAmount < building.currentBuilding.constructionTime){
                    delta.r[0] -= building.currentBuilding.constructionMonthlyCost.r[0];
                    delta.r[1] -= building.currentBuilding.constructionMonthlyCost.r[1];
                    delta.r[2] -= building.currentBuilding.constructionMonthlyCost.r[2];
                }else{
                    delta.r[0] += building.currentBuilding.production.r[0];
                    delta.r[1] += building.currentBuilding.production.r[1];
                    delta.r[2] += building.currentBuilding.production.r[2];
                }
            }
        }

        resources.r[0] = Mathf.Clamp(resources.r[0] + delta.r[0], 0, resourcesLimit.r[0]);
        resources.r[1] = Mathf.Clamp(resources.r[1] + delta.r[1], 0, resourcesLimit.r[1]);
        resources.r[2] = Mathf.Clamp(resources.r[2] + delta.r[2], 0, resourcesLimit.r[2]);

        GM.I.ui.resourceMeters.UpdateResources();
    }
}
[System.Serializable]
public class Resource{
    public float[] r = new float[3];
}

public enum R{
    Energy,
    Water,
    Material,
}
