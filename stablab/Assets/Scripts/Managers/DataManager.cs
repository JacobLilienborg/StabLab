using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    List<AData> dataList = new List<AData>();
    private readonly string workingDirectory; // projectDirectory

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
        for (int i = 0; i< dataList.Count; i++)
        {
            try
            {
                dataList[i] = FileManager.Load<AData>(Path.Combine(workingDirectory, dataList[i].GetPath()));
                dataList[i].Load();
                Debug.Log(dataList[i].fileName + " was loaded successfully");
            }
            catch (FileNotFoundException)
            {
                    FileManager.Save(dataList[i], Path.Combine(workingDirectory, dataList[i].GetPath()));
                    Debug.Log(dataList[i].fileName + " couldn't be found and is therefore created");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(new FileInfo(Path.Combine(workingDirectory, dataList[i].GetPath())).Directory.FullName);
                FileManager.Save(dataList[i], Path.Combine(workingDirectory, dataList[i].GetPath()));
                Debug.Log(dataList[i].fileName + " couldn't be found and is therefore created");
            }
        }
    }

    public void Save()
    {
        foreach(AData data in dataList)
        {
            data.Update();
            FileManager.Save(data, Path.Combine(workingDirectory, data.GetPath()));
            Debug.Log(data.fileName + " was saved successfully");
        }
    }

    public void Update()
    {
        foreach(AData data in dataList)
        {
            data.Load();
        }
    }
}
