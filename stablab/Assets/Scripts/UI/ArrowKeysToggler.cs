using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowKeysToggler : MonoBehaviour, IPointerClickHandler
{
    public static bool DeactivateArrowKeys = false; // A flag for knowing if the inputfield is pressed

    public void OnPointerClick(PointerEventData eventData) // Sets the bool FieldPressed to true when field is pressed
    {
        DeactivateArrowKeys = true;
    }

    public void FieldExit() // Sets the bool FieldPressed to false when the field is deselected
    {
        DeactivateArrowKeys = false;
    }
}
