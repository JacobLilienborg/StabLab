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
    private GameObject injuryMarkerObj;

    public DateTime Id { get; }
    public MarkerData MarkerData { get; protected set; }
    public CameraData CameraData { get; set; }
    public BodyData BodyPose { get; set; }
    public Certainty Certainty { get; set; }
    public string InfoText { get; set; }

    //private List<Image> images = new List<Image>();

    public InjuryData(DateTime id) 
    {
        Id = id;
    }

    public GameObject InjuryMarkerObj
    {
        get { return injuryMarkerObj; }

        set
        {
            injuryMarkerObj = value;
            MarkerData = new MarkerData(value);
        }

    }
}
