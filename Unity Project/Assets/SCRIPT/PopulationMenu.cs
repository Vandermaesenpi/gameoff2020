using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationMenu : MonoBehaviour
{
    public GameObject populationOverview;
    public Text totalText;
    public Text totalOverviewText;
    public Text growthText;
    public Text growthOverviewText;
    public Text unemployementText;
    public Text unemployementOverviewText;
    public Text workerText;
    public Text workerOverviewText;
    uint lastWorkingPopulation;
    public Text idleText;
    public Text birthText;
    int lastBirth;
    public Text deathText;
    int lastDeath;

    public List<Image> agePyramid;
    public Text moodOverviewText;
    public Text needText;
    public Text comfortText;
    public Text cultureText;
    public Text hopeText;
    public Text needTextMenu;
    public Text needEnergyRatioText;
    public Text needWaterRatioText;
    public Text needMaterialRatioText;
    public Text comfortTextMenu;
    public Text comfortRatioText;
    public Text cultureTextMenu;
    public Text cultureRatioText;
    public Text hopeTextMenu;

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
        totalText.color = GM.I.people.Growing? GM.I.art.green : GM.I.art.red;
        growthOverviewText.text = ""+Mathf.Round(10f*Mathf.Pow(GM.I.people.GrowthPercentage,120f))/10f + "%";
        
        if(GM.I.people.GrowthPercentage > 0) { growthOverviewText.text = "+"+growthOverviewText.text;}else{ growthOverviewText.text = "-"+growthOverviewText.text;}
        growthOverviewText.color = GM.I.people.GrowthPercentage > 0? GM.I.art.green : GM.I.art.red;
        growthText.text = growthOverviewText.text;
        growthText.color = growthOverviewText.color;
        unemployementOverviewText.text = UIManager.HumanNotation(GM.I.people.Unemployement);
        if(GM.I.people.Unemployement > GM.I.people.UnemployementLimit){
            unemployementOverviewText.color = GM.I.art.red;
        }else{
            unemployementOverviewText.color = GM.I.art.green;
        }
        unemployementText.text = unemployementOverviewText.text;
        unemployementText.color = unemployementOverviewText.color;
        workerText.text = UIManager.HumanNotation(GM.I.people.WorkingPopulation);
        workerText.color = GM.I.people.WorkingPopulation >= lastWorkingPopulation ? GM.I.art.light : GM.I.art.light;
        workerOverviewText.text = workerText.text;
        workerOverviewText.color = workerText.color;
        lastWorkingPopulation = GM.I.people.WorkingPopulation;
        idleText.text = UIManager.HumanNotation(GM.I.people.IdlePopulation);
        idleText.color = GM.I.people.IdlePopulation < GM.I.people.WorkingPopulation/3 ? GM.I.art.light : GM.I.art.light;
        birthText.text = UIManager.HumanNotation(GM.I.people.MonthlyBirth);
        birthText.color = GM.I.people.MonthlyBirth >= GM.I.people.MonthlyDeath ? GM.I.art.green : GM.I.art.red;
        lastBirth = GM.I.people.MonthlyBirth;
        deathText.text = UIManager.HumanNotation(GM.I.people.MonthlyDeath);
        deathText.color = GM.I.people.MonthlyDeath < GM.I.people.MonthlyBirth? GM.I.art.green : GM.I.art.red;
        lastDeath = GM.I.people.MonthlyDeath;

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

        // moodText.text = UIManager.HumanNotation(GM.I.people.Mood);
        // moodOverviewText.text = moodText.text;
        
        ProcessMood(GM.I.people.needs,needText,needTextMenu);
        ProcessMood(GM.I.people.comfort,comfortText,comfortTextMenu);
        ProcessMood(GM.I.people.culture,cultureText,cultureTextMenu);
        ProcessMood(GM.I.people.hope,hopeText,hopeTextMenu);

        needEnergyRatioText.text = ""+(int)GM.I.resource.resources.Energy + "/" + (int)GM.I.people.NeedsThreshold; 
        needMaterialRatioText.text = ""+(int)GM.I.resource.resources.Material + "/" + (int)GM.I.people.NeedsThreshold; 
        needWaterRatioText.text = ""+(int)GM.I.resource.resources.Water + "/" + (int)GM.I.people.NeedsThreshold; 

        comfortRatioText.text = ""+GM.I.people.TotalPopulation+"/"+GM.I.people.ComfortThreshold;
        cultureRatioText.text = ""+GM.I.people.TotalPopulation+"/"+GM.I.people.CultureThreshold;

    }

    void ProcessMood(float mood, Text text, Text text2){
        Color color = Color.white;
        if(mood < 0.25f){
            color = GM.I.art.red;
        }else if(mood < 0.5f){
            color = GM.I.art.orange;
        }else if(mood < 0.75f){
            color = GM.I.art.yellowLight;
        }else{
            color = GM.I.art.green;
        }
        text.text = UIManager.HumanNotation(mood);
        text.color = color;
        text2.text = text.text;
        text2.color = text.color;
    }
}
