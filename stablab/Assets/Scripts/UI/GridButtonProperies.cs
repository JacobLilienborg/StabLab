using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButtonProperies : MonoBehaviour
{
    public Texture2D image; 
    public string modelPath;

    // Start is called before the first frame update
    public GridButtonProperies(string path, Texture2D image)
    {
        modelPath = path;
        this.image = image;
    }
}
