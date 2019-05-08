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

    public GameObject crushMarker, cutMarker, shotMarker, stabMarker;
    public GameObject crushModel, cutModel, shotModel, stabModel;
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

                    Destroy(newMarker);
                    newMarker = AddMarker(markerPos, hitPart);
                    modelManager.UpdateModel();
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



    public void Reset() {
        modelManager.RemoveModel();
        Destroy(newMarker);
        if (InjuryManager.activeInjury.HasMarker()) InjuryManager.activeInjury.Marker.GetParent().SetActive(true);
        if (InjuryManager.activeInjury.injuryMarkerObj != null) InjuryManager.activeInjury.ToggleMarker(true);
    }

    // Save the added marker to the active injury
    public void SaveMarker()
    {
        if (newMarker == null) return;


        InjuryManager.activeInjury.RemoveCurrent();
        InjuryManager.activeInjury.AddInjuryMarker(newMarker);
        modelManager.SaveModel();

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
    private GameObject AddMarker(Vector3 position, Transform parent)
    {
        if (InjuryManager.activeInjury.injuryMarkerObj != null) {
            InjuryManager.activeInjury.ToggleMarker(false);
        }
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
                return markerObj;
        }

        markerObj.transform.parent = parent;
        return markerObj;
    }

    // Instantiate a saved marker from an injury.
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

    public GameObject LoadModel(Injury injury) {
        if (injury.Marker == null) {
            throw new System.Exception("Injury has no Marker");
        }

        GameObject model = Instantiate(modelManager.GetModel(injury));
        //model.transform.rotation = injury.Marker.Rotation;
        model.transform.position = injury.Marker.Position;
        model.GetComponent<InjuryModelGizmos>().gizmo = modelManager.gizmo;
        modelManager.SetModelColor(injury.Marker.modelColorIndex,injury);
        return model;
    }

    // Called when the DeleteButton is pressed
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
