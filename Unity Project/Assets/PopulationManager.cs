using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public int[] population = new int[100];
    public int totalPopulation{
        get{
            int total = 0;
            for (int i = 0; i < population.Length; i++)
            {
                total += population[i];
            }
            return total;
        }
    }
    public int ablePopulation{
        get{
            int total = 0;
            for (int i = 17; i < 64; i++)
            {
                total += population[i];
            }
            return total;
        }
    }
}
