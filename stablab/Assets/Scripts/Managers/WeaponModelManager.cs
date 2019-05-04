using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelManager : MonoBehaviour
{

    public List<GameObject> models;
    private GameObject model;
    private bool modelActive = false;
    private GameObject parent;

    public GameObject injuryAddingObj;
    private InjuryAdding injuryAdding;

    public GameObject transformGizmoObj;
    private RuntimeGizmos.TransformGizmo gizmo;

    private bool staticChange;

    public void Start()
    {
        injuryAdding = injuryAddingObj.GetComponent<InjuryAdding>();
        gizmo = transformGizmoObj.GetComponent<RuntimeGizmos.TransformGizmo>();
    }

    public void Reset() {
    }

    public void ToggleCurrentModel() {
        SetParentIfNonExisting();
        if (parent != null)
        {
            InjuryManager.activeInjury.Marker.activeInPresentation = !InjuryManager.activeInjury.Marker.activeInPresentation;
            parent.SetActive(InjuryManager.activeInjury.Marker.activeInPresentation);
            modelActive = InjuryManager.activeInjury.Marker.activeInPresentation;
            gizmo.enabled = InjuryManager.activeInjury.Marker.activeInPresentation;
        }
    }

    public void ToggleGizmo() {
        SetParentIfNonExisting();
        if (parent == null) return;
        InjuryModelGizmos modelGizmo = parent.GetComponentInChildren<InjuryModelGizmos>();
        if (modelGizmo.gizmoActive) {
            SaveRotation();
            RemoveGizmoFromModel();
            modelGizmo.gizmoActive = false;
        }
        else {
            modelGizmo.gizmoActive = true;
            AddGizmoToModel();
        }
    }

    public void AddGizmoToModel()
    {
        SetParentIfNonExisting();
        GameObject curModel = parent;
        if (curModel == null) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo gizmo = comp.gizmo;
        comp.AddGizmo(curModel);
        gizmo.bodyPartsMovement = false;
    }

    public void RemoveGizmoFromModel()
    {
        SetParentIfNonExisting();
        GameObject curModel = parent;
        if (curModel == null) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo gizmo = comp.gizmo;
        comp.RemoveGizmo(curModel);
        gizmo.bodyPartsMovement = true;
    }

    public void AddModel()
    {
        if (model != null) parent.SetActive(true);
        modelActive = true;
    }

    public void RemoveModel()
    {
        if (model != null) parent.SetActive(false);
        modelActive = false;
    }

    public void UpdateModel()
    {
        /*
        if (InjuryManager.activeInjury.HasMarker()) {
            parent = InjuryManager.activeInjury.Marker.parent;
            model = parent.transform.GetChild(0).gameObject;
            UpdateParentAndChild();
            Debug.Log(model == null);
            return;
        }*/

        if (parent != null)
        {
            Destroy(parent);
            Destroy(model);
        }

        GameObject currentModel = GetCurrentModel();
        if (InjuryManager.activeInjury.HasMarker())
        {
            InjuryManager.activeInjury.Marker.parent.SetActive(false);
            //UpdateParentAndChild();
        }

        if (currentModel == null) return;
        model = GameObject.Instantiate(currentModel);

        parent = new GameObject("parentToWeaponModel" + InjuryManager.activeInjury.Name);
        UpdateParentAndChild();
        RotateModel();
    }

    public void UpdateParentAndChild() {
        parent.transform.parent = injuryAdding.hitPart;
        parent.transform.position = injuryAdding.markerPos;
        parent.SetActive(modelActive);

        model.transform.position = injuryAdding.markerPos;
        model.SetActive(true);
        model.transform.parent = parent.transform;
    }

    public void SetParentIfNonExisting() {
        if (parent == null && InjuryManager.activeInjury.HasMarker())
        {
            parent = InjuryManager.activeInjury.Marker.parent;

        }
    }

    public void RotateModel()
    {
        // Rotates the model towards the user

        Vector3 cameraPos = Camera.main.transform.position;

        parent.transform.rotation = Quaternion.FromToRotation(Vector3.left, cameraPos - injuryAdding.markerPos);
    }

    public void SaveRotation() {
        // Save the rotation from the gizmo to the temporary model.
        InjuryModelGizmos comp = parent.GetComponentInChildren<InjuryModelGizmos>();
        if (comp != null)
        {

            Quaternion rot = comp.GetRotation();
            if (rot != null) parent.transform.rotation = rot;
        }
    }

    public void SaveModel() {
        // Add the temporary model to the injury and reset variables
        SetParentIfNonExisting();
        if (parent == null) return;

        SaveRotation();
        RemoveGizmoFromModel();
        InjuryManager.activeInjury.AddModel(parent);

        Destroy(parent);
        Destroy(model);
        model = null;
        parent = null;
    }

    private GameObject GetModel()
    {
        switch (InjuryManager.activeInjury.Type)
        {
            case InjuryType.Cut:
                return models[0];
            case InjuryType.Crush:
                return models[1];
            case InjuryType.Shot:
                return models[2];
            case InjuryType.Stab:
                return models[3];
            default:
                return null;
        }
    }

    public GameObject GetCurrentModel()
    {
        return GetModel();
    }

    public void SetModelColor(int colorIndex) {
        Color color;
        Material m;
        switch (colorIndex) {
            case 0:
                color = Color.red;
                break;
            case 1:
                color = Color.yellow;
                break;
            case 2:
                color = Color.green;
                break;
            default:
                color = Color.white;
                break;
        }
        if (!InjuryManager.activeInjury.HasMarker()) return;

        MeshRenderer mesh = InjuryManager.activeInjury.Marker.parent.transform.GetComponentInChildren<MeshRenderer>();
        if (mesh == null) return;
        m = mesh.material;
        m.color = color;
        mesh.material = m;
    }
}
