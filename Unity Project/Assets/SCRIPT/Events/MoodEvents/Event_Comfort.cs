using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Comfort : EventChecker
{
    public override void Check()
    {
        if(GM.I.people.comfort < threshold && !triggered){
            TriggerEvent();
        }
        if(triggered && resetValue < GM.I.people.comfort){
            Reset();
        }
        base.Check();
    }
}
