using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Show(description);
        Debug.Log("SHOW");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Hide();
        Debug.Log("HIDE");
    }
}
