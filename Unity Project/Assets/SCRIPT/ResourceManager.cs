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
                    if(building.currentBuilding.productor){
                        delta.Add(building.Production);
                    }
                    delta.Add(building.Cost);
                    if(building.increaseStorage && !building.currentBuilding.storageIncreaseMonthlyCost.Limited(GM.I.resource.resources)){
                        delta.Add(building.currentBuilding.storageIncreaseMonthlyCost);
                    }
                    if(building.currentProject != null){
                        delta.Add(building.currentProject.monthlyCost);
                    }
                }else if (!building.constructionHalted){
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

    public Resource Multiply(float e,float w,float m){
        Resource newR = new Resource();
        newR.r[0] = Energy * e;
        newR.r[1] = Water * w;
        newR.r[2] = Material * m;
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

    public bool Limited(Resource stock){
        return stock.Energy < Mathf.Abs(Energy) || stock.Water < Mathf.Abs(Water) || stock.Material < Mathf.Abs(Material);
    }
}