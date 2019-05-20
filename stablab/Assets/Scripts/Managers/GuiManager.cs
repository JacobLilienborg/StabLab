using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour
{

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangePose(bool active)
    {
        if (active)
        {
            ModelManager.instance.AddOnClickListener(ModelManager.instance.activeModel.AddGizmo);
        }
        else
        {
            ModelManager.instance.RemoveOnClickListener(ModelManager.instance.activeModel.AddGizmo);
        }
    }

    public void ChangePosition(bool active)
    {
        if (active)
        {
            ModelManager.instance.AddOnClickListener(InjuryManager.instance.activeInjury.PlaceInjury);
        }
        else
        {
            ModelManager.instance.RemoveOnClickListener(InjuryManager.instance.activeInjury.PlaceInjury);
        }
    }

    public void ToggleWeaponGizmo(bool active)
    {
        if (active)
        {
            InjuryManager.instance.activeInjury.AddGizmo();
        }
        else
        {
            InjuryManager.instance.activeInjury.RemoveGizmo();
        }
    }

    public void ToggleWeapon(bool active)
    {
        InjuryManager.instance.activeInjury.ToggleWeapon(active);
    }

    public void UpdateInjury()
    {
        InjuryManager.instance.activeInjury.UpdateData();
    }
    public void RevertInjury()
    {
        InjuryManager.instance.activeInjury.RevertData();
    }

}
