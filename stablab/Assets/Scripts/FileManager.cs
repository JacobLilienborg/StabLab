using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Crosstales.FB;
using System.Collections.Generic;

public class FileManager : MonoBehaviour
{
    private string dataFolderPath;

    void Start()
    {
        dataFolderPath = Path.Combine(Application.persistentDataPath, "Data");
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
    }

    public void SaveCamera()
    {
        CameraData cam = new CameraData(Camera.main);
        SaveData<CameraData>("CameraData", cam);
    }

    public void LoadCamera()
    {
        CameraData cam = LoadData<CameraData>("CameraData");
        Camera.main.transform.position = cam.GetPosition();
        Camera.main.transform.rotation = cam.GetRotation();
        Camera.main.fieldOfView = cam.GetFieldOfView();
    }

    // Saves an object of class type T in a binary format in persistentDataPath
    public void SaveData<T>(string dataName, T data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(Path.Combine(dataFolderPath, dataName), FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    // Returns a loaded object of class type T form a binary format in persistentDataPath
    public T LoadData<T>(string dataName)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(Path.Combine(dataFolderPath, dataName), FileMode.Open))
        {
            return (T)binaryFormatter.Deserialize(fileStream);
        }
    }

    // Saves a file
    public void SaveFile(string fileName)
    {
        string path = FileBrowser.SaveFile(fileName, "txt");
        Debug.Log("Saved file: " + path);
    }

    public void LoadFile()
    {
        string path = FileBrowser.OpenSingleFile();
        Debug.Log("Loaded file: " + path);
    }


}
