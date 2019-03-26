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
    Crush,
    Cut,
    Shot,
    Stab
}


public class InjuryAdding : MonoBehaviour
{
    public InjuryState currentInjuryState = InjuryState.Inactive;
    private InjuryType currentInjuryType;

    public GameObject injuryManagerObj;
    private InjuryManager injuryManager;

    bool injuryAddingMode = false;

    public GameObject crushMarker, cutMarker, shotMarker, stabMarker,marker;
    public GameObject skeleton;
    private Vector3 markerPos;
    private Transform hitPart;


    // Start is called before the first frame update
    void Start()
    {
        injuryManager = injuryManagerObj.GetComponent<InjuryManager>();
    }

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
                if (currentInjuryState == InjuryState.Add && hit.collider.tag == "Body") //If the hit was on this collider ***not needed if the floor is removed***
                {
                    markerPos = hit.point;
                    hitPart = hit.transform;

                    injuryManager.AddInjuryMarker(AddMarker(currentInjuryType, markerPos, hitPart));
                    currentInjuryState = InjuryState.Inactive;
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

    private GameObject AddMarker(InjuryType type, Vector3 position, Transform parent)
    {
        GameObject markerObj;
        switch (type)
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
        markerObj.GetComponent<MarkerHandler>().MarkerData.BodyPose = new BodyData(gameObject);
        markerObj.GetComponent<MarkerHandler>().MarkerData.Position = markerObj.transform.position;
        return markerObj;
    }

    public GameObject LoadMarker(MarkerData marker)
    {
        SetBodyPose(marker.BodyPose);
        Transform parent = GameObject.Find(marker.BodyPart).transform;
       return AddMarker(marker.Type, marker.Position, parent);
    }

    private void SetBodyPose(BodyData data) 
    {
        skeleton.transform.position = data.GetPosition();
        skeleton.transform.rotation = data.GetRotation();

        Transform[] children = skeleton.GetComponentsInChildren<Transform>();
        for (int i = 0; i < data.bodyParts.Count; i++)
        {
            children[i].position = data.bodyParts[i].GetPosition();
            children[i].rotation = data.bodyParts[i].GetRotation();
        }
        
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
