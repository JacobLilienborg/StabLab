using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Certainty
{
    High,
    Medium,
    Low
};

[Serializable]
public class InjuryData : AbstractTest
{
    [NonSerialized]
    private GameObject injuryMarkerObj;

    private MarkerData markerData;
    private CameraData cameraData;
    private ModelData modelPose;

    private Certainty certainty;
    private string infoText;

    //private List<Image> images = new List<Image>();

    public void SetInjuryMarker(GameObject markerObj) 
    {
        injuryMarkerObj = markerObj;
        markerData = markerObj.GetComponent<MarkerData>();
    }

    public MarkerData GetInjuryMarker() 
    {
        return markerData;
    }

    public void SetCameraData(CameraData cam) 
    {
        cameraData = cam;
    }

    public CameraData GetCameraData() 
    {
        return cameraData;
    }

    public void SetModelPose(ModelData pose) 
    {
        modelPose = pose; 
    }

    public ModelData GetModelPose() 
    {
        return modelPose;
    }

    public void SetCertainty(Certainty c) 
    {
        certainty = c; 
    }

    public Certainty GetCertainty() 
    {
        return certainty;
    }

    public void SetInfoText(string text) 
    {
        infoText = text; 
    }
    public string GetInfoText() 
    {
        return infoText;
    }

    public void AddImage() { }
    public void RemoveImage() { }

    override public string GetPath()
    {
        return "injury";
    }
}
