using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectChoice : MonoBehaviour
{
    public Text text;
    public Image image;
    public Project project;
    public void Init(Project project){
        text.text = project.projectName;
        image.sprite = project.sprite;
    }
}
