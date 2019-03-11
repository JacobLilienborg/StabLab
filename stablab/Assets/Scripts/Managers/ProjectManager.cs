using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private float projectVersion = 0.1f;
    private ProjectManager instance = null;
    private ProjectData currentProject;
    private DataManager dataManager;

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

    //Creates a new project data. 
    public void Create(string name, string directory)
    {
        currentProject = new ProjectData(name, directory, projectVersion);
        dataManager = new DataManager(directory, currentProject, new SettingsData(), new CameraData(Camera.main));
    }

    //Saves the current project
    public void Save()
    {
        dataManager.Save();
    }

    //Loads a project data file
    public void Load()
    {
        ProjectData prevProject = currentProject;
        try
        {
            string path = FileManager.OpenFileBrowser("*");
            currentProject = FileManager.Load<ProjectData>(path);
            dataManager = new DataManager(currentProject.GetDirectory(), currentProject, new SettingsData(), new CameraData());
            dataManager.Load();
            dataManager.Update();
        }
        catch(FileNotFoundException) //Just an example of what we might catch, probably more/other exception is needed
        {
            currentProject = prevProject;
            Debug.Log("Couldn't load selected project");
        }
    }
    
    /*private void AddToRecent()
    {
        SettingsData settings = FileManager.LoadAppData<SettingsData>("Settings");
        if (settings.recentProjects.Contains(currentProject))
        {
            settings.recentProjects.Remove(currentProject);
        }
        settings.recentProjects.Insert(0, currentProject);
        FileManager.SaveAppData("Settings", settings);
    }*/

}
