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
    public Text moodText;
    public Text moodOverviewText;
    public Text needText;
    public Text comfortText;
    public Text cultureText;
    public Text hopeText;

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
        totalOverviewText.color = GM.I.people.Growing? GM.I.art.green : GM.I.art.red;
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

        moodText.text = UIManager.HumanNotation(GM.I.people.Mood);
        moodOverviewText.text = moodText.text;
        needText.text = UIManager.HumanNotation(GM.I.people.needs);
        comfortText.text = UIManager.HumanNotation(GM.I.people.comfort);
        cultureText.text = UIManager.HumanNotation(GM.I.people.culture);
        hopeText.text = UIManager.HumanNotation(GM.I.people.hope);

    }
}
