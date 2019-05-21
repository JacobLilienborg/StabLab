using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This Singleton is used to show an image on the entire screen.
/// </summary>
public class FullScreenImageShower : MonoBehaviour
{
    
    public static FullScreenImageShower instance;

    public  DarkenScreen                darkenScreen;
    private UnityEngine.UI.RawImage     image;
    private Vector2                     screenSize;
    public static bool showingFullscreen = false;

    [SerializeField] private GameObject ImagesHandlerObj;
    private ImagesHandler ImagesHandler;
    
    private void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            image      = instance.image;
            Destroy(instance.gameObject);
            instance   = this;
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        ImagesHandler = ImagesHandlerObj.GetComponent<ImagesHandler>();
    }

    void Update() {
        if (showingFullscreen) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                Debug.Log("Right");
                hide();
                show(ImagesHandler.NextImage());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                hide();
                show(ImagesHandler.PrevImage());
            }
        }
    }
    
    // When this method gets called, it shows the incoming image in full screen.
    public void show(UnityEngine.UI.RawImage image)
    {
        ArrowKeysToggler.DeactivateArrowKeys = true;
        showingFullscreen = true;
        float ratio = image.texture.width / (float)image.texture.height;
        float w, h;

        this.image = Instantiate(image, transform);
        
        // Resizes the image so it fits the screen.
        w          = screenSize.x;
        h          = w / ratio;

        this.image.rectTransform.sizeDelta = new Vector2(w, h);

        if (this.image.rectTransform.rect.height > screenSize.y)
        {
            h = screenSize.y;
            w = h * ratio;

            this.image.rectTransform.sizeDelta = new Vector2(w, h);
        }
        
        // Makes it unclickable (darkenScreen is instead clickable).
        this.image.raycastTarget = false;

        // Makes the background darker (It also blocks mouse input on the background).
        darkenScreen.gameObject.SetActive(true);
    }

    // When called, this method hides the image (destroys it).
    public void hide()
    {
        ArrowKeysToggler.DeactivateArrowKeys = false;
        showingFullscreen = false;
        Destroy(image.gameObject);
        image = null;
    }

}
