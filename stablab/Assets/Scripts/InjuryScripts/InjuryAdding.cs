using UnityEngine;

public enum InjuryState
{
    Add,
    Inactive,
    Delete
};

/*
 * InjuryAdding spawns an injurymarker of the currently active injury on the body by clicking somewhere on the body.
 */

public class InjuryAdding : MonoBehaviour
{
    public InjuryState currentInjuryState = InjuryState.Inactive;
    public InjuryType currentInjuryType;

    public GameObject injuryManagerObj;
    private InjuryManager injuryManager;
    private ModelController modelController;

    public GameObject modelManagerObj;
    private WeaponModelManager modelManager;

    public Vector3 markerPos;
    public Transform hitPart;

    public GameObject newMarker;


    // Start is called before the first frame update
    void Start()
    {
        modelManager = modelManagerObj.GetComponent<WeaponModelManager>();
        injuryManager = injuryManagerObj.GetComponent<InjuryManager>();
        modelController = gameObject.GetComponent<ModelController>();
    }

    // Waits for the user to clicks on the body when state is in add, and then adds one marker to the body.
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

                    if(newMarker == null) 
                    {
                        newMarker = AddMarker(markerPos, hitPart, Quaternion.identity);
                    }
                    else 
                    {
                        UpdateNewMarker(markerPos, hitPart, Quaternion.identity);
                    }

                    modelManager.UpdateModel();
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



    public void Reset() {
        SetStateToInactive();

        modelManager.ResetModel();
        Destroy(newMarker);
        newMarker = null;
        if (InjuryManager.activeInjury.HasMarker())
        {
            InjuryManager.activeInjury.Marker.GetWeaponModel().SetActive(true);
        }

        if (InjuryManager.activeInjury.injuryMarkerObj != null)
        {
            InjuryManager.activeInjury.ToggleMarker(true);
        }
    }

    // Save the added marker to the active injury
    public void SaveMarker()
    {
        SetStateToInactive();

        if (newMarker == null) return;


        InjuryManager.activeInjury.RemoveCurrent();
        InjuryManager.activeInjury.AddInjuryMarker(newMarker);

        newMarker = null;
    }

    // Set state to add wich will enable the user to add a marker to the body.
    public void SetStateToAdd()
    {
        currentInjuryState = InjuryState.Add;
    }

    // Deactivate the adding state
    public void SetStateToInactive()
    {
        currentInjuryState = InjuryState.Inactive;
    }

    // Set state to delet so a marker can be deleted
    public void SetStateToDelete()
    {
        currentInjuryState = InjuryState.Delete;
    }

    public void RemoveCurrentMarker()
    {
        Destroy(newMarker);
        //newMarker = null;
    }

    // Returns injury state
    public InjuryState GetInjuryState()
    {
        return currentInjuryState;
    }

    public void HideCurrentMarker() {
        InjuryManager.activeInjury.ToggleMarker(false);
    }

    // Instantiate a marker based on position and make it a child of parent input.
    private GameObject AddMarker(Vector3 position, Transform parent, Quaternion rotation)
    {
        if (InjuryManager.activeInjury.injuryMarkerObj != null) {
            InjuryManager.activeInjury.ToggleMarker(false);
        }
        GameObject markerObj = InjuryManager.activeInjury.InstantiateMarker(position, rotation, parent);

        //markerObj.transform.rotation = rotation;
        return markerObj;
    }

    // Update newMarker
    private void UpdateNewMarker(Vector3 position, Transform parent, Quaternion rotation)
    {
        if (newMarker != null)
        {
            newMarker.transform.parent = parent;
            newMarker.transform.position = position;
            newMarker.transform.rotation = rotation;
        }
    }


    // Instantiate a saved marker from an injury.
    public GameObject LoadMarker(Injury injury)
    {
        if(injury.Marker == null)
        {
            throw new System.Exception("Injury has no Marker");
        }

        //ModelController.SetBodyPose(injury.BodyPose);
        Transform parent = GameObject.Find(injury.Marker.BodyPartParent).transform;
        return AddMarker(injury.Marker.MarkerPosition, parent, injury.Marker.MarkerRotation);
    }

    public GameObject LoadModel(Injury injury) {
        if (injury.Marker == null) {
            throw new System.Exception("Injury has no Marker");
        }
        Transform parent = GameObject.Find(injury.Marker.BodyPartParent).transform;
        GameObject model = injury.InstantiateModel(injury.Marker.ModelPosition, injury.Marker.ModelRotation, parent);
        model.GetComponent<InjuryModelGizmos>().gizmo = modelManager.gizmo;
        modelManager.SetModelColor(injury.Marker.modelColorIndex,injury); // This is the problem - - - - - - - - - - - - - - - - 
        return model;
    }

    // Called when the DeleteButton is pressed
    public void DeletePressed()
    {
        currentInjuryState = InjuryState.Delete;
    }

}
