using UnityEngine;
using System;
using System.Collections.Generic;

public enum Certainty
{
    Null,
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
    public InjuryType Type { get; set; }
    public CameraSettings CameraSettings { get; set; }
    public BodyPose BodyPose { get; set; }
    public Certainty Certainty { get; set; }
    public string InfoText { get; set; }
    public string Name { get; set; }

    public List<byte[]> images = new List<byte[]>();

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

    // Save current pose
    public void SaveBodyPose()
    {
        BodyPose = ModelController.GetBodyPose();
        if(Marker != null) 
        {
            Marker.MarkerUpdate(InjuryMarkerObj);
        }
    }

    // Add a marker to the injury.
    public void AddInjuryMarker(GameObject markerObj)
    {
        InjuryMarkerObj = markerObj;
        SaveBodyPose();
    }


    public void AddImage(byte[] image) 
    {
        images.Add(image);
    }

    public Texture2D GetImageTexture(int index) 
    {
        Texture2D img = new Texture2D(2, 2);
        img.LoadImage(images[index]);
        return img;
    }
}
