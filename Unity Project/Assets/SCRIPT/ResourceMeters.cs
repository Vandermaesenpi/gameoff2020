using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeters : MonoBehaviour
{
    public Image energy, water, material;
    public Text energyText, waterText, materialText;

    public void UpdateResources(){
        energy.fillAmount = GM.I.resource.resources.r[(int)R.Energy] / GM.I.resource.resourcesLimit.r[(int)R.Energy];
        water.fillAmount = GM.I.resource.resources.r[(int)R.Water] / GM.I.resource.resourcesLimit.r[(int)R.Water];
        material.fillAmount = GM.I.resource.resources.r[(int)R.Material] / GM.I.resource.resourcesLimit.r[(int)R.Material];
        energyText.text = ""+ (int)GM.I.resource.resources.r[(int)R.Energy];
        waterText.text = ""+ (int)GM.I.resource.resources.r[(int)R.Water];
        materialText.text = ""+ (int)GM.I.resource.resources.r[(int)R.Material];
    }
}
