// 
// Created by Martin Jirenius, Simon Gustavsson
//

using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    static private ProjectManager instance = null;

    private float projectVersion = 0.1f;                                        // Mainly for future implementation where projectVersion is critical
    private ProjectData currentProject;                                         // A copy of the current project data
    [SerializeField] private DataManager dataManager;                           // A data manager which handles how to save and load data files
    [SerializeField] private ViewManager viewManager;                           // A view manager which handles how to switch scenes and how to transition between these.

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
            projectVersion = instance.projectVersion;
            currentProject = instance.currentProject;
            dataManager = instance.dataManager;
            viewManager = instance.viewManager;
            Destroy(instance.gameObject);
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void DebugMe(string message)
    {
        Debug.Log(message);
    }

    //Creates a new project data. 
    public void Create(string name, string directory)
    {
        currentProject = new ProjectData(name, directory, projectVersion);
        dataManager.SetWorkingDirectory(currentProject.GetDirectory());
        Load(true);
        viewManager.FadeIn(2);
    }

    public void Open()
    {
        ProjectData prevProject = currentProject;
        try
        {
            string path = FileManager.OpenFileBrowser("");
            currentProject = FileManager.Load<ProjectData>(Path.Combine(path, "Data", "project"));
            dataManager.SetWorkingDirectory(currentProject.GetDirectory());
            Load(true);
            viewManager.FadeIn(2);
        }
        catch (FileNotFoundException) //Just an example of what we might catch, probably more/other exception is needed
        {
            currentProject = prevProject;
            Debug.Log("Couldn't open selected project, the file was not found");
        }
        catch (SerializationException)
        {
            currentProject = prevProject;
            Debug.Log("Couldn't open selected project, it wasn'ta binary file");
        }

    }

    //Saves the current project
    public void Save()
    {
        dataManager.Save();
    }

    //Loads a project's data files
    public void Load(bool reset = false)
    {
        try
        {
            if (reset) ResetTrackingData();
            dataManager.Load();
            dataManager.Update();
        }
        catch(FileNotFoundException) //Just an example of what we might catch, probably more/other exception is needed
        {
            Debug.Log("Couldn't load selected project");
        }

    }

    private void ResetTrackingData()
    {
        dataManager.Reset();
        dataManager.Track(currentProject);
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
