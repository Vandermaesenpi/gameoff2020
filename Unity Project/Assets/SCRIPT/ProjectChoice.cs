using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectChoice : MonoBehaviour
{
    public Text text;
    public Image image;
    public Image selectedImage;
    public Project project;
    public void Init(Project _project, BuildingSpot spot){
        project = _project;
        text.text = project.projectName;
        image.sprite = project.sprite;
        selectedImage.enabled = spot.currentProject == project;
    }

    public void Clic(){
        GM.I.ui.buildingInformation.SelectProject(project);
    }
}
