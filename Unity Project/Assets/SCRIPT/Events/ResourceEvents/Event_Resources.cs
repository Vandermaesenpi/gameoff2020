using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Resources : EventChecker
{
    public int resourceType;
    public override void Check()
    {
        if(GM.I.resource.resources.r[resourceType] < threshold && !triggered){
            TriggerEvent();
        }
        if(triggered && resetValue < GM.I.resource.resources.r[resourceType]){
            Reset();
        }
        base.Check();
    }
}
