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

    private Injury activeInjury;

    private string nameDefault;
    private string woundTypeDefault;
    private string positionDefault;
    private string certaintyDefault;
    private string infoDefault;
    private CameraSettings camDefault;
    private BodyPose poseDefault;

    private void Awake()
    {
        nameDefault = injuryName.text;
        woundTypeDefault = woundType.text;
        positionDefault = position.text;
        certaintyDefault = certainty.text;
        infoDefault = info.text;
        camDefault = new CameraSettings(Camera.main);
        poseDefault = ModelController.GetBodyPose();
    }

    private void OnEnable()
    {
        if (InjuryManager.activeInjury != null)
        {
            activeInjury = InjuryManager.activeInjury;
            LoadActiveInjury(false);
        }
    }

    private void Update()
    {
        if(InjuryManager.activeInjury != activeInjury)
        {
            LoadActiveInjury(true);
        }
    }

    public void LoadActiveInjury(bool withCamera)
    {
        if (InjuryManager.activeInjury != null)
        {
            index.text = InjuryManager.injuries.IndexOf(InjuryManager.activeInjury).ToString();
            activeInjury = InjuryManager.activeInjury;
            LoadNameText();
            LoadTypeText();
            LoadPositionText();
            LoadCertaintyText();
            LoadImages();
            LoadInfoText();
            LoadImages();
            LoadPose();
            if(withCamera) LoadCamera();
        }
    }

    public void RemoveModel()
    {
        InjuryManager.activeInjury.Marker.RemoveModel();
    }

    public void UpdateName(string name)
    {
        activeInjury.Name = name;
    }

    public void UpdateInfo(string info)
    {
        activeInjury.InfoText = info;
    }

    public void SetCamera()
    {
        activeInjury.CameraSettings = new CameraSettings(Camera.main);
    }

    public void SetPose()
    {
        activeInjury.SaveBodyPose();
    }

    private void LoadNameText()
    {
        if (!string.IsNullOrEmpty(activeInjury.Name))
        {
            injuryName.text = activeInjury.Name;
        }
        else
        {
            injuryName.text = nameDefault;
        }

    }

    private void LoadTypeText()
    {
        if (activeInjury.Type != InjuryType.Null)
        {
            woundType.text = activeInjury.Type.ToString();
        }
        else
        {
            woundType.text = woundTypeDefault;
        }
    }

    private void LoadPositionText()
    {
        if (activeInjury.Marker != null)
        {
            position.text = activeInjury.Marker.BodyPartParent;
        }
        else
        {
            position.text = positionDefault;
        }
    }

    private void LoadCertaintyText()
    {
        if (activeInjury.Certainty != Certainty.Null)
        {
            certainty.text = activeInjury.Certainty.ToString();
        }
        else
        {
            certainty.text = certaintyDefault;
        }
    }

    private void LoadImages()
    {
        imagesHandler.LoadAllImages();
    }

    private void LoadInfoText()
    {
        if (!string.IsNullOrEmpty(activeInjury.InfoText))
        {
            info.text = activeInjury.InfoText;
        }
        else
        {
            info.text = infoDefault;
        }
    }

    private void LoadCamera()
    {
        CameraSettings newCam = camDefault;
        if(activeInjury.CameraSettings != null)
        {
            newCam = activeInjury.CameraSettings;
        }

        Camera.main.transform.position = newCam.GetPosition();
        Camera.main.transform.rotation = newCam.GetRotation();
        Camera.main.fieldOfView = newCam.GetFieldOfView();
    }

    private void LoadPose()
    {
        BodyPose newPose = poseDefault;
        if (activeInjury.BodyPose != null)
        {
            newPose = activeInjury.BodyPose;
        }

        ModelController.SetBodyPose(newPose);
    }
}
