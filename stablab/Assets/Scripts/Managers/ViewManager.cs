using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager : MonoBehaviour
{

    [SerializeField] public enum Scene {start, creation, editing, presentation};
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



    public void changeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void FadeIn(int index, float fadeTime = 0.0f)
    {
        SceneManager.LoadScene(index);
    }

    public void FadeOut(int index, float fadeTime = 0.0f)
    {

    }
}
