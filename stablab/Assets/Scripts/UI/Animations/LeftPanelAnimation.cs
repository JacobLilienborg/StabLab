using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelAnimation : MonoBehaviour
{
    public GameObject Panel;
    public GameObject primaryPage;
    private bool open;
    private ObjectChooser objChooser;

    private void Start()
    {
        objChooser = Panel.GetComponent<ObjectChooser>();
        InjuryManager.instance.AddActivationListener(OpenPanel);
        InjuryManager.instance.AddDeactivationListener(ClosePanel);
    }

    public void OpenPanel() {
        if (Panel != null) {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null && !animator.GetBool("isOpen")) {
                animator.SetBool("isOpen", true);
                open = true;
            }
        }
        objChooser.ShowObject(primaryPage);
    }

    public void ClosePanel()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null && animator.GetBool("isOpen"))
            {
                animator.SetBool("isOpen", false);
                open = false;
            }
        }

    }

    public bool IsOpen() {
        return open;
    }
}
