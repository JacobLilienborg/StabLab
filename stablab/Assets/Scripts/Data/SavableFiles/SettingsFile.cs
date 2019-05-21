using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SettingsFile : Savable
{
    public ModelView modelView = ModelView.ActiveVisible;
    public string screenShotFilePath;
    public bool hightTrackerActivated = true;


    public SettingsFile(string path) : base("settings", path)
    {
        Debug.Log("Creating settings data");
    }

}
