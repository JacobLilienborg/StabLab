using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager : MonoBehaviour
{

    static private ViewManager instance = null;

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
    
    private void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            changeScene(0);
        }
        else if (Input.GetKeyDown("1"))
        {
            changeScene(1);
        }
        else if (Input.GetKeyDown("2"))
        {
            changeScene(2);
        }
    }


    public void changeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void FadeIn(int index, float fadeTime = 0.0f)
    {
       
    }

    public void FadeOut(int index, float fadeTime = 0.0f)
    {

    }
}
