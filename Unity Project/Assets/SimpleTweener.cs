using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTweener : MonoBehaviour
{
    public Gradient gradient;
    public Image image;
    // Update is called once per frame
    void Update()
    {
        image.color = gradient.Evaluate(Mathf.Sin(Time.time*10f));
    }
}
