using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    public BuildingObject currentBuilding;
    public GameObject positionRing;
    public GameObject positionPoint;
    public GameObject statusRing;
    public GameObject selectionRing;

    private void OnMouseEnter() {
        selectionRing.SetActive(true);
    }

    private void OnMouseExit() {
        selectionRing.SetActive(false);
    }
}
