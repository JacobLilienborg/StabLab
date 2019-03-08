using System;
using System.IO;

public class IData
{
    private string fileName;
    private string directory;
    
    public string GetPath()
    {
        return Path.Combine(directory, fileName);
    }
}
