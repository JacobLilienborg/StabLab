using UnityEngine;
using UnityEngine.EventSystems;

public class RadiobuttonController : CheckboxController
{
    [SerializeField] protected GameObject RadioGroup = null;

    protected new virtual void Awake()
    {
        base.Awake();
        if (transform.parent != null) RadioGroup = transform.parent.gameObject;
    }

    public override void Checked(bool trigger = true)
    {
        if (RadioGroup)
        {
            foreach (Transform child in RadioGroup.transform)
            {
                RadiobuttonController btn = child.GetComponent<RadiobuttonController>();
                if (btn != null && btn != this && btn.mode != Mode.Disabled)
                {
                    btn.Unchecked(true);
                }
            }
        } 
        else
        {
            Debug.Log("WARNING: There appear to be no parent");
        }
        base.Checked(trigger);
    }
}
