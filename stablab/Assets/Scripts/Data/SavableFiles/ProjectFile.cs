﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ProjectFile : Savable
{
    private string projectName;
    private string projectPath;
    private readonly float projectVersion;
    private readonly DateTime created;
    private DateTime modified;
    private DateTime saved;

    public List<InjuryData> injuryData;
    public ModelData modelData = null;



    public ProjectFile(string projectName, string projectDirectory, float projectVersion) : base(projectName + ".cvz", projectDirectory)
    {
        this.projectName = projectName;
        this.projectPath = Path.Combine(projectDirectory, projectName);
        this.projectVersion = projectVersion;
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

}