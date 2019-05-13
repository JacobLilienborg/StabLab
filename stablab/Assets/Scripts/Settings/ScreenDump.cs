using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenDump : MonoBehaviour
{
    public List<GameObject> objectsToDisable;


    public void ToggleObjects(bool active) {
        foreach (GameObject o in objectsToDisable) {
            o.SetActive(active);
        }
    }

    IEnumerator Screenshot() {

        string name;
        ToggleObjects(false);
        if (InjuryManager.activeInjury != null && InjuryManager.activeInjury.Name != null) name = "Screenshot of " + InjuryManager.activeInjury.Name + ".png";
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
        ScreenCapture.CaptureScreenshot("Screenshots/" + name);

        yield return new WaitForEndOfFrame();

        ToggleObjects(true);
        yield return 0;
    }

    public void RunCoroutineScreenshot() {
        StartCoroutine(Screenshot());

    }
}
