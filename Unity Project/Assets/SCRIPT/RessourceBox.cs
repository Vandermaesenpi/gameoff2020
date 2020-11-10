using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceBox : MonoBehaviour
{
    public GameObject energy, water, material;
    public Text energyText, waterText, materialText;
    public void UpdateRessourceBox(Resource delta){
        ProcessBox(energy, energyText, GM.I.art.yellow, delta.r[0]);
        ProcessBox(water, waterText, GM.I.art.blue, delta.r[1]);
        ProcessBox(material, materialText, GM.I.art.brown, delta.r[2]);
    }

    void ProcessBox(GameObject box, Text text, Color goodColor, float value){
        if(value > 0){
            box.SetActive(true);
            text.text = "+" + Mathf.Round(value*10f)/10f;
            text.color = goodColor;
        }else if(value < 0){
            box.SetActive(true);
            text.text = ""+Mathf.Round(value*10f)/10f;
            text.color = GM.I.art.red;
        }else{
            box.SetActive(false);
        }
    }
}
