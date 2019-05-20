using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InjuryStats : MonoBehaviour
{

    public TMP_Text index;
    public TMP_Text injuryName;
    public TMP_Text woundType;

    private void Update()
    {
        if (InjuryManager.activeInjury != null)
        {
            index.text      = InjuryManager.injuries.IndexOf(InjuryManager.activeInjury).ToString();
            woundType.text  = InjuryManager.activeInjury.ToString();
            if (InjuryManager.activeInjury.Name != null)
                injuryName.text = InjuryManager.activeInjury.Name;
            else
                injuryName.text = "Wound " + index.text;
        }
    }

}
