using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simple script that hides the button when an Injury is deactivated.
public class ShowPanelButton : MonoBehaviour
{
    void Start()
    {
        InjuryManager.instance.AddDeactivationListener(Hide);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
