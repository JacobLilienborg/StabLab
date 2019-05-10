using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CrushInjury : Injury
{
    protected override string MarkerName { get { return "Markers/CrushMarker"; } }
    protected override string ModelName { get { return modelName; } }
    public string modelName = "Models/Crush/crushModel";
    public static readonly string modelPath ="Models/Crush";

    public CrushInjury(Guid id) : base(id)
    {

    }
    public CrushInjury(Injury injury) : base(injury)
    {

    }
    public override string ToString()
    {
        return "Crush";
    }
}
