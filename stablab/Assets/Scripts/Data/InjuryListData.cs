﻿using System.Collections.Generic;
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
        foreach(InjuryController injuryController in InjuryManager.instance.injuries)
        {
            injuries.Add(injuryController.injury);
        }
    }

    public override void Update()
    {
        foreach(Injury injury in injuries)
        {
            InjuryManager.instance.CreateInjury(injury);
            // Temporary!!
            InjuryManager.instance.PlaceInjury(null, null);
        }
    }

}
