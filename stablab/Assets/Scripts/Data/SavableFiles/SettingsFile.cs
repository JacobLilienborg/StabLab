using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SettingsFile : Savable
{
    public ModelView modelView = ModelView.ActiveVisible;
    public string screenShotFilePath;
    public bool hightTrackerActivated = true;
    public bool invertedControls;
    public bool tooltipEnabled = true;


    public SettingsFile(string path) : base("Settings", path)
    {
        Debug.Log("Creating settings data");
    }

}
