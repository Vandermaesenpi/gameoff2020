using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    // Animation curves for convenience
    public AnimationCurve populationCurve;
    public AnimationCurve deathProbabilityCurve;

    // --------------Variables
    // Population
    [HideInInspector]
    public int[] Population = new int[1200];
    // Parametters
    [HideInInspector]
    public float[] DeathProbability = new float[1200];
    public Vector2Int workingAge;
    public Vector2Int reproducingAge;
    public float reproducingChance;

    public float needs, culture, comfort, hope;
    public float cultureDecay;
    public float cultureGain;
    public float needsModifier, cultureModifier, comfortModifier, hopeModifier;
    public bool holiday = true;
    
    // --------------Processed variables
    public float Mood {get{return (needs + culture + comfort + hope)/4f;}}

    public float NeedsThreshold{get{return 200 + GM.I.project.FX(FXT.Needs);}}
    public float ComfortThreshold{get{return GM.I.city.HousingSpace()+ GM.I.project.FX(FXT.Comfort);}}
    public float CultureThreshold{get{return (float)(GM.I.city.Culture() * 50000f + GM.I.project.FX(FXT.Culture)+ GM.I.project.FX(FXT.Idle));}}
    public uint GetPopulationRange(int min, int max){
        uint total = 0;
            for (int i = min * 12; i < max * 12; i++)
            {
                total += (uint)Population[i];
            }
            return total;
    }
    public uint GetPopulationRange(Vector2Int minMax){
        return GetPopulationRange(minMax.x, minMax.y);
    }
    public uint TotalPopulation{
        get{
            return GetPopulationRange(0, Population.Length/12);
        }
    }
    public uint WorkingPopulation{
        get{
            return GetPopulationRange(workingAge);
        }
    }

    public uint IdlePopulation{
        get{
            return (uint)Mathf.Max(0, WorkingPopulation - GM.I.city.WorkplaceSpace());
        }
    }
    public int MonthlyBirth{
        get{
            return (int)(((float)GetPopulationRange(reproducingAge) * reproducingChance) * Mood * (1f+GM.I.project.FX(FXT.Birth))); 
        }
    }

    public int MonthlyDeath;

    public bool Growing{
        get{
            return MonthlyBirth >= MonthlyDeath;
        }
    }

    public int GrowthPercentage{
        get{
            return MonthlyBirth - MonthlyDeath;
        }
    }

    public float Unemployement{
        get{
            return (float)IdlePopulation/(float)WorkingPopulation;
        }
    }

    public float UnemployementLimit{
        get{
            return 0.20f;
        }
    }

    // --------------- Game functions

    private void Awake() {
        // Translate animation curves
        for (int i = 0; i < Population.Length; i++)
        {
            float t = (float)i/(float)Population.Length;
            Population[i] = (int)(populationCurve.Evaluate(t) * 70000f);
            DeathProbability[i] = deathProbabilityCurve.Evaluate(t);
        }
    }

    public void Kill(float amount){
        for (int i = 0; i < Population.Length; i++)
        {
            Population[i] = (int)(amount * (float)Population[i]);
        }
    }

    public void ProcessAging(){
        // Calculate births
        int agingPouplationSlice = (GM.I.city.ResourceShortage()? 0: MonthlyBirth);
        // Reset death counter
        MonthlyDeath = 0;
        // Kill
        for (int i = 0; i < Population.Length; i++)
        {
            float deathFactor =  4f-(Mood*3f);
            int deathInSlice = (int)((float)Population[i] * DeathProbability[i] * GM.I.project.FX(FXT.Death) * deathFactor);
            deathInSlice = Mathf.Min(Population[i],deathInSlice);
            Population[i] -= deathInSlice;
            MonthlyDeath += deathInSlice;
        }
        // Age population
        for (int i = 0; i < Population.Length; i++)
        {
            int tempInt = Population[i];
            Population[i] = agingPouplationSlice;
            agingPouplationSlice = tempInt;
        }
        // Kill super old peoples
        MonthlyDeath += agingPouplationSlice;

        GM.I.ui.populationMenu.UpdateMenu();
    }

    public void ProcessMood(){
        ProcessNeeds();
        ProcessComfort();
        ProcessCulture();
        ProcessHope();
        if(culture < 0.2f || needs < 0.2f || comfort < 0.2f){
            holiday = false;
        }
    }

    void ProcessNeeds(){
        float needsDelta = 0;
        float threshold = NeedsThreshold;
        needsDelta += GM.I.resource.resources.Energy/threshold < 1 ? GM.I.resource.resources.Energy/threshold : 1;
        needsDelta += GM.I.resource.resources.Water/threshold < 1 ? GM.I.resource.resources.Water/threshold : 1;
        needsDelta += GM.I.resource.resources.Material/threshold < 1 ? GM.I.resource.resources.Material/threshold : 1;
        
        if(needsDelta < 3f){
            needs -= cultureDecay * (3f-needsDelta);
        }else{
            needs += cultureGain;
        }
        needs = Mathf.Clamp(needs, 0,1);
    }

    void ProcessComfort(){
        if(TotalPopulation  < ComfortThreshold){
            comfort += cultureGain;
        }else if(TotalPopulation  < (1.5f*ComfortThreshold)){
            comfort -= cultureDecay/2f;
        }else{
            comfort -= cultureDecay;
        }
        comfort = Mathf.Clamp(comfort, 0,1);
    }

    void ProcessCulture(){
        float cultureRatio = (float)TotalPopulation/CultureThreshold;
        if(cultureRatio < 1){
            culture += cultureGain;
        }else{
            culture -= cultureDecay;
        }
        culture = Mathf.Clamp(culture, 0,1);
    }

    void ProcessHope(){
        hope = Mathf.Clamp(hope, 0,1) + GM.I.project.FX(FXT.Hope);
    }

}
