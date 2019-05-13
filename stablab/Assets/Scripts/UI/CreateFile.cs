using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFile : MonoBehaviour
{
    public InputField projectName;
    public InputField projectLocation;
    public ProjectManager projectManager;

    public void CreateNewProject() {
        projectManager.Create(projectName.text, projectLocation.text);
    }

    public void BrowseFileLocation() {
        projectLocation.text = FileManager.OpenFileBrowser();
    }

    public void CreateNewFromMenu() {
    }
}
