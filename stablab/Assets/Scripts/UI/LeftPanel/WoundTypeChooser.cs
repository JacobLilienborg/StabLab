using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundTypeChooser : MonoBehaviour
{
    private InjuryType injuryType = InjuryType.Undefined;
    public GameObject InOutOption;
    private bool inHole = true;

    public void checkShot()
    {
        injuryType = InjuryType.Shot;
        InOutOption.SetActive(true);
    }

    public void checkCrush()
    {
        injuryType = InjuryType.Crush;
        InOutOption.SetActive(false);
    }

    public void checkCut()
    {
        injuryType = InjuryType.Cut;
        InOutOption.SetActive(false);
    }

    public void checkStab()
    {
        injuryType = InjuryType.Stab;
        InOutOption.SetActive(true);
    }

    public void checkUndefined()
    {
        injuryType = InjuryType.Undefined;
        InOutOption.SetActive(false);
    }

    public void SetInOrOut(bool inHole) {
        this.inHole = inHole;
    }

    public void SaveChecked()
    {
        InjuryManager.TransformActive(injuryType,inHole);
    }

}
