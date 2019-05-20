using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjuryStats : MonoBehaviour
{

    public Text index;
    public Text injuryName;
    public Text woundType;

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
