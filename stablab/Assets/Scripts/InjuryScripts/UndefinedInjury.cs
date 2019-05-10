using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UndefinedInjury : Injury
{
    protected override string MarkerName { get { return "Markers/UndefinedMarker"; } }
    protected override string ModelName { get { return modelName; } }
    public string modelName = "Models/Undefined/undefinedModel";
    public static readonly string modelPath = "Models/Undefined";


    public UndefinedInjury(Guid id) : base(id)
    {

    }

    public UndefinedInjury(Injury injury) : base(injury)
    {

    }
    public override string ToString()
    {
        return "Undefined";
    }

}
