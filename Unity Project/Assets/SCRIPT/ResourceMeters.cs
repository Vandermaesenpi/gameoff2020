using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeters : MonoBehaviour
{
    public Image energy, water, material;
    public Text energyText, waterText, materialText;
    public Resource lastDelta;

    public void UpdateResources(){
        Resource r = GM.I.resource.resources;
        energy.fillAmount = r.Energy / GM.I.resource.resourcesLimit.Energy;
        water.fillAmount = r.Water / GM.I.resource.resourcesLimit.Water;
        material.fillAmount = r.Material / GM.I.resource.resourcesLimit.Material;
        energyText.text = ""+ (int)r.Energy;
        waterText.text = ""+ (int)r.Water;
        materialText.text = ""+ (int)r.Material;
        if(r.Energy < lastDelta.Energy){
            energyText.color = GM.I.art.red;
        }else{
            energyText.color = GM.I.art.yellow;
        }
        if(r.Water < lastDelta.Water){
            waterText.color = GM.I.art.red;
        }else{
            waterText.color = GM.I.art.blue;
        }
        if(r.Material < lastDelta.Material){
            materialText.color = GM.I.art.red;
        }else{
            materialText.color = GM.I.art.brown;
        }
        lastDelta.r[0] = r.Energy;
        lastDelta.r[1] = r.Water;
        lastDelta.r[2] = r.Material;
    }
}
