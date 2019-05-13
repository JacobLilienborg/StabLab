using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InjuryImage : UnityEngine.UI.RawImage, IPointerClickHandler
{
    
    // When the image is pressed, the resize method is invoked.
    public void OnPointerClick(PointerEventData eventData)
    {
        FullScreenImageShower.instance.show(this);
        //resize();
    }

}
