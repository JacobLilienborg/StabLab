using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelAnimation : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel() {
        if (Panel != null) {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null && !animator.GetBool("isOpen")) {
                animator.SetBool("isOpen", true);
            }
        }
    }

    public void ClosePanel()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null && animator.GetBool("isOpen"))
            {
                animator.SetBool("isOpen", false);
            }
        }
    }
}
