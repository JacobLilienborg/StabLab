using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Crosstales.FB;

public class FileBrowserExample : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string path = FileBrowser.SaveFile("MyFile", "txt");
            Debug.Log("Save file: " + path);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            string path = GetFolderPath();
            Debug.Log("Selected file: " + path);
        }
    }

    string GetFilePath() 
    { 
        return FileBrowser.OpenSingleFile("Open single file", "txt", "jpg", "pdf");
    }

    string GetFolderPath()
    {
        return FileBrowser.OpenSingleFolder();
    }

}
