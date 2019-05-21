using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjurySettings : MonoBehaviour
{
    public InputField injuryName;
    public Text woundType;
    public Text position;
    public Image model;
    public InputField info;
    public ImagesHandler imagesHandler;
    public Text index; // New

    private InjuryController activeInjury;

    private string nameDefault;
    private string woundTypeDefault;
    private string positionDefault;
    private Color modelDefault;
    private string infoDefault;
    private CameraSettings camDefault;
    private BodyPose poseDefault;

    private void Awake()
    {
        nameDefault = injuryName.text;
        woundTypeDefault = woundType.text;
        positionDefault = position.text;
        modelDefault = model.color;
        infoDefault = info.text;
        camDefault = new CameraSettings(Camera.main);
        //poseDefault = ModelController.GetBodyPose();

        //InjuryManager.instance.AddActivationListener(LoadActiveInjury);
        InjuryManager.instance.OnChange.AddListener(LoadActiveInjury);
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

    public void LoadActiveInjury()
    {
        activeInjury = InjuryManager.instance.activeInjury;
        if (activeInjury != null)
        {
            index.text = InjuryManager.instance.injuries.FindIndex(x => x == activeInjury).ToString();
            LoadNameText(activeInjury);
            LoadTypeText(activeInjury);
            LoadPositionText(activeInjury);
            LoadModelText(activeInjury);
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
        InjuryManager.instance.activeInjury.SetName(name);
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
        if (activeInjury.injuryData.name == null)
        {
            injuryName.text = nameDefault;
        }

        else
        {
            injuryName.text = activeInjury.injuryData.name;
        }
    }

    private void LoadTypeText(InjuryController activeInjury)
    {
        if (activeInjury.injuryData == null)
        {
            woundType.text = woundTypeDefault;
        }
        else
        {
            woundType.text = activeInjury.injuryData.ToString();
        }
    }

    private void LoadPositionText(InjuryController activeInjury)
    {
        if(activeInjury.GetBoneName() == null)
        {
            position.text = positionDefault;
        }
        else
        {
            position.text = activeInjury.GetBoneName();
        }
    }

    private void LoadModelText(InjuryController activeInjury)
    {
        Color empty = new Color(0, 0, 0, 0);
        if (activeInjury.GetColor() == empty)
        {
            model.color = modelDefault;
        }
        else
        {
            model.color = activeInjury.GetColor();
        }
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
       // Camera.main.transform.position = activeInjury.injuryData.cameraData.transformData.position;
       // Camera.main.transform.rotation = activeInjury.injuryData.cameraData.transformData.rotation;
    }

    private void LoadPose(InjuryController activeInjury)
    {

    }
}
