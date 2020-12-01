using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Jobs : EventChecker
{
    public override void Check()
    {
        if(GM.I.people.Unemployement > threshold && !triggered){
            TriggerEvent();
        }
        base.Check();
    }
}
