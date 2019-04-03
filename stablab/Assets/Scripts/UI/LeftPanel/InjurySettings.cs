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

    private Injury activeInjury;

    private string nameDefault;
    private string woundTypeDefault;
    private string positionDefault;
    private string certaintyDefault;
    private string infoDefault;

    private void Awake()
    {
        nameDefault = injuryName.text;
        woundTypeDefault = woundType.text;
        positionDefault = position.text;
        certaintyDefault = certainty.text;
        infoDefault = info.text;

        Debug.Log("Look at mee i'm in start" + nameDefault);
    }

    private void OnEnable()
    {
        if (InjuryManager.activeInjury != null)
        {
            activeInjury = InjuryManager.activeInjury;
            LoadActiveInjury();
        }
    }

    public void LoadActiveInjury()
    {
        if (InjuryManager.activeInjury != null)
        {
            activeInjury = InjuryManager.activeInjury;
            LoadNameText();
            LoadTypeText();
            LoadPositionText();
            LoadCertaintyText();
            LoadImages();
            LoadInfoText();
        }
    }

    public void UpdateName(string name)
    {
        activeInjury.Name = name;
    }

    public void UpdateInfo(string info)
    {
        activeInjury.InfoText = info;
    }

    private void LoadNameText()
    {
        if (!string.IsNullOrEmpty(activeInjury.Name))
        {
            injuryName.text = activeInjury.Name;
            Debug.Log("Name exissts");
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
        // Call imagesHandler
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
}
