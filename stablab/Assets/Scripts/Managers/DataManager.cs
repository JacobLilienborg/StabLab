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
            Destroy(gameObject);
        }

        //Don't destroy when reloading scene
        DontDestroyOnLoad(gameObject);

        applicationData = LoadApplicationData();

        Debug.Log(applicationData.recentProjects[0]);
        Debug.Log(applicationData.recentProjects[1]);
    }

    private void Start()
    {
        string path = "";
        foreach(string arg in System.Environment.GetCommandLineArgs()) 
        { 
            if (arg.Contains(".cvz")) 
            {
                path = arg;
                break;  
            }
        }

        if (path != "") LoadProject(path);
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

    public void LoadProject(string path = "")
    {
        try
        {
            if(path == "") 
            {
                path = FileManager.OpenFileBrowser("cvz", applicationData.recentWorkingDirectory);
            }
            if (path == "") return;

            ProjectFile proj = FileManager.Load<ProjectFile>(path);
            SetWorkingDirectory(proj.directory);
            AddToRecent(proj);
            ProjectManager.instance.Open(proj);
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
        SaveSettings();
        FileManager.Save(applicationData, applicationData.GetPath());
    }

    public void SaveSettings() 
    {
        if(Settings.data != null) 
        {
            FileManager.Save(Settings.data, Settings.data.GetPath());
            Debug.Log("In Data save settings");
        }
        else
        {   //If settings doesn't exist create a new
            SettingsFile settings = new SettingsFile(Application.persistentDataPath);
            FileManager.Save(settings, settings.GetPath());
        }
    }

    public void SaveProject()
    {
        ProjectManager.instance.SetProjectData();
        FileManager.Save(ProjectManager.instance.currentProject, ProjectManager.instance.currentProject.GetPath());
    }
    
    public void AddToRecent(ProjectFile proj)
    {
        if (applicationData.recentProjects.Contains(proj.GetPath()))
        {
            applicationData.recentProjects.Remove(proj.GetPath());
        }
        applicationData.recentProjects.Insert(0, proj.GetPath());
        applicationData.recentWorkingDirectory = proj.directory;
        //FileManager.Save(applicationData, applicationData.GetPath());
    }

}
