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

        InjuryManager.instance.AddActivationListener(LoadActiveInjury);
    }

    private void OnEnable()
    {
        if (InjuryManager.instance.activeInjury != null)
        {
            activeInjury = InjuryManager.instance.activeInjury;
            //LoadActiveInjury(false);
        }
    }

    private void Update()
    {
        /*if(InjuryManager.instance.activeInjury != activeInjury)
        {
            //LoadActiveInjury(true);
        }*/
    }

    public void LoadActiveInjury(InjuryController activeInjury)
    {
        if (activeInjury != null)
        {
            index.text = activeInjury.GetText();
            LoadNameText(activeInjury);
            LoadTypeText(activeInjury);
            LoadPositionText(activeInjury);
            LoadCertaintyText(activeInjury);
            LoadImages(activeInjury);
            LoadInfoText(activeInjury);
            LoadImages(activeInjury);
            LoadPose(activeInjury);
            LoadCamera(activeInjury);
        }
    }

    public void RemoveModel()
    {

    }

    public void UpdateName(string name)
    {
        activeInjury.injuryData.name = name;
    }

    public void UpdateInfo(InputField info)
    {
        InjuryManager.instance.activeInjury.SetText(info.text);
    }

    public void SetCamera()
    {
    }

    public void SetPose()
    {
    }

    private void LoadNameText(InjuryController activeInjury)
    {


    }

    private void LoadTypeText(InjuryController activeInjury)
    {
       woundType.text = activeInjury.ToString();
    }

    private void LoadPositionText(InjuryController activeInjury)
    {

    }

    private void LoadCertaintyText(InjuryController activeInjury)
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

    private void LoadImages(InjuryController activeInjury)
    {
        imagesHandler.LoadAllImages();
    }

    private void LoadInfoText(InjuryController activeInjury)
    {
        info.text = activeInjury.GetText();
    }

    private void LoadCamera(InjuryController activeInjury)
    {

    }

    private void LoadPose(InjuryController activeInjury)
    {

    }
}
