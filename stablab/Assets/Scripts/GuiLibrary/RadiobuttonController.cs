using UnityEngine;
using UnityEngine.EventSystems;

public class RadiobuttonController : CheckboxController
{
    private GameObject RadioGroup = null;

    private new void Start()
    {
        base.Start();
        if (transform.parent.name == "RadioGroup") RadioGroup = transform.parent.gameObject;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hello");
        if (mode == Mode.Highlighted)
        {
            foreach (Transform child in RadioGroup.transform)
            {
                RadiobuttonController btn = child.GetComponent<RadiobuttonController>();
                if (btn != null && btn != this && btn.mode != Mode.Disabled)
                {
                    btn.mode = Mode.Inactive;
                    btn.OnUnchecked.Invoke();
                }
            }
            mode = Mode.Active;
            OnChecked.Invoke();
        }
        else if (mode == Mode.Active)
        {
            mode = Mode.Highlighted;
            OnUnchecked.Invoke();
        }
    }
}
