using UnityEngine;
using System;
using System.Collections.Generic;

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

    public Guid Id { get; }
    public Marker Marker { get; protected set; }
    public CameraSettings CameraSettings { get; set; }
    public BodyPose BodyPose { get; set; }
    public string InfoText { get; set; }
    public string Name { get; set; }

    public List<byte[]> images = new List<byte[]>();

    protected Injury(Guid id)
    {
        Id = id;

    }

    protected Injury(Injury oldInjury)
    {
        Id = oldInjury.Id;
        Marker = oldInjury.Marker;
        CameraSettings = oldInjury.CameraSettings;
        BodyPose = oldInjury.BodyPose;
        InfoText = oldInjury.InfoText;
        Name = oldInjury.Name;
        images = oldInjury.images;

        if(oldInjury.injuryMarkerObj != null) 
        {
            Transform oldMarker = oldInjury.injuryMarkerObj.transform;
            GameObject newMarkerObj = InstantiateMarker(oldMarker.position, oldMarker.rotation, oldMarker.transform.parent);
            UnityEngine.Object.Destroy(oldInjury.injuryMarkerObj);
            injuryMarkerObj = newMarkerObj;
        }

        if (oldInjury.HasMarker())
        {
            GameObject oldModel = oldInjury.Marker.GetWeaponModel();
            GameObject newModelObj = InstantiateModel(oldModel.transform.position, oldModel.transform.rotation, oldModel.transform.parent);
            newModelObj.GetComponent<InjuryModelGizmos>().gizmo = oldModel.GetComponent<InjuryModelGizmos>().gizmo;
            UnityEngine.Object.Destroy(oldModel);
            Marker.SetWeaponModel(newModelObj);
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

    public GameObject InjuryMarkerObj
    {
        get { return injuryMarkerObj; }

        // When a marker object is added the serializable Marker is also set to match the object.
        set
        {
            injuryMarkerObj = value;
            if (value != null && Marker == null)
            {
                Marker = new Marker(value);
            } else if(value != null) 
            {
                Marker.MarkerDataUpdate(value);
            }
        }
    }

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
        UnityEngine.Object.Destroy(injuryMarkerObj);
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
        Marker.SetWeaponModel(model);
    }

    public void RemoveCurrent() {
        if(injuryMarkerObj != null) RemoveInjuryMarker();
        if (Marker != null && Marker.GetWeaponModel() != null) Marker.RemoveModel();
    }

    public bool HasMarker() {
        return Marker != null && Marker.GetWeaponModel() != null;
    }

    public bool IsSameMarker(GameObject parent) {
        return parent == Marker.GetWeaponModel();
    }
    
    public override abstract string ToString();
}
