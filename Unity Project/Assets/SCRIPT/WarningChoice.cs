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
        ProcessEffect(currentChoice.type, currentChoice.amount);
        ProcessEffect(currentChoice.typeSecondary, currentChoice.amountSecondary);

        GM.I.ui.resourceMeters.UpdateResources();
        GM.I.ui.populationMenu.UpdateMenu();
        
    }

    void ProcessEffect(FXT type, float amount){
        switch (type)
        {
            case FXT.BasicNeeds:
                GM.I.people.needs += amount;
                break;
            case FXT.Birth:
                GM.I.people.Population[0] += (int)amount;
                break;
            case FXT.Comfort:
                GM.I.people.comfort += amount;
                break;
            case FXT.Culture:
                GM.I.people.culture += amount;
                break;
            case FXT.Death:
                GM.I.people.Kill(amount);
                break;
            case FXT.Energy:
                GM.I.resource.resources.r[0] += amount;
                break;
            case FXT.Hope:
                GM.I.people.hope += amount;
                break;
            case FXT.Integrity:
                GM.I.city.ModifyIntegrity(amount);
                break;
            case FXT.Material:
                GM.I.resource.resources.r[2] += amount;
                break;
            case FXT.Water:
                GM.I.resource.resources.r[1] += amount;
                break;
            default:
                break;
        }
    }
}
