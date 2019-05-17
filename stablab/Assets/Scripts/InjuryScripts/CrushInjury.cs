using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CrushInjury : Injury
{
    protected override string MarkerName { get { return "Markers/CrushMarker"; } }
    protected override string ModelName { get { return modelName; } }
    protected override string IconName { get { return "Icons/Crush"; } }
    protected override string ModelPath { get { return modelPath; } }
    public string modelName = "Models/Crush/crushModel";
    public static readonly string modelPath ="Models/Crush";

    public CrushInjury(Guid id) : base(id)
    {
        loadIcon();
    }
    public CrushInjury(Injury injury) : base(injury)
    {
        loadIcon();
    }
    public override string ToString()
    {
        return "Crush";
    }
}
