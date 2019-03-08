using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    ProjectData project;
    SettingsData settings;
    CameraData camera;

    public DataManager(ProjectData projectData)
    {
        project = projectData;
        Load();
    }

    public void Save()
    {
        FileManager.Save(settings, SettingsData.path);
        FileManager.Save(project, ProjectData.path);
        FileManager.Save(camera, CameraData.path);
    }
    public void Load()
    {
        //Load Settings
        try
        {
            FileManager.Load<ProjectData>(ProjectData.path);
        }
        catch (FileNotFoundException)
        {
            FileManager.Save(project = new ProjectData(), ProjectData.path);
        }
        //Load Settings
        try
        {
            FileManager.Load<SettingsData>(SettingsData.path);
        }
        catch (FileNotFoundException)
        {
            FileManager.Save(settings = new SettingsData(), SettingsData.path);
        }

        //Load Settings
        try
        {
            FileManager.Load<CameraData>(CameraData.path);
        }
        catch (FileNotFoundException)
        {
            FileManager.Save(camera = new CameraData(), CameraData.path);
        }
    }
    public void Update()
    {

    }
}
