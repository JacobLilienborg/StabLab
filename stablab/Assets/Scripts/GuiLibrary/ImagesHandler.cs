using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesHandler : MonoBehaviour
{
    [SerializeField] private RectTransform imageArea;
    [SerializeField] private Button addButton;
    [SerializeField] private RawImage emptyImage;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    
    private List<RawImage> images = new List<RawImage>();
    private int activeIndex = 1; // 0 <= activeIndex >= images.Count , images.Count is addButton

    public void LoadAllImages() 
    {
        foreach(RawImage image in images) { Destroy(image.gameObject); }
        images.Clear();

        for(int i = 0; i < InjuryManager.activeInjury.images.Count; i++)
        {
            LoadImage(i);
        }

        ShowImage(0);
    }


    public void AddImage()
    {
        string imagePath = FileManager.OpenFileBrowser("png,jpg"); // Let the user pick an image
        InjuryManager.activeInjury.AddImage(FileManager.ReadBytes(imagePath)); // Save the image to active injury
        LoadImage(InjuryManager.activeInjury.images.Count -1);
    }


    private void LoadImage(int index) 
    {
        Texture2D imgTexture = new Texture2D(2, 2);
        imgTexture.LoadImage(InjuryManager.activeInjury.images[index]);

        RawImage image = Instantiate(emptyImage, imageArea);
        image.texture = imgTexture;

        float ratio = image.texture.width / (float)image.texture.height;
        float w, h;

        w = imageArea.rect.width;
        h = w / ratio;

        image.rectTransform.sizeDelta = new Vector2(w, h);

        if (image.rectTransform.rect.height > imageArea.rect.height)
        {
            h = imageArea.rect.height;
            w = h * ratio;

            image.rectTransform.sizeDelta = new Vector2(w, h);
        }

        images.Add(image);
        ShowImage(images.Count -1);
    }

    public void ShowNextImage()
    {
        if(activeIndex < images.Count)
        {
            ShowImage(activeIndex + 1);
        }
    }

    public void ShowPrevImage()
    {
        if(activeIndex >= 1) 
        {
            ShowImage(activeIndex - 1);
        }
    }

    private void ShowImage(int index)
    {
        for(int i = 0; i < images.Count; i++)
        {
            images[i].gameObject.SetActive(i == index);
        }

        addButton.gameObject.SetActive(index == images.Count);

        activeIndex = index;
    }

}
