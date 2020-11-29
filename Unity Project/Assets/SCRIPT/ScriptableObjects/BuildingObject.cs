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
    public string populationName;
    public int ressourceType;
    public List<Project> projects;
    public Resource production;
    public Resource constructionMonthlyCost;
    public Resource storageIncreaseMonthlyCost;
    public int constructionTime;
    public bool productor = true;
    public bool housing = true;
    public bool research = true;
    public bool control = false;
    public int culture;
    public float baseStorage;
    public float decay;
    public string decayEffect;
    public string maintenanceEffect;
    public string costInfo;
    [TextArea]
    public string shortageEffect;
    public AudioClip constructionSound;
}
