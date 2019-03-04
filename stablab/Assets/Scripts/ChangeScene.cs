using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string scene;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GoToScene);
    }

    void GoToScene() 
    {
        Debug.Log(Application.persistentDataPath);
        SceneManager.LoadScene(scene);
    }

}
