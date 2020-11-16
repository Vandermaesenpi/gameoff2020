using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Hope : EventChecker
{
    public override void Check()
    {
        if(GM.I.people.hope < threshold && !triggered){
            TriggerEvent();
        }
        if(triggered && resetValue < GM.I.people.hope){
            Reset();
        }
        base.Check();
    }
}
