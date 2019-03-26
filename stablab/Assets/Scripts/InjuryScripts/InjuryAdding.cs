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
    Kross,
    Skär,
    Skjut,
    Hugg
};


public class InjuryAdding : MonoBehaviour
{
    public InjuryState currentInjuryState = InjuryState.Inactive;
    private InjuryType currentInjuryType;

    public GameObject injuryManagerObj;
    private InjuryManager injuryManager;

    public GameObject krossMarker;
    public GameObject skärMarker;
    public GameObject skjutMarker;
    public GameObject huggMarker;

    //private GameObject marker;
    private Vector3 markerPos;


    // Start is called before the first frame update
    void Start()
    {
        injuryManager = injuryManagerObj.GetComponent<InjuryManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray from mouseclick on screen
            RaycastHit hit;  //Where the ray hits (the injury position)
            
            if (Physics.Raycast(ray, out hit))
            {
                if (currentInjuryState == InjuryState.Add && hit.collider == GetComponent<Collider>()) //If the hit was on this collider ***not needed if the floor is removed***
                {
                    markerPos = hit.point;
                    injuryManager.AddInjuryMarker(AddMarker(currentInjuryType, markerPos));
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

    private GameObject AddMarker(InjuryType type, Vector3 position)
    {
        GameObject markerObj;
        switch (type)
        {
            case InjuryType.Kross:
                markerObj = Instantiate(krossMarker, position, Quaternion.identity);
                break;
            case InjuryType.Skär:
                markerObj = Instantiate(skärMarker, position, Quaternion.identity);
                break;
            case InjuryType.Skjut:
                markerObj = Instantiate(skjutMarker, position, Quaternion.identity);
                break;
            case InjuryType.Hugg:
                markerObj = Instantiate(huggMarker, position, Quaternion.identity);
                break;
            default:
                Debug.Log("None existing marker");
                markerObj = null;
                break;
        }

        markerObj.transform.parent = transform;
        markerObj.GetComponent<MarkerHandler>().MarkerData.ModelPose = new ModelData(gameObject);
        markerObj.GetComponent<MarkerHandler>().MarkerData.Position = markerObj.transform.position;
        return markerObj;
    }

    public GameObject LoadMarker(MarkerData marker)
    {
        transform.position = marker.ModelPose.GetPosition();
        transform.rotation = marker.ModelPose.GetRotation();

       return AddMarker(marker.Type, marker.Position);
    }

    public void DeletePressed()
    {
        currentInjuryState = InjuryState.Delete;
    }

    //Called when the KrossButton is pressed
    public void KrossPressed()
    {
        currentInjuryType = InjuryType.Kross;
    }
    //Called when the SkärButton is pressed
    public void SkärPressed()
    {
        currentInjuryType = InjuryType.Skär;
    }
    //Called when the SkjutButton is pressed
    public void SkjutPressed()
    {
        currentInjuryType = InjuryType.Skjut;
    }
    //Called when the HuggButton is pressed
    public void HuggPressed()
    {
        currentInjuryType = InjuryType.Hugg;
    }
}
