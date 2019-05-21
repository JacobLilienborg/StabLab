//
// Created by Martin Jirenius
//


using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;
    private string workingDirectory;                // A string of where to save the files
    private AppDataFile applicationData;
    //private SettingsFile settings;

    // Start is called before the first frame update
    private void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            workingDirectory = instance.workingDirectory;
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);


        applicationData = LoadApplicationData();
    }

    public string GetWorkingDirectory()
    {
        return workingDirectory;
    }

    public void SetWorkingDirectory(string directory)
    {
        workingDirectory = directory;
    }

    public SettingsFile LoadSettings()
    {
        SettingsFile settings;
        try
        {
            settings = FileManager.Load<SettingsFile>(Path.Combine(Application.persistentDataPath, "Settings"));
            Debug.Log("Settings was loaded successfully");
        }
        catch (FileNotFoundException)
        {
            settings = new SettingsFile(Application.persistentDataPath);
            FileManager.Save(settings, settings.GetPath());
            Debug.Log("Settings couldn't be found and is therefore created");
        }
        return settings;
    }

    public void LoadProject()
    {
        try
        {
            string path = FileManager.OpenFileBrowser("cvz", applicationData.recentWorkingDirectory);
            ProjectFile proj = FileManager.Load<ProjectFile>(path);
            SetWorkingDirectory(proj.GetDirectory());
            ProjectManager.instance.Open(proj);
            AddToRecent(proj);
        }
        catch (FileNotFoundException)
        {
            Debug.Log("Couldn't open selected project, the file was not found");
        }
    }

    public AppDataFile LoadApplicationData()
    {
        AppDataFile appData;
        try
        {
            appData = FileManager.Load<AppDataFile>(Path.Combine(Application.persistentDataPath, "AppData"));
            Debug.Log("AppData was loaded successfully");
        }
        catch (FileNotFoundException)
        {
            appData = new AppDataFile(Application.persistentDataPath);
            FileManager.Save(appData, appData.GetPath());
            Debug.Log("AppData couldn't be found and is therefore created");
        }
        catch (SerializationException) 
        {
            appData = new AppDataFile(Application.persistentDataPath);
            FileManager.Save(appData, appData.GetPath());
            Debug.Log("AppData couldn't be found and is therefore created");
        }
        return appData;
    }

    // Save the current state of each data file and save it to each file path
    public void Save()
    {
        SaveProject();
        FileManager.Save(Settings.data, Settings.data.GetPath());
        FileManager.Save(applicationData, applicationData.GetPath());
    }

    public void SaveProject()
    {
        Debug.Log("Saving" + ProjectManager.instance.currentProject.GetPath());
        ProjectManager.instance.SetProjectData();
        FileManager.Save(ProjectManager.instance.currentProject, ProjectManager.instance.currentProject.GetPath());
    }

    private void AddToRecent(ProjectFile proj)
    {
        if (applicationData.recentProjects.Contains(proj.GetPath()))
        {
            applicationData.recentProjects.Remove(proj.GetPath());
        }
        applicationData.recentProjects.Insert(0, proj.GetPath());
    }

}
