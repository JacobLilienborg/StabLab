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

    public MarkerData MarkerData { get; protected set; }
    public CameraData CameraData { get; set; }
    public ModelData ModelPose { get; set; }
    public Certainty Certainty { get; set; }
    public string InfoText { get; set; }

    //private List<Image> images = new List<Image>();

    public GameObject InjuryMarkerObj
    {
        get { return injuryMarkerObj; }

        set
        {
            injuryMarkerObj = value;
            MarkerData = value.GetComponent<MarkerHandler>().MarkerData;
        }

    }
}
