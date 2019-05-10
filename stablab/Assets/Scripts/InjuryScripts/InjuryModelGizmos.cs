using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryModelGizmos : MonoBehaviour
{
    public RuntimeGizmos.TransformGizmo gizmo;
    public bool gizmoActive = false;

    // Start is called before the first frame update
    public void AddGizmo(GameObject target) {
        gizmo.AddTarget(target.transform);
        gizmoActive = true;
    }

    public void RemoveGizmo(GameObject target) {
        Debug.Log(target.name);
        gizmo.RemoveTarget(target.transform);
        gizmoActive = false;
    }

    public Quaternion GetRotation() {
        return transform.rotation;
    }
}
