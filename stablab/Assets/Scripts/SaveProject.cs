using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Some functions to use when saving and loading data
public static class SaveProject
{
    // Saves an object
    public static void Save<T>(string path, T data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    // Returns a loaded object
    public static T Load<T>(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (T)binaryFormatter.Deserialize(fileStream);
        }
    }

    // Returns the path to a file in a project
    public static string GetPath(string projectName, string fileName)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, projectName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string dataPath = Path.Combine(folderPath, fileName);

        return dataPath;
    }

}
