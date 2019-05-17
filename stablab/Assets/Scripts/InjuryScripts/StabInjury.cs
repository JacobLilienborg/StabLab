using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StabInjury : Injury
{
    protected override string MarkerName { get { return "Markers/StabMarker"; } }
    protected override string ModelName { get { return modelName; } }
    protected override string IconName { get { return "Icons/Stab"; } }
    protected override string ModelPath { get { return modelPath; } }
    public string modelName = "Models/Stab/stabModel";
    public static readonly string modelPath = "Models/Stab";

    public StabInjury(Guid id) : base(id)
    {
        loadIcon();
    }
    public StabInjury(Injury injury) : base(injury)
    {
        loadIcon();
    }

    public override string ToString() 
    {
        return "Stab"; 
    }
}
