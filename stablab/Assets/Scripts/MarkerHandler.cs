using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerHandler : MonoBehaviour
{
    private Renderer rend;
    private Material outlineMaterial;
    private Material originalMaterial;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        switch (stripInstance(gameObject.GetComponent<Renderer>().material.name))
        {
            case ("Kross"):
                outlineMaterial = GameObject.Find("KrossMarkerOutlined").GetComponent<Renderer>().material;
                originalMaterial = GameObject.Find("KrossMarker").GetComponent<Renderer>().material;
                break;
            case ("Skär"):
                outlineMaterial = GameObject.Find("SkärMarkerOutlined").GetComponent<Renderer>().material;
                originalMaterial = GameObject.Find("SkärMarker").GetComponent<Renderer>().material;
                break;
            case ("Skjut"):
                outlineMaterial = GameObject.Find("SkjutMarkerOutlined").GetComponent<Renderer>().material;
                originalMaterial = GameObject.Find("SkjutMarker").GetComponent<Renderer>().material;
                break;
            case ("Hugg"):
                outlineMaterial = GameObject.Find("HuggMarkerOutlined").GetComponent<Renderer>().material;
                originalMaterial = GameObject.Find("HuggMarker").GetComponent<Renderer>().material;
                break;
            default:
                break;
        }
    }
    //Removes the "(Instance)" from material name
    private string stripInstance(string str)
    {
        string[] splitStrings = str.Split(' ');
        return splitStrings[0];
    }

    //Changes the material while hovering over a marker
    private void OnMouseOver()
    {
        if (tag == "Marker" && outlineMaterial != null)
        {
            rend.material = outlineMaterial;
        }
    }

    //Changes the material back to the original material
    private void OnMouseExit()
    {
        if (tag == "Marker")
        {
            rend.material = originalMaterial;
        }
    }
}
