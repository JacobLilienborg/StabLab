using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InjuryListData : AData
{
    private List<Injury> injuries = new List<Injury>();

    public InjuryListData() : base("injuryList", "Injuries")
    {
        Debug.Log("Creating innjurylist data");
        injuries = InjuryManager.injuries;
    }

    public override void Update()
    {
        InjuryManager.injuries = injuries;
    }

}
