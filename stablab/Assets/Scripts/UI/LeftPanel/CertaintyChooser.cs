using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CertaintyChooser : MonoBehaviour
{
    private Certainty certainty = Certainty.Null;

    public void checkHigh()
    {
        certainty = Certainty.High;
    }

    public void checkMedium()
    {
        certainty = Certainty.Medium;
    }

    public void checkLow()
    {
        certainty = Certainty.Low;
    }

    public void SaveChecked()
    {
        InjuryManager.activeInjury.Certainty = certainty;
    }
}
