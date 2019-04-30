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
    public GameObject injuryMarkerObj;

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
            if(value != null) Marker = new Marker(value,Type);
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
        //ToggleMarker(true);
    }

    public void RemoveInjuryMarker() {
        if (InjuryMarkerObj == null) return;
        GameObject.Destroy(injuryMarkerObj);
        Marker.RemoveMarker();
    }


    public void AddImage(byte[] image) 
    {
        images.Add(image);
    }

    public void ToggleMarker(bool active) {
        injuryMarkerObj.SetActive(active);
    }

    public Texture2D GetImageTexture(int index) 
    {
        Texture2D img = new Texture2D(2, 2);
        img.LoadImage(images[index]);
        return img;
    }

    public void AddModel(GameObject newModel) {
        Marker.model = GameObject.Instantiate(newModel,newModel.transform.position,newModel.transform.rotation,newModel.transform.parent);
        Marker.model.SetActive(true);
        //Marker.InsertModel();
        //Marker.ToggleModel(newModel.active);
    }

    public void RemoveCurrent() {
        if(injuryMarkerObj != null) RemoveInjuryMarker();
        if(Marker != null && Marker.model != null) Marker.RemoveModel();
    }
}
