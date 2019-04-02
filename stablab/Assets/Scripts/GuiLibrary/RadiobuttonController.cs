using UnityEngine;
using UnityEngine.EventSystems;

public class RadiobuttonController : CheckboxController
{
    protected GameObject RadioGroup = null;

    protected new virtual void Start()
    {
        base.Start();
        if (transform.parent != null) RadioGroup = transform.parent.gameObject;
    }

    public override void Checked(bool trigger = true)
    {
        if (trigger)
        {
            foreach (Transform child in RadioGroup.transform)
            {
                RadiobuttonController btn = child.GetComponent<RadiobuttonController>();
                if (btn != null && btn != this && btn.mode != Mode.Disabled)
                {
                    btn.Unchecked(false);
                }
            }
        }
        base.Checked(trigger);
    }
}
