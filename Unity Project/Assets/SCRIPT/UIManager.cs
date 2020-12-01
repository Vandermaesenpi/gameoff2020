using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameScreen, winScreen, looseScreen;
    public BuildingInformation buildingInformation;
    public BuildingMenu buildingMenu;
    public TimeKeeper timeKeeper;
    public PopulationMenu populationMenu;
    public ResourceMeters resourceMeters;
    public WarningMenu warningMenu;

    public static string HumanNotation(uint number){
        string newString = ""+number;
        for (int i = 3; i < newString.Length; i += 4)
        {
            newString = newString.Insert(newString.Length - i, "'");
        }
        return newString;
    }
    public static string HumanNotation(int number){
        return HumanNotation((uint)number);
    }

    public static string HumanNotationSigned(int number){
        string newString = ""+Mathf.Abs(number);
        for (int i = 3; i < newString.Length; i += 4)
        {
            newString = newString.Insert(newString.Length - i, "'");
        }
        if(number < 0){
            newString = "-"+newString;
        }else{
            newString = "+"+newString;
        }
        return newString;
    }

    public void ShowWinScreen(){
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
    }
    public void ShowLooseScreen(){
        gameScreen.SetActive(false);
        looseScreen.SetActive(true);
    }

    public void CloseAllUI(){
        if(populationMenu.gameObject.activeInHierarchy){
            populationMenu.ClicPopulationMenu(false);
        }
        if(populationMenu.moodMenu.activeInHierarchy){
            populationMenu.ClicMoodMenu(false);
        }
        if(buildingMenu.gameObject.activeInHierarchy){
            buildingMenu.ClicBuildingMenu(false);
        }
        buildingInformation.ShowBuildingInfo(null);
    }

    public static string HumanNotation(float number){
        string newString = "";
        int percent = (int)(100f * (number/1f));
        newString = "" + percent + "%";
        return newString;
    }
    public static string TimeToDate(int time){
        int month = time % 12;
        int year = (time-month)/12;
        string clockText = "";
        switch (month)
        {
            case 0:
            clockText += "jan ";
            break;
            case 1:
            clockText += "feb ";
            break;
            case 2:
            clockText += "mar ";
            break;
            case 3:
            clockText += "apr ";
            break;
            case 4:
            clockText += "may ";
            break;
            case 5:
            clockText += "jun ";
            break;
            case 6:
            clockText += "jul ";
            break;
            case 7:
            clockText += "aug ";
            break;
            case 8:
            clockText += "sep ";
            break;
            case 9:
            clockText += "oct ";
            break;
            case 10:
            clockText += "nov ";
            break;
            case 11:
            clockText += "dec ";
            break;
        }
        clockText += "" + (2150 + year);
        return clockText;
    }

    public static string ColoredString(string text, Color color){
        return "<color=#"+ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }

}
