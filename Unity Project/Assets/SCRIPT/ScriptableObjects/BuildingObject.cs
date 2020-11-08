using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building - ", menuName = "ScriptableObjects/Building", order = 1)]
public class BuildingObject : ScriptableObject
{
    public string buildingName;
    public string description;
    public Color color;
    public Sprite sprite;
    public GameObject prefab;
    public int populationRequirement;
    public Resource production;
    public Resource constructionMonthlyCost;
    public int constructionTime;
    public bool productor = true;
    public bool housing = true;
    public int maxStorageUpgrade;
}
