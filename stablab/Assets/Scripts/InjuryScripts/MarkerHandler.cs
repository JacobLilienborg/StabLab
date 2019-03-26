using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerHandler : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Material originalMaterial;

    private void Start()
    {
        rend = GetComponent<Renderer>();
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
