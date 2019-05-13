using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CutInjury : Injury
{
    protected override string MarkerName { get { return "Markers/CutMarker"; } }
    protected override string ModelName { get { return modelName; } }
    protected override string IconName { get { return "Icons/Cut"; } }
    public string modelName                 = "Models/Cut/cutModel";
    public static readonly string modelPath = "Models/Cut";

    public CutInjury(Guid id) : base(id)
    {
        loadIcon();
    }
    public CutInjury(Injury injury) : base(injury)
    {
        loadIcon();
    }
    public override string ToString()
    {
        return "Cut";
    }
}
