using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}

public class InjuryButton : RadiobuttonController
{
    public int index = 0;

    public MyIntEvent OnCheckedInjury = new MyIntEvent();
    public MyIntEvent OnUncheckedInjury = new MyIntEvent();

    [SerializeField] private Text indexText;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        indexText.text = index.ToString();
    }

    public override void Checked(bool trigger = true)
    {
        base.Checked(trigger);
        if(trigger) OnCheckedInjury.Invoke(index);
    }

    public override void Unchecked(bool trigger = true)
    {
        base.Unchecked(trigger);
        if(trigger) OnUncheckedInjury.Invoke(index);
    }
}
    
