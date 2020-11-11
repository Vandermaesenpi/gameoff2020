using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Project - ", menuName = "ScriptableObjects/Project", order = 1)]
public class Project : ScriptableObject
{
    public string projectName;
    public Vector3Int projectLength;
    [TextArea]
    public string projectDescription;
    public string effectDescription;
    public Sprite sprite;

    public Resource monthlyCost;
    public EffectType type;
    public int resourceType;
    public Vector3 amount;
}
