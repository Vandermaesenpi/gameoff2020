using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    public Text clock;
    public Image clockBar;
    public Slider travelIndicator;
    public GameObject playButton, pauseButton;
    public void UpdateClock(int time, float smoothTime){
        int month = time % 12;
        int year = (time-month)/12;
        string clockText = "";
        switch (month)
        {
            case 0:
            clockText += "jan ";
            break;
            case 1:
            clockText += "feb ";
            break;
            case 2:
            clockText += "mar ";
            break;
            case 3:
            clockText += "apr ";
            break;
            case 4:
            clockText += "may ";
            break;
            case 5:
            clockText += "jun ";
            break;
            case 6:
            clockText += "jul ";
            break;
            case 7:
            clockText += "aug ";
            break;
            case 8:
            clockText += "sep ";
            break;
            case 9:
            clockText += "oct ";
            break;
            case 10:
            clockText += "nov ";
            break;
            case 11:
            clockText += "dec ";
            break;
        }
        clockText += "" + (2150 + year);

        clock.text = clockText;
        clockBar.fillAmount = smoothTime;

        travelIndicator.value = (float)time/(float)GM.I.gameplay.travelLenght;
    }

    public void PauseButton(){
        GM.I.gameplay.PauseTime(true);
    }

    public void PlayButton(){
        GM.I.gameplay.PauseTime(false);
    }

    public void UpdatePausedStatus(){
        playButton.SetActive(GM.I.gameplay.timePaused);
        pauseButton.SetActive(!GM.I.gameplay.timePaused);
        if(GM.I.gameplay.timePaused){
            clock.color = GM.I.art.red;
            clockBar.color = GM.I.art.red;
        }else{
            clock.color = GM.I.art.light;
            clockBar.color = GM.I.art.gray;
        }
    }
}
