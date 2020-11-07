using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BuildingInformation buildingInformation;
    public BuildingMenu buildingMenu;

    public TimeKeeper timeKeeper;

    public PopulationMenu populationMenu;


    public static string HumanNotation(int number){
        string newString = ""+number;
        for (int i = 3; i < newString.Length; i += 4)
        {
            newString = newString.Insert(newString.Length - i, "'");
        }
        return newString;
    }
}
