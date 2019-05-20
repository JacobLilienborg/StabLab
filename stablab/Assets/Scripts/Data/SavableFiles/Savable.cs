//
// Created by Martin Jirenius, Simon Gustavsson
//


using System.IO;

[System.Serializable]
public abstract class Savable
{
    public readonly string fileName;
    public readonly string path;

    protected Savable(string fileName, string path)
    {
        this.fileName = fileName;
        this.path = path;
    }

    public string GetPath()
    {
        return Path.Combine(path, fileName);
    }
}
