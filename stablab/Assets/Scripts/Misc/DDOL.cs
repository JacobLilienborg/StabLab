using UnityEngine;

public class DDOL : MonoBehaviour
{
    static private DDOL instance = null;

    // Start is called before the first frame update
    private void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }
}
