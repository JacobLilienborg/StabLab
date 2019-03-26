//
// Created by Martin Jirenius
//

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    List<AData> dataList = new List<AData>();                                       // A list of data files
    private readonly string workingDirectory;                                       // A string of where to save the files

    // Takes a working directory and a arbitrary list of data files
    public DataManager(string workingDirectory, params AData[] data)                
    {
        this.workingDirectory = workingDirectory;
        Track(data);
    }

    // Add an arbitrary amount of data files that the data manager will load and save upon request
    public void Track(params AData[] data)
    {
        foreach (AData d in data)
        {
            dataList.Add(d);
        }
        Load();
    }

    // Loads the data represented in each data files path. 
    // This does not Update the scene with the new data, only loads it into the project
    public void Load()
    {
        int i = 0;
        foreach (AData data in dataList)
        {
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
            i++;
        }
    }

    // Save the current state of each data file and save it to each file path
    public void Save()
    {
        foreach(AData data in dataList)
        {
            FileManager.Save(data, Path.Combine(workingDirectory, data.GetPath()));
            Debug.Log(data.fileName + " was saved successfully");
        }
    }

    // Call each data file's update function to update the scene
    public void Update()
    {
        foreach(AData data in dataList)
        {
            data.Update();
        }
    }
}
