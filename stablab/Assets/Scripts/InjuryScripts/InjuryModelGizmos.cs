using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryModelGizmos : MonoBehaviour
{
    public RuntimeGizmos.TransformGizmo gizmo;
    // Start is called before the first frame update
    public void AddGizmo() {
        gizmo.AddTarget(transform);
    }

    public void disableBodyMovement() {
    }
}
