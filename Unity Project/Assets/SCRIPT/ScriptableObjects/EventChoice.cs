using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event Choice - ", menuName = "ScriptableObjects/Event Choice", order = 1)]
public class EventChoice : ScriptableObject
{
    public string choiceName;
    public string description;
    public FXT type;
    public float amount;
    public FXT typeSecondary;
    public float amountSecondary;
}