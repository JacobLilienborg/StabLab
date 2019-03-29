//
// Created by Martin Jirenius, Simon Gustavsson
//


using System.IO;

[System.Serializable]
public abstract class AData
{
    public readonly string fileName;
    public readonly string directory;
    
    public AData(string fileName, string directory)
    {
        this.fileName = fileName;
        this.directory = directory;
    }

    public string GetPath()
    {
        return Path.Combine(directory, fileName);
    }
    
    public abstract void Update();

}
