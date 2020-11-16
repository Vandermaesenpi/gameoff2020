using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeters : MonoBehaviour
{
    public Image energy, water, material;
    public Text energyText, waterText, materialText;
    public Text energyDeltaText, waterDeltaText, materialDeltaText;
    public Text energyProductionText, waterProductionText, materialProductionText;
    public Text energyCostText, waterCostText, materialCostText;
    public Resource lastDelta;

    public void UpdateResources(){
        Resource r = GM.I.resource.resources;
        Resource delta = new Resource();
        delta.Add(r);
        delta.Add(lastDelta.Multiply(-1f));
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

        energyDeltaText.text = ""+ (Mathf.Round(delta.Energy*10f))/10f;
        waterDeltaText.text = ""+ (Mathf.Round(delta.Water*10f))/10f;
        materialDeltaText.text = ""+ (Mathf.Round(delta.Material*10f))/10f;

        if(delta.Energy >= 0){
            energyDeltaText.text = "+"+energyDeltaText.text;
            energyDeltaText.color = GM.I.art.yellow;
        }else{
            energyDeltaText.color = GM.I.art.red;
        }
        if(delta.Water >= 0){
            waterDeltaText.text = "+"+waterDeltaText.text;
            waterDeltaText.color = GM.I.art.blue;
        }else{
            waterDeltaText.color = GM.I.art.red;
        }
        if(delta.Material >= 0){
            materialDeltaText.text = "+"+materialDeltaText.text;
            materialDeltaText.color = GM.I.art.brown;
        }else{
            materialDeltaText.color = GM.I.art.red;
        }

        energyProductionText.text = "+"+(Mathf.Round(GM.I.resource.production.Energy*10f))/10f;
        waterProductionText.text = "+"+(Mathf.Round(GM.I.resource.production.Water*10f))/10f;
        materialProductionText.text = "+"+(Mathf.Round(GM.I.resource.production.Material*10f))/10f;

        energyCostText.text = ""+(Mathf.Round(GM.I.resource.cost.Energy*10f))/10f;
        waterCostText.text = ""+(Mathf.Round(GM.I.resource.cost.Water*10f))/10f;
        materialCostText.text = ""+(Mathf.Round(GM.I.resource.cost.Material*10f))/10f;

        lastDelta.r[0] = r.Energy;
        lastDelta.r[1] = r.Water;
        lastDelta.r[2] = r.Material;
    }
}
