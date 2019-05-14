using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelManager : MonoBehaviour
{

    private bool modelActive = false;
    private static GameObject tempWeapon;

    public GameObject injuryAddingObj;
    private InjuryAdding injuryAdding;

    public RuntimeGizmos.TransformGizmo gizmo;

    Quaternion previousRotation;
    int previousColor;

    private bool staticChange;

    public void Start()
    {
        injuryAdding = injuryAddingObj.GetComponent<InjuryAdding>();
        gizmo = Camera.main.gameObject.GetComponent<RuntimeGizmos.TransformGizmo>();
    }

    public void Reset() {
        if (tempWeapon == null || InjuryManager.activeInjury == null || !InjuryManager.activeInjury.HasMarker()) return;
        tempWeapon.transform.rotation = previousRotation;
        InjuryManager.activeInjury.AddModel(tempWeapon);

        Destroy(tempWeapon);
        tempWeapon = null;
    }

    public void ToggleCurrentModel() {

        Debug.Log(InjuryManager.activeInjury.isInHole);
        SetParentIfNonExisting();
        if (tempWeapon != null)
        {
            InjuryModelGizmos modelGizmo = tempWeapon.GetComponentInChildren<InjuryModelGizmos>();

            if(InjuryManager.activeInjury.HasMarker()){
                InjuryManager.activeInjury.Marker.activeInPresentation = !tempWeapon.activeSelf;
            }

            tempWeapon.SetActive(!tempWeapon.activeSelf);
            modelActive = tempWeapon.activeSelf;
            //gizmo.enabled = parent.activeSelf;
            if(!tempWeapon.activeSelf){
                RemoveGizmoFromModel();
                modelGizmo.gizmoActive = false;
            }
        }

    }

    public void ToggleGizmo() {
        SetParentIfNonExisting();
        if (tempWeapon == null || tempWeapon.activeSelf == false) return;
        InjuryModelGizmos modelGizmo = tempWeapon.GetComponentInChildren<InjuryModelGizmos>();
        if (modelGizmo.gizmoActive) {
            //SaveRotation();
            RemoveGizmoFromModel();
        }
        else {
            AddGizmoToModel();
        }
    }

    public void AddGizmoToModel()
    {
        SetParentIfNonExisting();
        GameObject curModel = InjuryManager.activeInjury.Marker.GetWeaponModel(); ;
        if (curModel == null || tempWeapon.activeSelf == false) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo transGizmo = comp.gizmo;
        comp.AddGizmo(curModel);
        transGizmo.bodyPartsMovement = false;
    }

    public void RemoveGizmoFromModel()
    {
        SetParentIfNonExisting();
        GameObject curModel = InjuryManager.activeInjury.Marker.GetWeaponModel();
        if (curModel == null) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo transGizmo = comp.gizmo;
        comp.RemoveGizmo(curModel);
        transGizmo.bodyPartsMovement = true;
    }

    public void AddModel()
    {
        if (tempWeapon != null) tempWeapon.SetActive(true);
        modelActive = true;
    }

    public void RemoveModel()
    {
        if (tempWeapon != null) tempWeapon.SetActive(false);
        modelActive = false;
    }

    public void UpdateModel()
    {
        if (InjuryManager.activeInjury == null) return;
        if (tempWeapon != null)
        {
            Destroy(tempWeapon);
        }

        if (InjuryManager.activeInjury.HasMarker())
        {
            InjuryManager.activeInjury.Marker.GetWeaponModel().SetActive(false);
        }

        tempWeapon = InjuryManager.activeInjury.InstantiateModel(Vector3.zero, Quaternion.identity, null);
        tempWeapon.GetComponent<InjuryModelGizmos>().gizmo = this.gizmo;

        UpdateParentAndChild();
        RotateModel();
    }

    public void UpdateParentAndChild() {
        tempWeapon.transform.parent = injuryAdding.hitPart;
        tempWeapon.transform.position = injuryAdding.markerPos;
        tempWeapon.SetActive(true);
        modelActive = true;
    }

    public void SetParentIfNonExisting() {
        if (tempWeapon == null && InjuryManager.activeInjury != null &&InjuryManager.activeInjury.HasMarker())
        {
            tempWeapon = InjuryManager.activeInjury.Marker.GetWeaponModel();

        }
    }

    public void RotateModel()
    {
        // Rotates the model towards the user

        Vector3 cameraPos = Camera.main.transform.position;

        tempWeapon.transform.rotation = Quaternion.FromToRotation(Vector3.left, cameraPos - injuryAdding.markerPos);
    }

    public void SaveRotation() {
        // Save the rotation from the gizmo to the temporary model.
        if (InjuryManager.activeInjury != null && InjuryManager.activeInjury.HasMarker())
        {
            InjuryManager.activeInjury.Marker.SetModelRotation(tempWeapon.transform.rotation);
        }
    }


    public void SaveModel() {
        // Add the temporary model to the injury and reset variables

        SetParentIfNonExisting();
        if (tempWeapon == null) return;

        RemoveGizmoFromModel();
        InjuryManager.activeInjury.Marker.activeInPresentation = tempWeapon.activeSelf;
        InjuryManager.activeInjury.Marker.SetWeaponModel(tempWeapon);
        Debug.Log("ActiveInjury Model: " + InjuryManager.activeInjury.Marker.GetWeaponModel());
        tempWeapon = null;
    }

    public void SetActiveInjuryColor(int colorIndex) {
        SetModelColor(colorIndex, InjuryManager.activeInjury, InjuryManager.activeInjury.Marker.GetWeaponModel());
    }

    public void SetModelColor(int colorIndex,Injury injury, GameObject weapon) {
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
       //if (!injury.HasMarker()) return;

        MeshRenderer mesh = weapon.transform.GetComponentInChildren<MeshRenderer>();
        if (mesh == null) return;
        m = mesh.material;
        m.color = color;
        mesh.material = m;
        previousColor = injury.Marker.modelColorIndex;
        injury.Marker.modelColorIndex = colorIndex;
    }

    public void RevertColorChange()
    {
        SetActiveInjuryColor(previousColor);
    }

    public void SetPreviousRotation() {
        if (InjuryManager.activeInjury == null || !InjuryManager.activeInjury.HasMarker()) return;
        previousRotation = InjuryManager.activeInjury.Marker.ModelRotation;
    }

    public void RevertRotationChange()
    {
        RemoveGizmoFromModel();
        InjuryManager.activeInjury.Marker.GetWeaponModel().transform.rotation = previousRotation;
        tempWeapon = null;
    }

    public void ResetModel(){
        if(tempWeapon != null && (!InjuryManager.activeInjury.HasMarker() || tempWeapon != InjuryManager.activeInjury.Marker.GetWeaponModel()))
        {
            Destroy(tempWeapon);
        }
        tempWeapon = null;

    }
}
