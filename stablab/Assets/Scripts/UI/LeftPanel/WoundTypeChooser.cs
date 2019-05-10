using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundTypeChooser : MonoBehaviour
{
    private InjuryType injuryType = InjuryType.Undefined;

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

    public void checkUndefined()
    {
        injuryType = InjuryType.Undefined;
    }

    public void SaveChecked()
    {
        InjuryManager.TransformActive(injuryType);
    }

}
