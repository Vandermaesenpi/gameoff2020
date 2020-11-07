using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationMenu : MonoBehaviour
{
    public GameObject populationOverview;
    public Text totalText;
    public Text totalOverviewText;
    public Text workerText;
    public Text idleText;
    public Text birthText;
    public Text deathText;

    public List<Image> agePyramid;

    public void ClicPopulationMenu(){
        bool value = !gameObject.activeInHierarchy;
        if(value && GM.I.ui.buildingMenu.gameObject.activeInHierarchy){
            GM.I.ui.buildingMenu.ClicBuildingMenu();
        }
        gameObject.SetActive(value);
        populationOverview.SetActive(!value);
    }

    public void UpdateMenu(){

        totalText.text = UIManager.HumanNotation(GM.I.people.TotalPopulation);
        totalOverviewText.text = totalText.text;
        workerText.text = UIManager.HumanNotation(GM.I.people.WorkingPopulation);
        birthText.text = UIManager.HumanNotation(GM.I.people.MonthlyBirth);
        deathText.text = UIManager.HumanNotation(GM.I.people.MonthlyDeath);

        float maxAmount = 0;
        for (int i = 0; i < agePyramid.Count; i++)
        {
            agePyramid[i].fillAmount = (float)GM.I.people.GetPopulationRange(i*5, (i+1)*5)/100000000000f;
            maxAmount = Mathf.Max(maxAmount, agePyramid[i].fillAmount);
        }

        for (int i = 0; i < agePyramid.Count; i++)
        {
            agePyramid[i].fillAmount = agePyramid[i].fillAmount/maxAmount;
        }
    }
}
