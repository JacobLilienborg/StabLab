using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentFilePath : MonoBehaviour
{
    public InputField text;
    private void OnEnable()
    {
        text.text = Settings.data.screenShotFilePath;   
    }
}
