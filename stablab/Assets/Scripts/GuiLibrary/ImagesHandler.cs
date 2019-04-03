using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesHandler : MonoBehaviour
{
    [SerializeField] private RectTransform imageArea;
    [SerializeField] private UnityEngine.UI.Button addButton;
    [SerializeField] private UnityEngine.UI.RawImage emptyImage;
    [SerializeField] private UnityEngine.UI.Button previousButton;
    [SerializeField] private UnityEngine.UI.Button nextButton;

    [SerializeField] private InjuryManager injuryManager;
   
    private List<UnityEngine.UI.RawImage> images = new List<UnityEngine.UI.RawImage>();
   

    public void AddImage()
    {   
        string imagePath = FileManager.OpenFileBrowser("png,jpg"); // Change to , instead of blancspace, resulting in correct behaviour on windows
        Texture2D imgTexture = new Texture2D(2, 2);
        imgTexture.LoadImage(FileManager.ReadBytes(imagePath));
        // --------

        UnityEngine.UI.RawImage image = Instantiate(emptyImage, imageArea);
        image.texture = imgTexture;
        float ratio = image.texture.width / (float)image.texture.height;
        float w, h;

        w = imageArea.rect.width;
        h = w / ratio;

        image.rectTransform.sizeDelta = new Vector2(w, h);

        if(image.rectTransform.rect.height > imageArea.rect.height) 
        {
            h = imageArea.rect.height;
            w = h * ratio;

            image.rectTransform.sizeDelta = new Vector2(w, h);
        }


        addButton.gameObject.SetActive(false);
    }


}
