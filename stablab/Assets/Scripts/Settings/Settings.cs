using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum ModelView{
    AllVisible,
    ActiveVisible,
    NonVisible
};

public class Settings : MonoBehaviour
{
    public static SettingsFile data;

    private static UnityEvent settingsConfirmedEvent = new UnityEvent();


    private void Start()
    {
        data = DataManager.instance.LoadSettings();
        //data.screenShotFilePath = DataManager.instance.GetWorkingDirectory();
    }

    public static void AddSettingsConfirmedListener(UnityAction action)
    {
        settingsConfirmedEvent.AddListener(action);
    }

    private void OnDisable()
    {
        //InvokeSettingsEvent();
    }

    public void SetInvertedButtons(bool active)
    {
        MovementButtons.InvertedControls(active);
    }

    public void SetHeightTracking(bool activated)
    {
        data.hightTrackerActivated = activated;
    }

    public void SetScreenshotPath(InputField screenshotPath)
    {
        if(screenshotPath.text != "") data.screenShotFilePath = screenshotPath.text;
    }

    public static bool IsActiveModel(bool selected) {
        switch(data.modelView) {
            case ModelView.AllVisible:
                return true;
            case ModelView.ActiveVisible:
                return selected;
            case ModelView.NonVisible:
                return false;
            default:
                return false;
        }
    }

    public void InvokeSettingsEvent()
    {
        settingsConfirmedEvent.Invoke();
    }

    public void SetModelView(int index) {
        switch (index) {
            case 0:
                data.modelView = ModelView.AllVisible;
                break;
            case 1:
                data.modelView = ModelView.ActiveVisible;
                break;
            case 2:
                data.modelView = ModelView.NonVisible;
                break;
            default:
                data.modelView = ModelView.ActiveVisible;
                break;

        }
        UpdateAllModels();
    }

    public static void UpdateAllModels() {
        foreach (InjuryController injuryController in InjuryManager.instance.injuries) {

        }
    }

    public void ExitProgram()
    {
        Application.Quit();
    }
}
