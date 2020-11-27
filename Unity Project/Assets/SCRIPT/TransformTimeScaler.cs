using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTimeScaler : MonoBehaviour
{
    Vector3 startScale;
    public float scaleFactor;
    public bool inverted = false;
    private void Start() {
        startScale = transform.localScale;
        if(inverted){
            transform.localScale = Vector3.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(inverted){
            transform.localScale = startScale * Mathf.Clamp01(-scaleFactor+ 1f + scaleFactor * ((float)GM.I.gameplay.currentTime + GM.I.gameplay.monthTime)/(float)GM.I.gameplay.travelLenght);
        }else{
            transform.localScale = startScale * Mathf.Clamp01(1f+scaleFactor * ((float)GM.I.gameplay.currentTime + GM.I.gameplay.monthTime)/(float)GM.I.gameplay.travelLenght);
        }
    }
}
