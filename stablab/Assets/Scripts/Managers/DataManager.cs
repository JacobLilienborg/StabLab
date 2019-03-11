using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    List<AData> dataList = new List<AData>();
    private readonly string workingDirectory;

    public DataManager(string workingDirectory, params AData[] data)
    {
        this.workingDirectory = workingDirectory;
        Track(data);
    }

    public void Track(params AData[] data)
    {
        foreach (AData d in data)
        {
            dataList.Add(d);
        }
        Load();
    }

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
            i++;
        }
    }

    public void Save()
    {
        foreach(AData data in dataList)
        {
            FileManager.Save(data, Path.Combine(workingDirectory, data.GetPath()));
            Debug.Log(data.fileName + " was saved successfully");
        }
    }

    public void Update()
    {
        foreach(AData data in dataList)
        {
            data.Update();
        }
    }
}
