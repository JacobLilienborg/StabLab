//
// Created by Martin Jirenius, Simon Gustavsson
//

using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;

    private float projectVersion = 0.1f;                                        // Mainly for future implementation where projectVersion is critical
    public ProjectFile currentProject { get; protected set; }                                         // A copy of the current project data

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
            Destroy(instance.gameObject);
            instance = this;
        }

        DontDestroyOnLoad(this);

    }


    //Creates a new project data.
    public void Create(string name, string directory)
    {
        currentProject = new ProjectFile(name, directory, projectVersion);
        DataManager.instance.SetWorkingDirectory(directory);
        DataManager.instance.AddToRecent(currentProject);
        SceneManager.LoadScene("CharacterMode");
    }

    public void Open(ProjectFile proj)
    {
        currentProject = proj;
        if(!proj.modelData.isModified) 
        {
            SceneManager.LoadScene("CharacterMode");
        }
        else 
        {
            SceneManager.LoadScene("InjuryMode");
            InjuryManager.instance.LoadInjuries(currentProject.injuryData);

        }
    }

    //Saves the current project
    public void SetProjectData()
    {
        currentProject.injuryData = InjuryManager.instance.GetListOfInjuryData();
        currentProject.modelData = ModelManager.instance.modelData;
    }

}
