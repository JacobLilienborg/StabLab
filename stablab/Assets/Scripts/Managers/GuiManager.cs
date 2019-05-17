using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangePose(bool active)
    {
        if(active)
        {
            ModelManager.instance.AddOnClickListener(ModelManager.instance.activeModel.AddGizmo);
        }
        else 
        {
            ModelManager.instance.RemoveOnClickListener(ModelManager.instance.activeModel.AddGizmo);
        }
    }
}
