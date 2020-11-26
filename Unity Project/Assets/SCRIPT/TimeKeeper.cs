using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    public Text clock;
    public Image clockBar;
    public Image border;
    public Slider travelIndicator;
    public GameObject playButton, pauseButton;
    public Toggle pauseToggle, playToggle, fastToggle;
    public void UpdateClock(int time, float smoothTime){
        int month = time % 12;
        int year = (time-month)/12;
        
        clock.text = UIManager.TimeToDate(time);
        clockBar.fillAmount = (smoothTime + (float)month)/12f;

        travelIndicator.value = (float)time/(float)GM.I.gameplay.travelLenght;
    }

    public void PauseButton(bool value){
        if(value){
            GM.I.gameplay.PauseTime(true);
            GM.I.sfx.Play(SFX.Pause);
        }
    }

    public void PlayButton(bool value){
        if(value){
            GM.I.gameplay.PauseTime(false);
            GM.I.sfx.Play(SFX.Play);
        }
    }

    public void FastButton(bool value){
        if(value){
            GM.I.gameplay.FastTime();
            GM.I.sfx.Play(SFX.PlayFast);
        }
    }


    public void UpdatePausedStatus(){
        if(GM.I.gameplay.timePaused){
            clock.color = GM.I.art.red;
            border.color = GM.I.art.red;
            clockBar.color = GM.I.art.red;
            pauseToggle.SetIsOnWithoutNotify(true);
            playToggle.SetIsOnWithoutNotify(false);
            fastToggle.SetIsOnWithoutNotify(false);
        }else if (GM.I.gameplay.currentSpeed == GM.I.gameplay.timeSpeed){
            clock.color = GM.I.art.light;
            border.color = GM.I.art.light;
            clockBar.color = GM.I.art.light;
            pauseToggle.SetIsOnWithoutNotify(false);
            playToggle.SetIsOnWithoutNotify(true);
            fastToggle.SetIsOnWithoutNotify(false);
        }
        else if (GM.I.gameplay.currentSpeed == GM.I.gameplay.fastTimeSpeed){
            clock.color = GM.I.art.greenLight;
            border.color = GM.I.art.greenLight;
            clockBar.color = GM.I.art.greenLight;
            pauseToggle.SetIsOnWithoutNotify(false);
            playToggle.SetIsOnWithoutNotify(false);
            fastToggle.SetIsOnWithoutNotify(true);
        }
    }

    
}
