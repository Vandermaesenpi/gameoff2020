using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningChoice : MonoBehaviour
{
    public Text choiceName, choiceEffect;
    public EventChoice currentChoice;
    public void InitChoice(EventChoice data){
        if(data == null){
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(true);
            currentChoice = data;
            choiceName.text = currentChoice.choiceName;
            choiceEffect.text = currentChoice.effectText;
        }
    }

    public void ShowFlavorText(bool onOff){
        if(onOff){
            GM.I.ui.warningMenu.ShowFlavorText(currentChoice.description);
        }else{
            GM.I.ui.warningMenu.ShowFlavorText("");
        }
    }

    public void MakeChoice(){
        GM.I.ui.warningMenu.ShowFlavorText("");
        GM.I.ui.warningMenu.warningPannel.SetActive(false);
        switch (currentChoice.type)
        {
            case FXT.BasicNeeds:
                GM.I.people.needs += currentChoice.amount;
                break;
            case FXT.Birth:
                GM.I.people.Population[0] += (int)currentChoice.amount;
                break;
            case FXT.Comfort:
                GM.I.people.comfort += currentChoice.amount;
                break;
            case FXT.Culture:
                GM.I.people.culture += currentChoice.amount;
                break;
            case FXT.Death:
                GM.I.people.Kill(currentChoice.amount);
                break;
            case FXT.Energy:
                GM.I.resource.resources.r[0] += currentChoice.amount;
                break;
            case FXT.Hope:
                GM.I.people.hope += currentChoice.amount;
                break;
            case FXT.Integrity:
            // Need rework
                break;
            case FXT.Material:
                GM.I.resource.resources.r[2] += currentChoice.amount;
                break;
            case FXT.Water:
                GM.I.resource.resources.r[1] += currentChoice.amount;
                break;
            default:
                break;
        }

        GM.I.ui.resourceMeters.UpdateResources();
        
    }
}
