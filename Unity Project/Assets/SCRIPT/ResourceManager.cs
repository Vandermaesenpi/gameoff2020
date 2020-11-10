using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Resource resources;
    public Resource resourcesLimit{get{
        Resource newR = new Resource();
        newR.r[0] = GM.I.city.Storage(0);
        newR.r[1] = GM.I.city.Storage(1);
        newR.r[2] = GM.I.city.Storage(2);
        return newR;
    }}

    public void UpdateResources(){
        Resource delta = new Resource();
        foreach (BuildingSpot building in GM.I.city.buildings)
        {
            if(building.currentBuilding != null){
                if(building.Built){
                    if(building.currentBuilding.productor && building.producing){
                        delta.Add(building.currentBuilding.production.GetProduction().Multiply(building.efficiency));
                    }
                    delta.Add(building.currentBuilding.production.GetCost().Multiply(building.costEfficiency));
                }else{
                    delta.Add(building.currentBuilding.constructionMonthlyCost);
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

    public void Add(Resource delta){
        r[0] += delta.r[0];
        r[1] += delta.r[1];
        r[2] += delta.r[2];
    }

    public Resource Multiply(float amount){
        Resource newR = new Resource();
        newR.r[0] = Energy * amount;
        newR.r[1] = Water * amount;
        newR.r[2] = Material * amount;
        return newR;
    }

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