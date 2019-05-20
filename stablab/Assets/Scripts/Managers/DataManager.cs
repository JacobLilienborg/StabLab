//
// Created by Martin Jirenius
//


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;
    private string workingDirectory;                // A string of where to save the files

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
            dataList = instance.dataList;
            workingDirectory = instance.workingDirectory;
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void SetWorkingDirectory(string directory)
    {
        workingDirectory = directory;
    }


    // Loads the data represented in each data files path.
    // This does not Update the scene with the new data, only loads it into the project
    public void Load()
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            AData data = dataList[i];
            try
            {
                dataList[i] = FileManager.Load<AData>(Path.Combine(workingDirectory, data.GetPath()));
                Debug.Log(data.fileName + " was loaded successfully");
            }
            catch (FileNotFoundException)
            {
                FileManager.Save(data, Path.Combine(workingDirectory, data.GetPath()));
                Debug.Log(data.fileName + " couldn't be found and is therefore created");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(new FileInfo(Path.Combine(workingDirectory, data.GetPath())).Directory.FullName);
                FileManager.Save(data, Path.Combine(workingDirectory, data.GetPath()));
                Debug.Log(data.fileName + " couldn't be found and is therefore created");
            }
        }
    }



    // Save the current state of each data file and save it to each file path
    public void Save()
    {
        /*
        foreach (AData data in dataList)
        {
            FileManager.Save(data, Path.Combine(workingDirectory, data.GetPath()));
            Debug.Log(data.fileName + " was saved successfully");
        }
        */

        SaveProject();
        SaveSettings();
        SaveApplicationData();
    }

    private void SaveSettings() 
    {
        FileManager.Save(Settings.settingsFile, Settings.settingsFile.GetPath());
    }

    private void SaveProject()
    {
        FileManager.Save(ProjectManager.instance., Settings.settingsFile.GetPath());
    }

    private void SaveApplicationData()
    {

    }

    // Call each data file's update function to update the scene
    public void UpdateScene()
    {
        foreach (AData data in dataList)
        {
            data.Update();
        }
    }

    public string GetWorkingDirectory()
    {
        return workingDirectory;
    }

}
