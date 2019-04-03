using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public List<GameObject> popups;

    public void ShowPopup(GameObject popup)
    {
        foreach (GameObject single in popups)
        {
            if (single == popup)
            {
                single.SetActive(true);
            }
            else {
                single.SetActive(false);
            }
        }
    }

}
