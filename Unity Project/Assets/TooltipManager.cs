using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public Camera uiCamera;
    public Text tooltipText;
    public RectTransform background;
    public Vector2 offset;
    public Vector2 additionnalOffset;

    public void ShowTooltip(string tooltipString, Vector2 offset){
        additionnalOffset = offset;
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize*3f, tooltipText.preferredHeight + textPaddingSize*2f);
        background.sizeDelta = backgroundSize;

    }

    private void LateUpdate() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        localPoint += offset + additionnalOffset;
        transform.localPosition = localPoint;
    }

    public void HideTooltip(){
        gameObject.SetActive(false);
    }
}
