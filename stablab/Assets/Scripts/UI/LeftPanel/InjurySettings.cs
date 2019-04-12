using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjurySettings : MonoBehaviour
{
    public InputField injuryName;
    public Text woundType;
    public Text position;
    public Text certainty;
    public InputField info;
    public ImagesHandler imagesHandler;
    public Text index; // New

    private Injury injury;

    private CameraSettings camDefault; // Needed until default cam exists
    private BodyPose poseDefault; // Needed until default pose exists


    private void Awake()
    {
        camDefault = new CameraSettings(Camera.main);
        //poseDefault = ModelController.GetBodyPose();
    }

    /*
    private string nameDefault;
    private string woundTypeDefault;
    private string positionDefault;
    private string certaintyDefault;
    private string infoDefault;


    private void OnEnable()
    {
        if (InjuryManager.activeInjury != null)
        {
            activeInjury = InjuryManager.activeInjury;
            LoadActiveInjury();
        }
    }

    private void Update()
    {
        if(InjuryManager.activeInjury != activeInjury) 
        {
            LoadActiveInjury();
        }
    }

    */

    
    public void LoadInjury(Injury injury)
    {
        index.text = InjuryManager.injuries.IndexOf(injury).ToString();
        this.injury = injury;
        LoadNameText();
        LoadTypeText();
        LoadPositionText();
        LoadCertaintyText();
        LoadImages();
        LoadInfoText();
        LoadImages();
        LoadCamera();
        LoadPose();
    }

    public void SetName(string name)
    {
        injury.Name = name;
    }

    public void SetInfo(string info)
    {
        injury.InfoText = info;
    }

    public void SetCamera()
    {
        injury.CameraSettings = new CameraSettings(Camera.main);
    }

    public void SetPose()
    {
        injury.SaveBodyPose();
    }

    private void LoadNameText()
    {
        if (!string.IsNullOrEmpty(injury.Name))
        {
            injuryName.text = injury.Name;
        }
    }

    private void LoadTypeText()
    {
        if (injury.Type != InjuryType.Null)
        {
            woundType.text = injury.Type.ToString();
        }
    }

    private void LoadPositionText()
    {
        if (injury.Marker != null)
        {
            position.text = injury.Marker.BodyPartParent;
        }
    }

    private void LoadCertaintyText()
    {
        if (injury.Certainty != Certainty.Null)
        {
            certainty.text = injury.Certainty.ToString();
        }
    }

    private void LoadImages()
    {
        imagesHandler.LoadAllImages();
    }

    private void LoadInfoText()
    {
        if (!string.IsNullOrEmpty(injury.InfoText))
        {
            info.text = injury.InfoText;
        }
    }

    private void LoadCamera() 
    {
        CameraSettings newCam = camDefault;
        if(injury.CameraSettings != null) 
        {
            newCam = injury.CameraSettings;
        }

        Camera.main.transform.position = newCam.GetPosition();
        Camera.main.transform.rotation = newCam.GetRotation();
        Camera.main.fieldOfView = newCam.GetFieldOfView();
    }

    private void LoadPose()
    {
        BodyPose newPose = poseDefault;
        if (injury.BodyPose != null)
        {
            newPose = injury.BodyPose;
        }

        ModelController.SetBodyPose(newPose);
    }
}
