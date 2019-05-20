using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{

}


public class InjuryButton : RadiobuttonController
{
    public int        index = 0;
    public RawImage   iconImage;

    public MyIntEvent OnCheckedInjury   = new MyIntEvent();
    public MyIntEvent OnUncheckedInjury = new MyIntEvent();

    [SerializeField] private TMP_Text indexText;

    public override void Checked(bool trigger = true)
    {
        base.Checked(trigger);
        if(trigger) OnCheckedInjury.Invoke(index);
    }

    public override void Unchecked(bool trigger = true)
    {
        base.Unchecked(trigger);
        if (trigger) OnUncheckedInjury.Invoke(index);
    }

    public void SetIndex(int index)
    {
        this.index     = index;
        indexText.text = (index + 1).ToString(); // People don't tend to count from 0.
    }

    // Sets the icon texture to a given image.
    public void setImage(Texture texture)
    {
        //Destroy(iconImage.texture);
        if(texture != null)
            iconImage.texture = texture;
    }

}
    
