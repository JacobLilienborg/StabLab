﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public ProjectManager instance = null;
    private string currentProject;

    // Setup instance of ProjectManager
    void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Don't destroy when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void SaveProject()
    {
        SaveCamera();
    }

    public void LoadProject()
    {
        currentProject = Path.GetFileNameWithoutExtension(FileManager.OpenFolderBrowser());
        if (currentProject.Length > 0)
        {
            AddToRecent();
            LoadCamera();
        }
        else 
        {
            currentProject = "";
        }

    }

    public void SaveCamera()
    {
        CameraData cam = new CameraData(Camera.main);
        FileManager.SaveProjectData(currentProject, "CameraData", cam);
    }

    public void LoadCamera()
    {
        try
        {
            CameraData cam = FileManager.LoadProjectData<CameraData>(currentProject, "CameraData");
            Camera.main.transform.position = cam.GetPosition();
            Camera.main.transform.rotation = cam.GetRotation();
            Camera.main.fieldOfView = cam.GetFieldOfView();
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e);
        }

    }

    public void CreateNewProject(string name)
    {
        string wd = FileManager.GetWorkingDirectory();
        string projPath = Path.Combine(wd, name);
        if (!Directory.Exists(projPath))
        {
            Directory.CreateDirectory(projPath);
            currentProject = name;
        }
    }

    private void AddToRecent()
    {
        SettingsData settings = FileManager.LoadAppData<SettingsData>("Settings");
        if (settings.recentProjects.Contains(currentProject))
        {
            settings.recentProjects.Remove(currentProject);
        }
        settings.recentProjects.Insert(0, currentProject);
        FileManager.SaveAppData("Settings", settings);
    }

}