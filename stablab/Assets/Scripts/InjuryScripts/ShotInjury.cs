using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShotInjury : Injury
{
    protected override string MarkerName { get { return "Markers/ShotMarker"; } }
    protected override string ModelName { get { return modelName; } }
    protected override string IconName { get { return "Icons/Shot"; } }
    protected override string ModelPath { get { return modelPath; } }
    public string modelName = "Models/Shot/stickModel";
    public static readonly string modelPath = "Models/Shot";


    public ShotInjury(Guid id) : base(id)
    {
        loadIcon();
    }
    public ShotInjury(Injury injury) : base(injury)
    {
        loadIcon();
    }
    public override string ToString()
    {
        return "Shot";
    }
}
