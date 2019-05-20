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
    public static ModelView modelView = ModelView.ActiveVisible;
    public static string screenShotFilePath;
    public static bool hightTrackerActivated = true;

    public static SettingsFile settingsFile;

    private static UnityEvent settingsConfirmedEvent = new UnityEvent();


    private void Start()
    {
        screenShotFilePath = DataManager.instance.GetWorkingDirectory();
    }

    public static void AddSettingsConfirmedListener(UnityAction action)
    {
        settingsConfirmedEvent.AddListener(action);
    }

    private void OnDisable()
    {
        //InvokeSettingsEvent();
    }

    public void SetHeightTracking(bool activated)
    {
        hightTrackerActivated = activated;
    }

    public void SetScreenshotPath(InputField screenshotPath)
    {
        if(screenshotPath.text != "") screenShotFilePath = screenshotPath.text;
    }

    public static bool IsActiveModel(bool selected) {
        switch (modelView) {
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
                modelView = ModelView.AllVisible;
                break;
            case 1:
                modelView = ModelView.ActiveVisible;
                break;
            case 2:
                modelView = ModelView.NonVisible;
                break;
            default:
                modelView = ModelView.ActiveVisible;
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
