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
        gizmo = Camera.main.gameObject.GetComponent<RuntimeGizmos.TransformGizmo>();
        injuryAdding = injuryAddingObj.GetComponent<InjuryAdding>();
        modelManager = weaponModelObj.GetComponent<WeaponModelManager>();
        InjuryManager.AddDeactivationListener(DisableAll);
    }

    public static void DisableAll() {
        //Camera.main.gameObject.GetComponent<RuntimeGizmos.TransformGizmo>().enabled = false;
        //modelManager.SaveModel();
        //modelManager.ResetModel();//SaveModel();
        //injuryAdding.SaveMarker();
        if(injuryAdding != null){
            injuryAdding.Reset();
            injuryAdding.SetStateToInactive();
        }
    
    }
}
