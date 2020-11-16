using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningMenu : MonoBehaviour
{
    public GameObject warningPannel;
    public Text title, description, flavorText;
    public List<WarningChoice> choices;
    public void InitEventMenu(Event e){
        warningPannel.SetActive(true);
        title.text = e.eventName;
        description.text = e.description;
        foreach (WarningChoice choice in choices)
        {
            choice.InitChoice(null);
        }
        for (var i = 0; i < e.choices.Count; i++)
        {
            choices[i].InitChoice(e.choices[i]);
        }
    }

    public void ShowFlavorText(string text){
        flavorText.text = text;
    }
}
