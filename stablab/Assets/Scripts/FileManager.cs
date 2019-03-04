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
        float[] cameraData = new float[8];
        cameraData[0] = Camera.main.transform.position.x;
        cameraData[1] = Camera.main.transform.position.y;
        cameraData[2] = Camera.main.transform.position.z;
        cameraData[3] = Camera.main.transform.rotation.x;
        cameraData[4] = Camera.main.transform.rotation.y;
        cameraData[5] = Camera.main.transform.rotation.z;
        cameraData[6] = Camera.main.transform.rotation.w;
        cameraData[7] = Camera.main.fieldOfView;
        SaveData("CameraData", cameraData);
    }

    public void LoadCamera()
    {
        
        float[] cameraData = LoadData<float[]>("CameraData");
        Camera.main.transform.position = new Vector3(cameraData[0], cameraData[1], cameraData[2]);
        Camera.main.transform.rotation = new Quaternion(cameraData[3], cameraData[4], cameraData[5], cameraData[6]);
        Camera.main.fieldOfView = cameraData[7];
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
        string path = FileBrowser.SaveFile(fileName,"txt");
        Debug.Log("Saved file: " + path);
    }

    public void LoadFile()
    {
        string path = FileBrowser.OpenSingleFile();
        Debug.Log("Loaded file: " + path);
    }
}
