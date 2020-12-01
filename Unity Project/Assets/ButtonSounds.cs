 using UnityEngine;
 using UnityEngine.EventSystems;
 
 public class ButtonSounds : MonoBehaviour
     , IPointerClickHandler
     , IPointerEnterHandler
{
    public bool hoverPlay = true;
    public SFX hover;
    public SFX clic;
 
 
     public void OnPointerClick(PointerEventData eventData)
     {
         GM.I.sfx.PlayFaithfull(clic);
     }
 
     
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         if(hoverPlay){
             GM.I.sfx.PlayFaithfull(hover);
         }
     }
 
 }
