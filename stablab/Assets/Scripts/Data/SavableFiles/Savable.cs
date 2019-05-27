//
// Created by Martin Jirenius, Simon Gustavsson
//


using System.IO;

[System.Serializable]
public abstract class Savable
{
    public readonly string fileName;
    public readonly string directory;

    protected Savable(string fileName, string directory)
    {
        this.fileName = fileName;
        this.directory = directory;
    }

    public string GetPath()
    {
        return Path.Combine(directory, fileName);
    }
}
