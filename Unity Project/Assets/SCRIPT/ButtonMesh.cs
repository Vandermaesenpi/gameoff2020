using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonMesh : MonoBehaviour
{
    public GameObject normal;
    public GameObject highlight;
    public GameObject click;

    public UnityEvent onClic;

    private void OnEnable() {
        if(highlight != null){
            highlight.SetActive(false);
        }
        if(normal != null){
            normal.SetActive(true);
        }
        if(click != null){
            click.SetActive(false);
        }
    }

    private void OnMouseEnter() {
        if(highlight != null){
            highlight.SetActive(true);
        }
        if(normal != null){
            normal.SetActive(false);
        }
        if(click != null){
            click.SetActive(false);
        }
    }

    private void OnMouseExit() {
        if(highlight != null){
            highlight.SetActive(false);
        }
        if(normal != null){
            normal.SetActive(true);
        }
        if(click != null){
            click.SetActive(false);
        }
    }

    private void OnMouseDown() {
        onClic.Invoke();
        if(highlight != null){
            highlight.SetActive(false);
        }
        if(normal != null){
            normal.SetActive(false);
        }
        if(click != null){
            click.SetActive(true);
        }
    }

}
