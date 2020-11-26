using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event - ", menuName = "ScriptableObjects/Event", order = 1)]
public class Event : ScriptableObject
{
    public string eventName;
    [TextArea]
    public string description;
    public List<EventChoice> choices;
}

public enum FXT{
    Production,
    Energy,
    Water,
    Material,
    Cost,
    Integrity,
    Needs,
    Comfort,
    Culture,
    Hope,
    Space,
    Birth,
    Death,
    Idle,
    None
}
