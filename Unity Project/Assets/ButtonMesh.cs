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
        highlight.SetActive(false);
        normal.SetActive(true);
        click.SetActive(false);
    }

    private void OnMouseEnter() {
        highlight.SetActive(true);
        normal.SetActive(false);
        click.SetActive(false);
    }

    private void OnMouseExit() {
        highlight.SetActive(false);
        normal.SetActive(true);
        click.SetActive(false);
    }

    private void OnMouseDown() {
        onClic.Invoke();
        highlight.SetActive(false);
        normal.SetActive(false);
        click.SetActive(true);
    }

}
