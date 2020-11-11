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
    public float needsModifier, cultureModifier, comfortModifier, hopeModifier;
    
    // --------------Processed variables
    public float Mood {get{return (needs*7f + culture + comfort + hope)/10f;}}
    public int GetPopulationRange(int min, int max){
        int total = 0;
            for (int i = min * 12; i < max * 12; i++)
            {
                total += Population[i];
            }
            return total;
    }
    public int GetPopulationRange(Vector2Int minMax){
        return GetPopulationRange(minMax.x, minMax.y);
    }
    public int TotalPopulation{
        get{
            return GetPopulationRange(0, Population.Length/12);
        }
    }
    public int WorkingPopulation{
        get{
            return GetPopulationRange(workingAge);
        }
    }

    public int IdlePopulation{
        get{
            return Mathf.Max(0, WorkingPopulation - GM.I.city.WorkplaceSpace());
        }
    }
    public int MonthlyBirth{
        get{
            return (int)(((float)GetPopulationRange(reproducingAge) * reproducingChance) * Mood); 
        }
    }

    public int MonthlyDeath;

    public bool Growing{
        get{
            return MonthlyBirth >= MonthlyDeath;
        }
    }

    // --------------- Game functions

    private void Awake() {
        // Translate animation curves
        for (int i = 0; i < Population.Length; i++)
        {
            float t = (float)i/(float)Population.Length;
            Population[i] = (int)(populationCurve.Evaluate(t) * 500000f);
            DeathProbability[i] = deathProbabilityCurve.Evaluate(t);
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
            float deathFactor = 1f;
            deathFactor = (GM.I.city.ResourceShortage()? 500f: 0f);
            int deathInSlice = (int)((float)Population[i] * DeathProbability[i] + deathFactor);
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
    }

    void ProcessNeeds(){
        needs = 0;
        int threshold = TotalPopulation/200000 + 1;
        needs += (0.33f)*(Mathf.Clamp(GM.I.resource.resources.r[0]/(float)threshold, 0f,1f));
        needs += (0.33f)*(Mathf.Clamp(GM.I.resource.resources.r[1]/(float)threshold, 0f,1f));
        needs += (0.33f)*(Mathf.Clamp(GM.I.resource.resources.r[2]/(float)threshold, 0f,1f));
        
        if(needs > 0.98f){
            needs = 1;
        }
    }

    void ProcessComfort(){
        comfort = Mathf.Clamp((float)GM.I.city.HousingSpace()/(float)TotalPopulation + comfortModifier -0.5f, 0f, 1f);
    }

    void ProcessCulture(){

    }

    void ProcessHope(){
        
    }

}
