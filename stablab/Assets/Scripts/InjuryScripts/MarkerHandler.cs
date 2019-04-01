using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// Created by Viktor Barr
// This script is added to every marker
//
public class MarkerHandler : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Material originalMaterial;
    public InjuryType type;
    private int id;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        id = InjuryManager.numOfInjuries - 1;
    }

    //Changes the material while hovering over a marker
    private void OnMouseOver()
    {
        if (tag == "Marker" && outlineMaterial != null)
        {
            rend.material = outlineMaterial;
        }
    }

    //Select: sets this marker to active
    private void OnMouseDown()
    {
        InjuryManager.SetActiveInjury(-1, id);
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
