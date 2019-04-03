using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundTypeChooser : MonoBehaviour
{
    public LeftPanel leftPanel;
    public GameObject nextState;

    public InjuryType injuryType = InjuryType.Null;
    public string test;

    public void checkShot()
    {
        injuryType = InjuryType.Shot;
    }

    public void checkCrush()
    {
        injuryType = InjuryType.Crush;
    }

    public void checkCut()
    {
        injuryType = InjuryType.Cut;
    }

    public void checkStab()
    {
        injuryType = InjuryType.Stab;
    }

    public void Done()
    {
        InjuryManager.activeInjury.Type = injuryType;
        leftPanel.SetState(nextState);
    }

    public void Reset()
    {
        leftPanel.SetState(nextState);
    }
}
