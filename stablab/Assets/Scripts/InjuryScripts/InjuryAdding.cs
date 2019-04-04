using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InjuryState
{
    Add,
    Inactive,
    Delete
};

public enum InjuryType
{
    Null,
    Crush,
    Cut,
    Shot,
    Stab, 
}


public class InjuryAdding : MonoBehaviour
{
    public InjuryState currentInjuryState = InjuryState.Inactive;
    private InjuryType currentInjuryType;

    public GameObject injuryManagerObj;
    private InjuryManager injuryManager;
    private ModelController modelController;

    public GameObject crushMarker, cutMarker, shotMarker, stabMarker;
    private Vector3 markerPos;
    private Transform hitPart;

    private GameObject newMarker;


    // Start is called before the first frame update
    void Start()
    {
        injuryManager = injuryManagerObj.GetComponent<InjuryManager>();
        modelController = gameObject.GetComponent<ModelController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray from mouseclick on screen
            RaycastHit hit;  //Where the ray hits (the injury position)

            if (Physics.Raycast(ray, out hit))
            {
                if (currentInjuryState == InjuryState.Add && hit.collider.tag == "Body") //If the hit was on this collider ***not needed if the floor is removed***
                {
                    markerPos = hit.point;
                    hitPart = hit.transform;

                    Destroy(newMarker);
                    newMarker = AddMarker(markerPos, hitPart);
                    //currentInjuryState = InjuryState.Inactive;
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

    public void SaveMarker() 
    {
        InjuryManager.activeInjury.AddInjuryMarker(newMarker);
        newMarker = null;
    }

    public void SetStateToAdd() 
    {
        currentInjuryState = InjuryState.Add;
    }

    public void SetStateToInactive()
    {
        currentInjuryState = InjuryState.Inactive;
    }

    public void SetStateToDelete()
    {
        currentInjuryState = InjuryState.Delete;
    }

    public void removeCurrentMarker()
    {
        Destroy(newMarker);
    }

    public InjuryState GetInjuryState()
    {
        return currentInjuryState;
    }

    private GameObject AddMarker(Vector3 position, Transform parent)
    {
        GameObject markerObj;
        switch (InjuryManager.activeInjury.Type)
        {
            case InjuryType.Crush:
                markerObj = Instantiate(crushMarker, position, Quaternion.identity);
                break;
            case InjuryType.Cut:
                markerObj = Instantiate(cutMarker, position, Quaternion.identity);
                break;
            case InjuryType.Shot:
                markerObj = Instantiate(shotMarker, position, Quaternion.identity);
                break;
            case InjuryType.Stab:
                markerObj = Instantiate(stabMarker, position, Quaternion.identity);
                break;
            default:
                Debug.Log("None existing marker");
                markerObj = null;
                break;
        }

        markerObj.transform.parent = parent;
        return markerObj;
    }

    public GameObject LoadMarker(Injury injury)
    {
        if(injury.Marker == null)
        {
            throw new System.Exception("Injury has no Marker");
        }

        ModelController.SetBodyPose(injury.BodyPose);
        Transform parent = GameObject.Find(injury.Marker.BodyPartParent).transform;
        return AddMarker(injury.Marker.Position, parent);
    }

    public void DeletePressed()
    {
        currentInjuryState = InjuryState.Delete;
    }

    //Called when the CrushButton is pressed
    public void CrushPressed()
    {
        currentInjuryType = InjuryType.Crush;
    }
    //Called when the CutButton is pressed
    public void CutPressed()
    {
        currentInjuryType = InjuryType.Cut;
    }
    //Called when the ShotButton is pressed
    public void ShotPressed()
    {
        currentInjuryType = InjuryType.Shot;
    }
    //Called when the StabButton is pressed
    public void StabPressed()
    {
        currentInjuryType = InjuryType.Stab;
    }
}
