using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event - ", menuName = "ScriptableObjects/Event", order = 1)]
public class Event : ScriptableObject
{
    
}

public enum EffectType{
    Production,
    Energy,
    Water,
    Material,
    Cost,
    Integrity,
    BasicNeeds,
    Comfort,
    Culture,
    Hope,
    Space,
    Birth,
    Death,
    Idle
}
