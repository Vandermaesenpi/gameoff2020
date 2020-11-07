using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public BuildingSpot spotStart, spotEnd;

    public BuildingSpot GetOther(BuildingSpot building){
        if(building == spotStart){
            return spotEnd;
        }else{
            return spotStart;
        }
    }
}
