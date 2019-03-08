using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Certainty
{
    High,
    Medium,
    Low
};

[Serializable]
public class InjuryData
{
    [NonSerialized]
    public GameObject injuryMarkerObj;

    public MarkerData markerData;
    public CameraData cameraData;
    public ModelData modelPose;

    private Certainty certainty;
    public string infoText;
}
