using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectData
{
    private string projectName = "";
    private string projectPath = "";
    private float projectVersion;
    private DateTime created;
    private DateTime modified;
    private DateTime saved;

    public ProjectData(string name, string path, float version)
    {
        projectName = name;
        projectPath = path;
        projectVersion = version;
        created = DateTime.Now;
        modified = created;
        saved = created;
    }

    public void Touch()
    {
        modified = DateTime.Now;
    }

    public void Save()
    {
        modified = DateTime.Now;
        saved = modified;
    }

    public void SetName(string name)
    {
        projectName = name;
    }

    public string GetName()
    {
        return projectName;
    }

    public void SetPath(string path)
    {
        projectPath = path;
    }

    public string GetPath()
    {
        return projectPath;
    }
}
