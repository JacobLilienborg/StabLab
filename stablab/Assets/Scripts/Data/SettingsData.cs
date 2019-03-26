using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SettingsData : AData
{
    public List<string> recentProjects;

    public SettingsData() : base("settings", "Data")
    {
        Debug.Log("Creating settings data");
    }

    public override void Update()
    {
        //throw new NotImplementedException();
    }
}
