﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShotInjury : Injury
{
    protected override string MarkerName { get { return "Markers/ShotMarker"; } }
    protected override string ModelName { get { return modelName; } }
    protected override string IconName { get { return "Icons/Shot"; } }
    public string modelName = "Models/Shot/shotModel";
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
