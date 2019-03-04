using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Crosstales.FB;
using System.Collections.Generic;

public class FileManager : MonoBehaviour
{
    private static string dataFolderPath;
    private const string DefaultExtension = "data";

    void Start()
    {
        dataFolderPath = Path.Combine(Application.persistentDataPath, "Data");
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }

        try
        {
            SettingsData settings = LoadAppData<SettingsData>("Settings");
            if (settings.workingDirectoryPath == SettingsData.defaultFolderName 
                || !Directory.Exists(settings.workingDirectoryPath))
            {
                SetWorkingDirectory();
            }
        }
        catch (FileNotFoundException)
        {
            SettingsData settings = new SettingsData();
            settings.recentProjects = new List<string>();
            SaveAppData("Settings", settings);
            SetWorkingDirectory();
        }
    }

    public static void SetWorkingDirectory()
    {
        SettingsData settings = LoadAppData<SettingsData>("Settings");
        string path = FileBrowser.OpenSingleFolder("Select location for working directory");
        string projPath = Path.Combine(path, "StabLab");

        Directory.CreateDirectory(projPath);

        settings.workingDirectoryPath = projPath;
        SaveAppData("Settings", settings);
    }

    public static string GetWorkingDirectory()
    {
        SettingsData settings = LoadAppData<SettingsData>("Settings");
        return settings.workingDirectoryPath;
    }

    public static void SaveAppData<T>(string dataName, T data)
    {
        SaveData<T>(dataFolderPath, dataName, data);
    }

    public static T LoadAppData<T>(string dataName)
    {
       return LoadData<T>(dataFolderPath, dataName);
    }

    public static void SaveProjectData<T>(string projName, string dataName, T data)
    {
        SaveData<T>(Path.Combine(GetWorkingDirectory(), projName), dataName, data);
    }

    public static T LoadProjectData<T>(string projName, string dataName)
    {
        return LoadData<T>(Path.Combine(GetWorkingDirectory(), projName), dataName);
    }

    // Saves an object of class type T in a binary format in path
    private static void SaveData<T>(string path, string dataName, T data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(Path.Combine(path, dataName), FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    // Returns a loaded object of class type T form a binary format in path
    private static T LoadData<T>(string path, string dataName)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(Path.Combine(path, dataName), FileMode.Open))
        {
            return (T)binaryFormatter.Deserialize(fileStream);
        }
    }

    // Saves a file using FileBrowser
    public static void SaveFileBrowser(string fileName, string extension = DefaultExtension)
    {
        string path = FileBrowser.SaveFile(fileName, extension);
        Debug.Log("Saved file: " + path);
    }

    // Load a file using FileBrowser
    public static void LoadFileBrowser()
    {
        string path = FileBrowser.OpenSingleFile();
        Debug.Log("Loaded file: " + path);
    }

    public static string OpenFolderBrowser() 
    {
        return FileBrowser.OpenSingleFolder("Select project", GetWorkingDirectory());
    }


}
