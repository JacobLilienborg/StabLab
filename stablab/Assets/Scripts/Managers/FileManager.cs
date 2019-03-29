//
// Created by Martin Jirenius
//

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Crosstales.FB;
using UnityEngine;

public class FileManager
{
    //Load a arbitrary binary file and return it's data
    public static T Load<T>(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (T)binaryFormatter.Deserialize(fileStream);
        }
    }

    //Saves an object of class type T in a binary format in path
    public static void Save<T>(T data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
        
    }

    //Reads a file as a text 
    public static string Read(string path)
    {
        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }

    //Writes to a file as text
    public static void Write(string text, string path)
    {
        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(text);
            }
        }
    }

    //Open up the file browser. Depending on the extension it will accept different files
    public static string OpenFileBrowser(string extension = "", string path = "./")
    {
        switch (extension)
        {
            case "": return FileBrowser.OpenSingleFolder("Select a folder", path);
            default: return FileBrowser.OpenSingleFile("Select a file", path, extension);
        }
    }
}
