using System.Collections.Generic;
using System;
using UnityEngine;

/*
 * A list of injuries that can be saved on the computer
 */

[Serializable]
public class InjuryListData : AData
{
    private List<Injury> injuries = new List<Injury>();

    public InjuryListData() : base("injuryList", "Injuries")
    {
        Debug.Log("Creating injurylist data");
        injuries = InjuryManager.injuries;
    }

    public override void Update()
    {
        InjuryManager.injuries = injuries;
    }

}
