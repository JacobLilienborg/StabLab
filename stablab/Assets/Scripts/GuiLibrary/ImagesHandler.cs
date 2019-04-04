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

    [SerializeField] private InjuryManager injuryManager;

    private List<RawImage> images = new List<RawImage>();
    private int activeIndex = 1; // 0 <= activeIndex >= images.Count , images.Count is addButton 


    public void AddImage()
    {
        string imagePath = FileManager.OpenFileBrowser("png,jpg"); // Change to , instead of blancspace, resulting in correct behaviour on windows
        Texture2D imgTexture = new Texture2D(2, 2);
        imgTexture.LoadImage(FileManager.ReadBytes(imagePath));
        // --------

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
        ShowImage(activeIndex + 1);
    }

    public void ShowPrevImage()
    {
        ShowImage(activeIndex - 1);
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