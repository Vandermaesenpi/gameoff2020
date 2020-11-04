using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform target;
    public LineRenderer line;

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            line.SetPosition(1,target.position);
        }else{
            line.SetPosition(1,line.GetPosition(0));
        }
    }
}
