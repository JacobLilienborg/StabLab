using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiNewProject : MonoBehaviour
{
    public ProjectManager pm;
    public InputField pname;
    public InputField dir;

    public void Create()
    {
        pm.Create(pname.text, dir.text);
    }

    public void FileBrowser()
    {
        dir.text = FileManager.OpenFileBrowser();
    }
}
