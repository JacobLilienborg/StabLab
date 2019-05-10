using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StabInjury : Injury
{
    protected override string MarkerName { get { return "Markers/StabMarker"; } }
    protected override string ModelName { get { return modelName; } }
    public string modelName = "Models/Stab/stabModel";
    public static readonly string modelPath = "Models/Stab";

    public StabInjury(Guid id) : base(id)
    {

    }
    public StabInjury(Injury injury) : base(injury)
    {

    }

    public override string ToString() 
    {
        return "Stab"; 
    }
}
