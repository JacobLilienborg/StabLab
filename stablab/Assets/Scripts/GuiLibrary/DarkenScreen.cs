using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This object darkens the screen, and hides the full screen image when pressed.
/// </summary>
public class DarkenScreen : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        FullScreenImageShower.instance.hide();
        this.gameObject.SetActive(false);
    }

}
