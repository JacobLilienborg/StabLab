using System;
using UnityEngine;

[Serializable]
public class ProjectData : AData
{
    private string projectName;
    private string projectDirectory;
    private readonly float projectVersion;
    private readonly DateTime created;
    private DateTime modified;
    private DateTime saved;

    /*
    public ProjectData() : base("default", "Data")
    {
        new ProjectData("default", "Data", 0.0f);
        
    }
    */   

    public ProjectData(string projectName, string projectDirectory, float projectVersion) : base("project.sl", "")
    {
        this.projectName = projectName;
        this.projectDirectory = projectDirectory;
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

    public void SetDirectory(string directory)
    {
        projectDirectory = directory;
    }

    public string GetDirectory()
    {
        return projectDirectory;
    }

    public override void Update()
    {
        //throw new NotImplementedException();
    }

    public override void Load()
    {
        //throw new NotImplementedException();
    }
}
