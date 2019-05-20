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

    private InjuryController activeInjury;

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
        //poseDefault = ModelController.GetBodyPose();
    }

    private void OnEnable()
    {
        if (InjuryManager.instance.activeInjury != null)
        {
            activeInjury = InjuryManager.instance.activeInjury;
            LoadActiveInjury(false);
        }
    }

    private void Update()
    {
        if(InjuryManager.instance.activeInjury != activeInjury)
        {
            LoadActiveInjury(true);
        }
    }

    public void LoadActiveInjury(bool withCamera)
    {
        if (InjuryManager.instance.activeInjury != null)
        {
            index.text = InjuryManager.instance.injuries.IndexOf(InjuryManager.instance.activeInjury).ToString();
            activeInjury = InjuryManager.instance.activeInjury;
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

    }

    public void UpdateName(string name)
    {
        activeInjury.injuryData.name = name;
    }

    public void UpdateInfo(string info)
    {
        activeInjury.injuryData.infoText = info;
    }

    public void SetCamera()
    {
    }

    public void SetPose()
    {
    }

    private void LoadNameText()
    {


    }

    private void LoadTypeText()
    {
       woundType.text = activeInjury.ToString();
    }

    private void LoadPositionText()
    {

    }

    private void LoadCertaintyText()
    {
        /*
        if (activeInjury.Certainty != Certainty.Null)
        {
            certainty.text = activeInjury.Certainty.ToString();
        }
        else
        {
            certainty.text = certaintyDefault;
        }
        */
    }

    private void LoadImages()
    {
        imagesHandler.LoadAllImages();
    }

    private void LoadInfoText()
    {

    }

    private void LoadCamera()
    {

    }

    private void LoadPose()
    {

    }
}
