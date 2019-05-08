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

/*
 * The Injury class contains all information that is needed to represent an injury.
 * It has to be Serializable.
 *
 * TODO: make an abstract class
 */

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

        // When a marker object is added the serializable Marker is also set to match the object.
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

    // Add a new image to the injury
    public void AddImage(byte[] image)
    {
        images.Add(image);
    }

    public void ToggleMarker(bool active) {
        injuryMarkerObj.SetActive(active);
    }

    // Return a Texture2D of the image with the specified index
    public Texture2D GetImageTexture(int index)
    {
        Texture2D img = new Texture2D(2, 2);
        img.LoadImage(images[index]);
        return img;
    }

    public void AddModel(GameObject newModel) {
        Marker.SetParent(GameObject.Instantiate(newModel,newModel.transform.position,newModel.transform.rotation,newModel.transform.parent));
        //Marker.parent.SetActive(Marker.active);
        //Marker.InsertModel();
        //Marker.ToggleModel(newModel.active);
    }

    public void RemoveCurrent() {
        if(injuryMarkerObj != null) RemoveInjuryMarker();
        if (Marker != null && Marker.GetParent() != null) ;//Marker.RemoveModel();
    }

    public bool HasMarker() {
        return Marker != null && Marker.GetParent() != null;
    }

    public bool IsSameMarker(GameObject parent) {
        return parent == Marker.GetParent();
    }
}
