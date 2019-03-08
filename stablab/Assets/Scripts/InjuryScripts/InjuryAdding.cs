using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InjuryState
{
    Inactive,
    Delete,
    Crush,
    Cut,
    Shot,
    Stab
};

public class InjuryAdding : MonoBehaviour
{
    private InjuryState currentInjuryState = InjuryState.Inactive;

    bool injuryAddingMode = false;

    public GameObject crushMarker, cutMarker, shotMarker, stabMarker,marker;
    private Vector3 markerPos;
    private Transform hitPart;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            injuryAddingMode = !injuryAddingMode;
        }
        if (Input.GetMouseButton(0) && injuryAddingMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray from mouseclick on screen
            RaycastHit hit;  //Where the ray hits (the injury position)
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Body") //If the hit was on this collider ***not needed if the floor is removed***
                {
                    markerPos = hit.point;
                    hitPart = hit.transform;
                    switch (currentInjuryState)
                    {
                        case InjuryState.Crush:
                            AddMarker(crushMarker, markerPos, hitPart);
                            break;
                        case InjuryState.Cut:
                            AddMarker(cutMarker, markerPos, hitPart);
                            break;
                        case InjuryState.Shot:
                            AddMarker(shotMarker, markerPos, hitPart);
                            break;
                        case InjuryState.Stab:
                            AddMarker(stabMarker, markerPos, hitPart);
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

    public InjuryState GetInjuryState()
    {
        return currentInjuryState;
    }

    private void AddMarker(GameObject markerType, Vector3 position, Transform parent)
    {
        marker = Instantiate(markerType); 
        marker.transform.position = position;
        marker.transform.parent = parent; //Marker is child to body
    }
    public void DeletePressed()
    {
        currentInjuryState = InjuryState.Delete;
    }

    //Called when the CrushButton is pressed
    public void CrushPressed()
    {
        currentInjuryState = InjuryState.Crush;
    }
    //Called when the CutButton is pressed
    public void CutPressed()
    {
        currentInjuryState = InjuryState.Cut;
    }
    //Called when the ShotButton is pressed
    public void ShotPressed()
    {
        currentInjuryState = InjuryState.Shot;
    }
    //Called when the StabButton is pressed
    public void StabPressed()
    {
        currentInjuryState = InjuryState.Stab;
    }
}
