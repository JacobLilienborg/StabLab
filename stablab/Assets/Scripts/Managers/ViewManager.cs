using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum Scenes {start, creation, editing, presentation};

[System.Serializable]
public class SceneEvent : UnityEvent<Scenes>
{
}

public class ViewManager : MonoBehaviour
{

    public SceneEvent onSceneChange;
    public static ViewManager instance;
    public Scenes scene;

    private void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            scene = instance.scene;
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }



    public void ChangeScene(int index)
    {
        scene = (Scenes)index;
        onSceneChange.Invoke((Scenes)index);
        SceneManager.LoadScene(index);
       
    }
}
