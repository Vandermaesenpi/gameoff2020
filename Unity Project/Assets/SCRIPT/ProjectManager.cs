using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public List<Project> projects;
    public List<int> levels;
    public List<int> time;

    public int GetTime(Project project){
        return time[projects.IndexOf(project)];
    }
    public int GetLevel(Project project){
        return levels[projects.IndexOf(project)];
    }

    public bool IsConstant(Project project){
        return project.projectLength.x < 0;
    }
    public bool IsMaxed(Project project){
        return levels[projects.IndexOf(project)] == 3;
    }
    public float FX(FXT type){
        foreach (Project p in projects)
        {
            if(p.type == type){
                if(IsConstant(p)){
                    return p.amount[0] * GetLevel(p);
                }else{
                    if(GetLevel(p) == 0){
                        return 1f;
                    }else if(GetLevel(p) == 4){
                        return p.amount[2];
                    }else{
                        return p.amount[GetLevel(p)-1];
                    }
                }
            }
        }
        
        return 1f;
    }
    public int GetLength(Project project){
        if(GetLevel(project)<3){
            return project.projectLength[GetLevel(project)];
        }
        return 0;
    }

    public void UpdateProjects(){
        
        for (int i = 0; i < projects.Count; i++)
        {
            if(IsConstant(projects[i])){
                levels[i] = 0;
            }
        }

        // Process research
        foreach (BuildingSpot spot in GM.I.city.buildings)
        {
            if(spot.currentProject != null){
                int index = projects.IndexOf(spot.currentProject);
                if(!spot.currentProject.monthlyCost.Limited(GM.I.resource.resources)){
                    if(IsConstant(spot.currentProject)){
                        levels[index] += 1;
                    }else{
                        time[index]++;
                    }
                }
            }
        }

        // Process finished research
        for (int i = 0; i < projects.Count; i++)
        {
            if(!IsConstant(projects[i])){
                if(levels[i] < 4){
                    if(GetLength(projects[i]) <= time[i]){
                        levels[i]++;
                        time[i] = 0;
                    }
                }
            }
        }

        // Stop finished projects
        foreach (BuildingSpot spot in GM.I.city.buildings)
        {
            if(spot.currentProject != null){
                int index = projects.IndexOf(spot.currentProject);
                if((time[index] == 0 || levels[index] == 4) && !IsConstant(projects[index])){
                    spot.currentProject = null;
                }
            }
        }

        
    }

    
}
