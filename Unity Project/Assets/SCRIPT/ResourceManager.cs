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
                if(building.Built){
                    delta.r[0] += building.currentBuilding.production.r[0];
                    delta.r[1] += building.currentBuilding.production.r[1];
                    delta.r[2] += building.currentBuilding.production.r[2];
                }else{
                    delta.r[0] -= building.currentBuilding.constructionMonthlyCost.r[0];
                    delta.r[1] -= building.currentBuilding.constructionMonthlyCost.r[1];
                    delta.r[2] -= building.currentBuilding.constructionMonthlyCost.r[2];
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
    public float Energy {get{return r[0];}}
    public float Water {get{return r[1];}}
    public float Material {get{return r[2];}}

    public Resource GetProduction(){
        Resource r = new Resource();
        if(Energy > 0){
            r.r[0] = Energy;
        }if(Water > 0){
            r.r[1] = Water;
        }if(Material > 0){
            r.r[2] = Material;
        }
        return r;
    }

    public Resource GetCost(){
        Resource r = new Resource();
        if(Energy < 0){
            r.r[0] = Energy;
        }if(Water < 0){
            r.r[1] = Water;
        }if(Material < 0){
            r.r[2] = Material;
        }
        return r;
    }
}