using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelManager : MonoBehaviour
{

    public List<GameObject> models;
    private bool modelActive = false;
    private static GameObject parent;

    public GameObject injuryAddingObj;
    private InjuryAdding injuryAdding;

    public RuntimeGizmos.TransformGizmo gizmo;

    Quaternion previousRotation;

    private bool staticChange;

    public void Start()
    {
        injuryAdding = injuryAddingObj.GetComponent<InjuryAdding>();
        gizmo = Camera.main.gameObject.GetComponent<RuntimeGizmos.TransformGizmo>();
    }

    public void Reset() {
        if (parent == null || InjuryManager.instance.activeInjury == null || !InjuryManager.instance.activeInjury.HasMarker()) return;
        parent.transform.rotation = previousRotation;
        InjuryManager.instance.activeInjury.AddModel(parent);

        Destroy(parent);
        parent = null;
    }

    public void ToggleCurrentModel() {
        SetParentIfNonExisting();
        InjuryModelGizmos modelGizmo = parent.GetComponentInChildren<InjuryModelGizmos>();
        if (parent != null)
        {
            if(InjuryManager.instance.activeInjury.HasMarker()){
                InjuryManager.instance.activeInjury.Marker.activeInPresentation = !parent.activeSelf;
            }
            
            parent.SetActive(!parent.activeSelf);
            modelActive = parent.activeSelf;
            //gizmo.enabled = parent.activeSelf;
            if(!parent.activeSelf){
                RemoveGizmoFromModel();
                modelGizmo.gizmoActive = false;
            }
        }

    }

    public void ToggleGizmo() {
        SetParentIfNonExisting();
        if (parent == null || parent.activeSelf == false) return;
        InjuryModelGizmos modelGizmo = parent.GetComponentInChildren<InjuryModelGizmos>();
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
        GameObject curModel = InjuryManager.instance.activeInjury.Marker.GetParent(); ;
        Debug.Log("Nuär jag här1111");
        if (curModel == null || parent.activeSelf == false) return;
        Debug.Log("Nuär jag här222222");
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo transGizmo = comp.gizmo;
        comp.AddGizmo(curModel);
        transGizmo.bodyPartsMovement = false;
    }

    public void RemoveGizmoFromModel()
    {
        SetParentIfNonExisting();
        GameObject curModel = InjuryManager.instance.activeInjury.Marker.GetParent();
        if (curModel == null) return;
        InjuryModelGizmos comp = curModel.GetComponentInChildren<InjuryModelGizmos>();
        RuntimeGizmos.TransformGizmo transGizmo = comp.gizmo;
        comp.RemoveGizmo(curModel);
        transGizmo.bodyPartsMovement = true;
    }

    public void AddModel()
    {
        if (parent != null) parent.SetActive(true);
        modelActive = true;
    }

    public void RemoveModel()
    {
        if (parent != null) parent.SetActive(false);
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
        }

        //GameObject currentModel = GetCurrentModel();
        if (InjuryManager.instance.activeInjury.HasMarker())
        {
            InjuryManager.instance.activeInjury.Marker.GetParent().SetActive(false);
            //UpdateParentAndChild();
        }

        if (InjuryManager.instance.activeInjury == null) return;
        parent = InjuryManager.instance.activeInjury.injury.InstantiateModel(Vector3.zero, Quaternion.identity, null);//GameObject.Instantiate(currentModel);
        parent.GetComponent<InjuryModelGizmos>().gizmo = this.gizmo;

        //parent = new GameObject("parentToWeaponModel to " + InjuryManager.activeInjury.Name);
        UpdateParentAndChild();
        RotateModel();
    }

    public void UpdateParentAndChild() {
        parent.transform.parent = injuryAdding.hitPart;
        parent.transform.position = injuryAdding.markerPos;
        parent.SetActive(true);
        modelActive = true;
    }

    public void SetParentIfNonExisting() {
        if (parent == null && InjuryManager.instance.activeInjury != null &&InjuryManager.instance.activeInjury.injury.HasMarker())
        {
            parent = InjuryManager.instance.activeInjury.injury.Marker.GetParent();

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
        /*InjuryModelGizmos comp = parent.GetComponentInChildren<InjuryModelGizmos>();
        if (comp != null)
        {

            Quaternion rot = comp.GetRotation();
            if (rot != null) parent.transform.rotation = rot;
        }*/
        if (InjuryManager.instance.activeInjury != null && InjuryManager.instance.activeInjury.HasMarker())
        {
            InjuryManager.instance.activeInjury.Marker.SetModelRotation(parent.transform.rotation);
            //InjuryManager.activeInjury.injuryMarkerObj.transform.rotation = parent.transform.rotation;
        }
    }


    public void SaveModel() {
        // Add the temporary model to the injury and reset variables

        SetParentIfNonExisting();
        if (parent == null) return;

        //SaveRotation();
        RemoveGizmoFromModel();
        InjuryManager.instance.activeInjury.Marker.activeInPresentation = parent.activeSelf;
        InjuryManager.instance.activeInjury.AddModel(parent);
        //Destroy(parent);
        parent = null;
    }

    public GameObject GetModel(Injury injury)
    {
        /*
        switch (injury.Type)
        {
            case InjuryType.Cut:
                return injuryAdding.cutModel;
            case InjuryType.Crush:
                return injuryAdding.crushModel;
            case InjuryType.Shot:
                return injuryAdding.shotModel;
            case InjuryType.Stab:
                return injuryAdding.stabModel;
            default:
                return null;
        }
        */
        return null;
    }
    /*
    public GameObject GetCurrentModel()
    {
        return InjuryManager.activeInjury.injuryModelObj;
    }
    */
    public void SetActiveInjuryColor(int colorIndex) {
        SetModelColor(colorIndex, InjuryManager.instance.activeInjury);
    }

    public void SetModelColor(int colorIndex,Injury injury) {
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
        if (!InjuryManager.instance.activeInjury.HasMarker()) return;

        MeshRenderer mesh = injury.Marker.GetParent().transform.GetComponentInChildren<MeshRenderer>();
        if (mesh == null) return;
        m = mesh.material;
        m.color = color;
        mesh.material = m;
        injury.Marker.modelColorIndex = colorIndex;
    }

    public void SetPreviousRotation() {
        if (InjuryManager.instance.activeInjury == null || !InjuryManager.instance.activeInjury.HasMarker()) return;
        previousRotation = InjuryManager.instance.activeInjury.Marker.MarkerRotation;
    }

    public static void SetParent(GameObject newParent) {
        parent = newParent;
    }

    public void ResetModel(){
        Destroy(parent);
        parent = null;
    }
}
