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

    public List<GameObject> models;
    private GameObject model;
    private bool modelState;
    private GameObject parent;

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
                    UpdateModel();
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

    public void AddGizmoToModel() {
        GameObject curModel = parent;//InjuryManager.activeInjury.Marker.model;
        if (curModel == null) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo gizmo = comp.gizmo;
        comp.AddGizmo(curModel);
        gizmo.bodyPartsMovement = false;
    }

    public void RemoveGizmoFromModel() {
        GameObject curModel = parent;
        if (curModel == null) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo gizmo = comp.gizmo;
        comp.RemoveGizmo(curModel);
        gizmo.bodyPartsMovement = true;
    }

    public void AddModel()
    {
        if(model != null) model.SetActive(true);
        modelState = true;
    }

    public void RemoveModel()
    {
        if (model != null) model.SetActive(false);
        modelState = false;
    }

    public void Reset() {
        RemoveModel();
        Destroy(newMarker);
        if (InjuryManager.activeInjury.Marker != null) InjuryManager.activeInjury.Marker.ToggleModel(true);
        if (InjuryManager.activeInjury.injuryMarkerObj != null) InjuryManager.activeInjury.ToggleMarker(true);
    }

    public void SaveMarker() 
    {
        if (newMarker == null || model == null) return;

        InjuryModelGizmos comp = parent.GetComponentInChildren<InjuryModelGizmos>();
        parent.transform.rotation = comp.GetRotation();

        RemoveGizmoFromModel();

        InjuryManager.activeInjury.RemoveCurrent();
        InjuryManager.activeInjury.AddInjuryMarker(newMarker);
        InjuryManager.activeInjury.AddModel(parent);

        Destroy(parent);
        Destroy(model);
        model = null;
        parent = null;
        newMarker = null;
    }

    public void SetStateToAdd() 
    {
        currentInjuryState = InjuryState.Add;
        if(InjuryManager.activeInjury.Marker != null) UpdateModel(true);
    }

    public void SetStateToInactive()
    {
        currentInjuryState = InjuryState.Inactive;
    }

    public void SetStateToDelete()
    {
        currentInjuryState = InjuryState.Delete;
    }

    public void RemoveCurrentMarker()
    {
        Destroy(newMarker);
    }

    public InjuryState GetInjuryState()
    {
        return currentInjuryState;
    }

    public void HideCurrentMarker() {
        InjuryManager.activeInjury.ToggleMarker(false);
    }

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

    private void UpdateModel(bool replace = false) {
        if (replace) {
            parent = InjuryManager.activeInjury.Marker.model;
            model = parent.transform.GetChild(0).gameObject;
            return;
        }
        if (model != null)
        {
            Destroy(parent.gameObject);
            Destroy(model.gameObject);
        }
        if(InjuryManager.activeInjury.Marker != null) InjuryManager.activeInjury.Marker.ToggleModel(false);
        GameObject currentModel = GetCurrentModel();
        if (currentModel == null) return;
        model = GameObject.Instantiate(currentModel);

        parent = new GameObject("parentToWeaponModel");
        parent.transform.parent = hitPart.transform;
        parent.transform.position = markerPos;

        model.transform.position = markerPos;
        model.SetActive(modelState);
        model.transform.parent = parent.transform;
        RotateModel();
    }

    public void RotateModel()
    {
        if (InjuryManager.activeInjury.Marker != null) parent.transform.rotation = InjuryManager.activeInjury.Marker.model.transform.rotation;
        else
        {
            Vector3 cameraPos = Camera.main.transform.position;

            parent.transform.rotation = Quaternion.FromToRotation(Vector3.left, cameraPos - markerPos);
        }
    }

    private GameObject GetModel()
    {
        switch (InjuryManager.activeInjury.Type)
        {
            case InjuryType.Cut:
                Debug.Log("CUT");
                return models[0];
            case InjuryType.Crush:
                Debug.Log("CRUSH");
                return models[1];
            case InjuryType.Shot:
                Debug.Log("SHOT");
                return models[2];
            case InjuryType.Stab:
                Debug.Log("STAB");
                return models[3];
            default:
                return null;
        }
    }

    public GameObject GetCurrentModel()
    {
        return GetModel();
    }
}
