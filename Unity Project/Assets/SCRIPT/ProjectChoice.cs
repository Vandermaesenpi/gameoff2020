using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectChoice : MonoBehaviour
{
    public Text text;
    public Image image;
    public Image selectedImage;
    public List<GameObject> levelMarkers;
    public GameObject additionnalInfo;
    public Image time;
    public Tooltip tooltip;
    public Project project;
    public void Init(Project _project, BuildingSpot spot){
        project = _project;
        text.text = project.projectName;
        image.sprite = project.sprite;
        selectedImage.enabled = spot.currentProject == project;
        tooltip.tip = project.effectDescription;
        foreach (GameObject marker in levelMarkers)
        {
            marker.SetActive(false);
        }
        additionnalInfo.SetActive(!GM.I.project.IsConstant(project));
        if(GM.I.project.IsConstant(project)){
            time.fillAmount = 0f;
        }else{
            for (var i = 1; i <= GM.I.project.GetLevel(project); i++)
            {
                if(levelMarkers.Count > i){
                    levelMarkers[i].SetActive(true);
                }
            }
            time.fillAmount = (float)GM.I.project.GetTime(project)/(float)GM.I.project.GetLength(project);
        }
    }

    public void Clic(){
        GM.I.ui.buildingInformation.SelectProject(project);
    }
}
