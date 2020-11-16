using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Needs : EventChecker
{
    public override void Check()
    {
        if(GM.I.people.needs < threshold && !triggered){
            TriggerEvent();
        }
        if(triggered && resetValue < GM.I.people.needs){
            Reset();
        }
        base.Check();
    }
}
