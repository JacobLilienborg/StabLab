using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private string description, clickedDescription = "";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (description != "")
            TooltipManager.Show(description, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickedDescription != "")
            TooltipManager.Show(clickedDescription, true);
    }

}
