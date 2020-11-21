using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
     [TextArea]
     public string tip;

     public Vector2 offset;
     public void OnPointerEnter(PointerEventData eventData)
     {
         GM.I.tooltip.ShowTooltip(tip, offset);
     }

     public void OnPointerExit(PointerEventData eventData)
     {
         GM.I.tooltip.HideTooltip();
     }
}
