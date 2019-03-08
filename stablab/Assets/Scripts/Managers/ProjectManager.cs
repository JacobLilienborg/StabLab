using System.IO;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private float projectVersion = 0.1f;
    private ProjectManager instance = null;
    private ProjectData currentProject;

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
    public void CreateProject(string name, string path)
    {
        currentProject = new ProjectData(name, path, projectVersion);
        SaveProject();
    }

    //Saves the current project
    public void SaveProject()
    {
        currentProject.Save();
        FileManager.Save(currentProject, currentProject.GetPath());
    }

    //Loads a project data file
    public void LoadProject()
    {
        ProjectData prevProject = currentProject;
        try
        {
            string path = FileManager.OpenFileBrowser("*");
            currentProject = FileManager.Load<ProjectData>(path);
        }
        catch(FileNotFoundException) //Just an example of what we might catch, probably more/other exception is needed
        {
            currentProject = prevProject;
            Debug.Log("Couldn't log selected project");
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
