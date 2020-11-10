using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Project - ", menuName = "ScriptableObjects/Project", order = 1)]
public class Project : ScriptableObject
{
    public string projectName;
    public int projectLength;
    public string projectDescription;
    public string flavorText;
}
