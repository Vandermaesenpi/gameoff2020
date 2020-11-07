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

    
    // --------------Processed variables
    public float Mood {get{return (needs + culture + comfort + hope)/4f;}}
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
    public int MonthlyBirth{
        get{
            return (int)((float)GetPopulationRange(reproducingAge) * reproducingChance); 
        }
    }

    public int MonthlyDeath;

    // --------------- Debug
    public bool test;

    private void Update() {
        if(test){
            test = false;
            ProcessAging();
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
        int agingPouplationSlice = MonthlyBirth;
        // Reset death counter
        MonthlyDeath = 0;
        // Kill
        for (int i = 0; i < Population.Length; i++)
        {
            int deathInSlice = (int)((float)Population[i] * DeathProbability[i]);
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

}
