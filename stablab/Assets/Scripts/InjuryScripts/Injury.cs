using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

/*
 * The Injury class contains all information that is needed to represent an injury.
 * It has to be Serializable.
 *
 * TODO: make an abstract class
 */

[Serializable]
public abstract class Injury
{
    [NonSerialized]
    public GameObject injuryMarkerObj;
    //public GameObject injuryModelObj;
    protected abstract string MarkerName { get; }
    protected abstract string ModelName { get; }
    protected abstract string IconName { get; }
    protected abstract string ModelPath { get; }

    public Guid Id { get; }
    public Marker Marker { get; protected set; }
    public CameraSettings CameraSettings { get; set; }
    public BodyPose BodyPose { get; set; }
    //public Color Color { get; set; }
    public string InfoText { get; set; }
    public string Name { get; set; }
    public Texture woundIcon { get; set; } //Should be a sprite, as the woundIcons exists as sprites as well.

    public bool isInHole = true;

    public List<byte[]> images = new List<byte[]>();

    protected Injury(Guid id)
    {
        Id = id;
    }

    public string GetModelPath() {
        return ModelPath;
    }

    protected Injury(Injury oldInjury)
    {
        Id = oldInjury.Id;
        Marker = oldInjury.Marker;
        CameraSettings = oldInjury.CameraSettings;
        BodyPose = oldInjury.BodyPose;
        //Color = oldInjury.Color;
        InfoText = oldInjury.InfoText;
        Name = oldInjury.Name;
        images = oldInjury.images;
        isInHole = oldInjury.isInHole;

        if(oldInjury.injuryMarkerObj != null) 
        {
            GameObject newMarkerObj = InstantiateMarker(oldInjury.injuryMarkerObj.transform.position, oldInjury.injuryMarkerObj.transform.rotation, oldInjury.injuryMarkerObj.transform.parent);
            UnityEngine.Object.Destroy(oldInjury.injuryMarkerObj);
            injuryMarkerObj = newMarkerObj;
        }

        if (oldInjury.HasMarker())
        {
            Transform oldModel = oldInjury.Marker.GetParent().transform;
            GameObject newModelObj = InstantiateModel(oldModel.position, oldModel.rotation, oldModel.parent);
            UnityEngine.Object.Destroy(oldInjury.Marker.GetParent());
            Marker.SetParent(newModelObj);
        }
    }


    public GameObject InstantiateMarker(Vector3 pos, Quaternion rot, Transform parent) 
    {
        GameObject marker = UnityEngine.Object.Instantiate((GameObject)Resources.Load(MarkerName), pos, rot);
        marker.transform.parent = parent;
        return marker;
    }

    public GameObject InstantiateModel(Vector3 pos, Quaternion rot, Transform parent)
    {
        GameObject model = UnityEngine.Object.Instantiate((GameObject)Resources.Load(ModelName), pos, rot);
        model.transform.parent = parent;
        return model;
    }

    // Loads the wound Icon if the path to it is given.
    public void loadIcon()
    {
        try
        {
            //TODO: Fixa detta!
            if (IconName != null)
               // woundIcon = Instantiate(new UnityEngine.UI.RawImage(), );
                woundIcon = (Texture)Resources.Load(IconName);
        }
        catch(Exception e)
        {
            Debug.Log("Exception: " + e.ToString());
            Debug.Log("Icon image couldn't be found in \"" + IconName + "\".");
        }
    }

    public GameObject InjuryMarkerObj
    {
        get { return injuryMarkerObj; }

        // When a marker object is added the serializable Marker is also set to match the object.
        set
        {
            injuryMarkerObj = value;
            if(value != null) Marker = new Marker(value);
        }
    }
    /*
    public GameObject InjuryModelObj
    {
        get { return injuryModelObj; }

        // When a model object is added the serializable Marker is also set to match the object.
        set
        {
            injuryModelObj = value;
            if (value != null && Marker != null)
            {
                Marker.ModelUpdate(value);
            }
        }
    }
    */

    // Save current pose
    public void SaveBodyPose()
    {
        BodyPose = ModelController.GetBodyPose();
        if(Marker != null)
        {
            Marker.MarkerDataUpdate(injuryMarkerObj);
            Marker.ModelDataUpdate();
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

    public void AddModel(GameObject model) {
        //GameObject newModel = InstantiateModel(model.transform.position, model.transform.rotation, model.transform.parent);
        Marker.SetParent(model);
        //Marker.SetParent(GameObject.Instantiate(newModel,newModel.transform.position,newModel.transform.rotation,newModel.transform.parent));
        //Marker.parent.SetActive(Marker.active);
        //Marker.InsertModel();
        //Marker.ToggleModel(newModel.active);
    }

    public void RemoveCurrent() {
        if(injuryMarkerObj != null) RemoveInjuryMarker();
        if (Marker != null && Marker.GetParent() != null) Marker.RemoveModel();
    }

    public bool HasMarker() {
        return Marker != null && Marker.GetParent() != null;
    }

    public bool IsSameMarker(GameObject parent) {
        return parent == Marker.GetParent();
    }
    
    public override abstract string ToString();

    public void SetInHole(bool inHole) {
        isInHole = inHole;
    }

}
