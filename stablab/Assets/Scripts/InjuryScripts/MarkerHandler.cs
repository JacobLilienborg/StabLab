using System;
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
    public Guid Id { get; protected set; }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        Id = InjuryManager.activeInjury.Id;
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
        InjuryManager.SetActiveInjury(Id);
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
