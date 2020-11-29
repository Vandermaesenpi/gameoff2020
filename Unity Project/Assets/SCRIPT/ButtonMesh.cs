using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonMesh : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject normal;
    public GameObject highlight;
    public GameObject click;

    public UnityEvent onClic;
    public UnityEvent onEnter;
    public UnityEvent onExit;
    

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
        if(eventSystem.IsPointerOverGameObject()){return;}
        onEnter.Invoke();
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
        onExit.Invoke();
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
        if(eventSystem.IsPointerOverGameObject()){return;}
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
