using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoonRotator : MonoBehaviour
{
    public bool canMove = true;
    public Vector3 mousePos;
    public float mouseSensitivity;
    public Transform target;
    public float lerpSpeed;
    public EventSystem eventSystem;
    public bool interactable = true;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !eventSystem.IsPointerOverGameObject()&&interactable){
            Vector3 delta = Input.mousePosition - mousePos;
            target.RotateAround(Vector3.zero, Vector3.down,delta.x * mouseSensitivity);
            target.RotateAround(Vector3.zero, Vector3.right,delta.y * mouseSensitivity);
        }
        mousePos = Input.mousePosition;
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * lerpSpeed);
    }
    public void ShowBuildingSpot(Transform spot){
        target.rotation = Quaternion.FromToRotation(spot.localPosition, Vector3.back);
    }
}
