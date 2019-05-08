using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModelView{
    AllVisible,
    ActiveVisible,
    NonVisible
};

public class Settings : MonoBehaviour
{
    static ModelView modelView = ModelView.ActiveVisible;

    public static bool IsActiveModel(bool selected) {
        switch (modelView) {
            case ModelView.AllVisible:
                return true;
            case ModelView.ActiveVisible:
                return selected;
            case ModelView.NonVisible:
                return false;
            default:
                return false;
        }
    }

    public void SetModelView(int index) {
        switch (index) {
            case 0:
                modelView = ModelView.AllVisible;
                break;
            case 1:
                modelView = ModelView.ActiveVisible;
                break;
            case 2:
                modelView = ModelView.NonVisible;
                break;
            default:
                modelView = ModelView.ActiveVisible;
                break;

        }
        UpdateAllModels();
    }

    public static void UpdateAllModels() {
        foreach (Injury injury in InjuryManager.injuries) {
            if (injury.HasMarker()) injury.Marker.GetParent().SetActive(IsActiveModel(injury == InjuryManager.activeInjury));
        }
    }

}
