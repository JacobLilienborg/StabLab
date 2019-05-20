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
        if (InjuryManager.instance.activeInjury != null)
        {
            index.text      = InjuryManager.instance.injuries.IndexOf(InjuryManager.instance.activeInjury).ToString();
            woundType.text  = InjuryManager.instance.activeInjury.ToString();
            if (InjuryManager.instance.activeInjury.injuryData.name != null)
                injuryName.text = InjuryManager.instance.activeInjury.injuryData.name;
            else
                injuryName.text = "Wound " + index.text;
        }
    }

}
