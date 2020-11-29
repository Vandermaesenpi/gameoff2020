using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Time : EventChecker
{
    public override void Check()
    {
        if(GM.I.gameplay.currentTime > threshold && !triggered){
            TriggerEvent();
        }
        base.Check();
    }
}
