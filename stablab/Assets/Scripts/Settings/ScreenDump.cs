using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenDump : MonoBehaviour
{
    public List<GameObject> objectsToDisable;
    private string workingDirectory;
    private string folder = "screenshots";

    private void Start()
    {
        workingDirectory = DataManager.instance.GetWorkingDirectory();
        Settings.AddSettingsConfirmedListener(ChangeWorkingDirectory);
    }

    public void ChangeWorkingDirectory()
    {
        //Debug.Log("Settings.screenShotFilePath;");
        string tmpPath = Settings.data.screenShotFilePath;
        if (tmpPath != "") workingDirectory = tmpPath;
    }

    public void ToggleObjects(bool active) {
        foreach (GameObject o in objectsToDisable) {
            o.SetActive(active);
        }
    }

    IEnumerator Screenshot() {

        string name;
        ToggleObjects(false);
        if (InjuryManager.instance.activeInjury != null && InjuryManager.instance.activeInjury.injuryData.name != null) name = "Screenshot of " + InjuryManager.instance.activeInjury.injuryData.name + ".png";
        else name = "unknown" + Time.time.ToString() + ".png";

        yield return new WaitForEndOfFrame();
        /*int width = Screen.width;
        int height = Screen.height;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        yield return new WaitForFixedUpdate();

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        */
        string[] directories = Directory.GetDirectories(workingDirectory);
        bool saved = false;
        foreach (string i in directories)
        {
            if (i == folder)
            {
                ScreenCapture.CaptureScreenshot(Path.Combine(workingDirectory, folder, name));
                saved = true;
            }
        }
        if (!saved)
        {
            Directory.CreateDirectory(new FileInfo(Path.Combine(workingDirectory, folder, name)).Directory.FullName);
            ScreenCapture.CaptureScreenshot(Path.Combine(workingDirectory, folder, name));
        }




        yield return new WaitForEndOfFrame();

        ToggleObjects(true);
        yield return 0;
    }

    public void RunCoroutineScreenshot() {
        StartCoroutine(Screenshot());

    }
}
