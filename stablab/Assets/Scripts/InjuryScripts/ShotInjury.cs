using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShotInjury : Injury
{
    protected override string MarkerName { get { return "Markers/ShotMarker"; } }
    protected override string ModelName { get { return modelName; } }
    public string modelName = "Models/Shot/shotModel";
    public static readonly string modelPath = "Models/Shot";

    public ShotInjury(Guid id) : base(id)
    {

    }
    public ShotInjury(Injury injury) : base(injury)
    {

    }
    public override string ToString()
    {
        return "Shot";
    }
}
