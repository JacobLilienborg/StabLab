using UnityEngine;
using System;

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

    public Guid Id { get; }
    public Marker Marker { get; protected set; }
    public CameraSettings CameraSettings { get; set; }
    public BodyPose BodyPose { get; set; }
    public Certainty Certainty { get; set; }
    public string InfoText { get; set; }

    //private List<Image> images = new List<Image>();

    public Injury(Guid id)
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
}
