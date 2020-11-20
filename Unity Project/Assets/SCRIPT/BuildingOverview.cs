using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOverview : MonoBehaviour
{
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,Time.deltaTime * rotateSpeed, 0);
    }
}
