using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundTypeChooser : MonoBehaviour
{
    private InjuryType injuryType = InjuryType.Null;

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

    public void SaveChecked()
    {
        InjuryManager.activeInjury.Type = injuryType;
    }

}
