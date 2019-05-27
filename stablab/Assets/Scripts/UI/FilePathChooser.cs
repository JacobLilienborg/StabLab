using UnityEngine;
using UnityEngine.UI;

// A simple class that lets one choose a file path, and changes the given InputField accordingly.
public class FilePathChooser : MonoBehaviour
{

    public InputField filePath;

    public void BrowseFilePath()
    {
        filePath.text = FileManager.OpenFileBrowser("", DataManager.instance.LoadApplicationData().recentWorkingDirectory);
        filePath.onEndEdit.Invoke(filePath.text);
    }

}
