using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InjuryListData : AData
{
    private List<InjuryData> injuries = new List<InjuryData>();

    public InjuryListData() : base("injuryList", "Injuries")
    {
        Debug.Log("Creating innjurylist data");
    }

    public InjuryListData(List<InjuryData> list) : base("injuryList", "Injuries")
    {
        injuries = list;
    }

    public override void Update()
    {
        injuries = InjuryManager.injuries;
        Debug.Log("update innjurylist data");
    }

    public override void Load()
    {
        InjuryManager.LoadInjuries(injuries);
        Debug.Log("load innjurylist data");
    }

}
