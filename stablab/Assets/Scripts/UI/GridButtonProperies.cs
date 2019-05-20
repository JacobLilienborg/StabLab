using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButtonProperies : MonoBehaviour
{
    public Texture2D image; 
    public GameObject model;

    // Start is called before the first frame update
    public GridButtonProperies(GameObject model, Texture2D image)
    {
        this.model = model;
        this.image = image;
    }
}
