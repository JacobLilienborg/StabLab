using UnityEngine;
using System;
using System.Collections.Generic;

public enum Certainty
{
    High,
    Medium,
    Low
};

[Serializable]
public class Injury
{
    [NonSerialized]
    private GameObject injuryMarkerObj;

    public DateTime Id { get; }
    public Marker Marker { get; protected set; }
    public CameraSettings CameraSettings { get; set; }
    public BodyPose BodyPose { get; set; }
    public Certainty Certainty { get; set; }
    public string InfoText { get; set; }

    public List<byte[]> images = new List<byte[]>();

    public Injury(DateTime id) 
    {
        Id = id;
    }

    public GameObject InjuryMarkerObj
    {
        get { return injuryMarkerObj; }

        set
        {
            injuryMarkerObj = value;
            Marker = new Marker(value);
        }

    }

    public void AddImage(byte[] image) 
    {
        images.Add(image);
    }
}
