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
    }

    public void RemoveGizmo(GameObject target) {
        gizmo.RemoveTarget(target.transform);
    }

    public Quaternion GetRotation() {
        return transform.parent.transform.rotation;
    }
}
