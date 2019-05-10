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

    private void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }



    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
        onSceneChange.Invoke((Scenes)index);
    }
}
