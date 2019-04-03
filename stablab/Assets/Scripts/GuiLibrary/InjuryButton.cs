using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}

public class InjuryButton : RadiobuttonController
{
    public int id;
    public MyIntEvent OnCheckedInjury = new MyIntEvent();
    public MyIntEvent OnUncheckedInjury = new MyIntEvent();
    public LeftPanelAnimation panelAnimation;

    public override void Checked(bool trigger = true)
    {
        base.Checked(trigger);
        if(trigger) OnCheckedInjury.Invoke(id);
        if(panelAnimation != null) panelAnimation.OpenPanel();

    }

    public override void Unchecked(bool trigger = true)
    {
        base.Unchecked(trigger);
        if(trigger) OnUncheckedInjury.Invoke(id);

        if (panelAnimation != null)panelAnimation.ClosePanel();
    }
}
    
