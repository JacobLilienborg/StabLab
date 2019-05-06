using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledComponents : MonoBehaviour
{
    public GameObject gizmoObj;
    private static RuntimeGizmos.TransformGizmo gizmo;

    public GameObject injuryAddingObj;
    private static  InjuryAdding injuryAdding;

    public GameObject weaponModelObj;
    private static WeaponModelManager modelManager;




    // Start is called before the first frame update
    void Start()
    {
        gizmo = gizmoObj.GetComponent<RuntimeGizmos.TransformGizmo>();
        injuryAdding = injuryAddingObj.GetComponent<InjuryAdding>();
        modelManager = weaponModelObj.GetComponent<WeaponModelManager>();
    }

    public static void DisableAll() {
        //gizmo.enabled = false;
        modelManager.SaveModel();
        injuryAdding.SetStateToInactive();
    }
}
