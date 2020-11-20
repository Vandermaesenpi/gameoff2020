using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Culture : EventChecker
{
    public override void Check()
    {
        if(GM.I.people.culture < threshold && !triggered){
            TriggerEvent();
        }
        if(triggered && resetValue < GM.I.people.culture){
            Reset();
        }
        base.Check();
    }
}
