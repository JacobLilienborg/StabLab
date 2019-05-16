using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InjuryData
{   
    public string name { get; set; }
    public Guid id { get; }
    public string infoText { get; set; }
    public List<byte[]> images = new List<byte[]>();
    public PoseData poseData { get; set; }
    public CameraData cameraData { get; set; }
    public MarkerData markerData;
    public WeaponData weaponData;
}
