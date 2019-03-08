using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SettingsData
{
    [SerializeField] public static string path;
    public const string defaultFolderName = "StabLab";
    public string workingDirectoryPath;
    public List<string> recentProjects;

    public SettingsData()
    {
        Debug.Log("Creating settings data");
    }

}
