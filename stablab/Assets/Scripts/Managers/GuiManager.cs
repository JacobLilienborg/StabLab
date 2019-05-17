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
            ModelManager.instance.activeModel.onClick.AddListener(ModelManager.instance.activeModel.AddGizmo);
        }
        else 
        {
            ModelManager.instance.activeModel.onClick.RemoveListener(ModelManager.instance.activeModel.AddGizmo);
        }
    }
}
