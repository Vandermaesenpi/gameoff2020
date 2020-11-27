using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public string linkedAchievement;
    public Text nameText;
    public GameObject icon, lockIcon, iconHolder, hint;
    public Color color;

    private void OnEnable() {
        if(PlayerPrefs.GetInt(linkedAchievement, 0) == 1){
            iconHolder.SetActive(true);            
            hint.SetActive(false);
            icon.SetActive(true);
            lockIcon.SetActive(false);
        }else{
            nameText.text = "??????";
            nameText.color = color;
            iconHolder.SetActive(true);            
            hint.SetActive(false);
            icon.SetActive(false);
            lockIcon.SetActive(true);
        }
    }

    public void ShowHint(bool value){
        hint.SetActive(value);
        iconHolder.SetActive(!value);
    }

}
