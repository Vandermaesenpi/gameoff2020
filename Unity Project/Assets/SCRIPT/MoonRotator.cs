using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotator : MonoBehaviour
{
    public bool canMove = true;
    public Vector3 mousePos;
    public float mouseSensitivity;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Vector3 delta = Input.mousePosition - mousePos;
            transform.RotateAround(Vector3.zero, Vector3.down,delta.x * mouseSensitivity);
            transform.RotateAround(Vector3.zero, Vector3.right,delta.y * mouseSensitivity);
        }
        mousePos = Input.mousePosition;

    }
}
