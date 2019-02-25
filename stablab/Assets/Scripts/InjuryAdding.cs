using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InjuryState
{
    Inactive,
    Delete,
    Kross,
    Skär,
    Skjut,
    Hugg
};

public class InjuryAdding : MonoBehaviour
{
    private InjuryState currentInjuryState = InjuryState.Inactive;

    public GameObject krossMarker;
    public GameObject skärMarker;
    public GameObject skjutMarker;
    public GameObject huggMarker;

    private GameObject marker;
    private Vector3 markerPos;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray from mouseclick on screen
            RaycastHit hit;  //Where the ray hits (the injury position)
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == GetComponent<Collider>()) //If the hit was on this collider ***not needed if the floor is removed***
                {
                    markerPos = hit.point;
                    switch (currentInjuryState)
                    {
                        case InjuryState.Kross:
                            AddMarker(krossMarker, markerPos);
                            break;
                        case InjuryState.Skär:
                            AddMarker(skärMarker, markerPos);
                            break;
                        case InjuryState.Skjut:
                            AddMarker(skjutMarker, markerPos);
                            break;
                        case InjuryState.Hugg:
                            AddMarker(huggMarker, markerPos);
                            break;
                        default:
                            break;
                    }
                }
                else if (hit.collider.tag == "Marker")
                {
                    if (currentInjuryState == InjuryState.Delete)
                    {
                        Debug.Log("Destroy");
                        Destroy(hit.collider.gameObject); //Remove marker that was hit
                    }
                }
            }
        }
    }

    //markerType is the object you want to copy
    public InjuryState GetInjuryState()
    {
        return currentInjuryState;
    }

    private void AddMarker(GameObject markerType, Vector3 position)
    {
        marker = Instantiate(markerType); 
        marker.transform.position = position;
    }
    public void DeletePressed()
    {
        currentInjuryState = InjuryState.Delete;
    }

    //Called when the KrossButton is pressed
    public void KrossPressed()
    {
        currentInjuryState = InjuryState.Kross;
    }
    //Called when the SkärButton is pressed
    public void SkärPressed()
    {
        currentInjuryState = InjuryState.Skär;
    }
    //Called when the SkjutButton is pressed
    public void SkjutPressed()
    {
        currentInjuryState = InjuryState.Skjut;
    }
    //Called when the HuggButton is pressed
    public void HuggPressed()
    {
        currentInjuryState = InjuryState.Hugg;
    }
}
