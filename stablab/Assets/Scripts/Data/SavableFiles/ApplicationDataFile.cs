using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AppDataFile : Savable
{
    public string recentWorkingDirectory;
    public List<string> recentProjects = new List<string>();


    public AppDataFile(string path) : base("AppData", path)
    {
        Debug.Log("Creating AppData data");
    }
}
