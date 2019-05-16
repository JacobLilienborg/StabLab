using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjuryStats : MonoBehaviour
{

    public Text index;
    public Text injuryName;

    private void Update()
    {
        if (InjuryManager.activeInjury != null)
        {
            index.text      = InjuryManager.injuries.IndexOf(InjuryManager.activeInjury).ToString();
            if (InjuryManager.activeInjury.Name != null)
                injuryName.text = InjuryManager.activeInjury.Name;
            else
                injuryName.text = "Wound " + index.text;
        }
    }

}
