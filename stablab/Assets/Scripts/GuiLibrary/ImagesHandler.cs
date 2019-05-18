using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ImageHandler has the functionality to add/remove images to an injury and go through the images one at a time by clicking left/right arrows.
 * 
 * TODO: Remove image, fix left/right arrows.
 */

public class ImagesHandler : MonoBehaviour
{
    [SerializeField] private RectTransform imageArea;
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private InjuryImage emptyImage;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    
    private List<InjuryImage> images = new List<InjuryImage>();
    private int activeIndex = 1; // 0 <= activeIndex >= images.Count , images.Count is addButton

    // We disable the navigation buttons at start.
    private void Start()
    {
        previousButton.interactable = false;
        nextButton.interactable     = false;
    }

    // Load all images saved to the active injury
    public void LoadAllImages() 
    {
        foreach(InjuryImage image in images) { Destroy(image.gameObject); }
        images.Clear();
        if (InjuryManager.activeInjury == null) return;
        for(int i = 0; i < InjuryManager.activeInjury.images.Count; i++)
        {
            LoadImage(i);
        }

        ShowImage(0);
    }

    // Opens the filebrowser and adds the chosen image to the injury.
    public void AddImage()
    {
        string imagePath = FileManager.OpenFileBrowser("png,jpg"); // Let the user pick an image
        InjuryManager.activeInjury.AddImage(FileManager.ReadBytes(imagePath)); // Save the image to active injury
        LoadImage(InjuryManager.activeInjury.images.Count -1);
    }

    // Removes the active image from the injury
    public void RemoveImage()
    {
        Destroy(images[activeIndex].gameObject);
        images.Remove(images[activeIndex]);
        InjuryManager.activeInjury.RemoveImage(activeIndex);
        ShowImage(activeIndex);
    }

    // Load an image in to the UI in the right position.
    private void LoadImage(int index) 
    {
        Texture2D imgTexture = new Texture2D(2, 2);
        imgTexture.LoadImage(InjuryManager.activeInjury.images[index]);
        imgTexture.Compress(false);

        InjuryImage image = Instantiate(emptyImage, imageArea);
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

    // Show next image
    public void ShowNextImage()
    {
        if(activeIndex < images.Count)
        {
            ShowImage(activeIndex + 1);
        }
    }

    // Show previous image
    public void ShowPrevImage()
    {
        if(activeIndex >= 1) 
        {
            ShowImage(activeIndex - 1);
        }
    }

    // Make the image with the index from input visible and the rest invisible
    private void ShowImage(int index)
    {
 
        for(int i = 0; i < images.Count; i++)
        {
            images[i].gameObject.SetActive(i == index);
        }

        if(addButton != null) addButton.gameObject.SetActive(index == images.Count);

        activeIndex = index;
        CheckInteractability();

        //If our active Image is in full screen, we shrink it.
        /*if (activeIndex < images.Count)
        {
            if (images[activeIndex].IsFullScreen)
            {
                images[activeIndex].resize();
            }
        }*/

    }

    // Check if the previous/next button are going to be interactable
    private void CheckInteractability()
    {
        previousButton.interactable = activeIndex > 0;
        if (addButton != null)
        {
            nextButton.interactable = activeIndex < images.Count;
            if (removeButton == null) return;
            removeButton.gameObject.SetActive(activeIndex < images.Count);
        }
        else {
            nextButton.interactable = activeIndex < images.Count - 1;
            if (removeButton == null) return;
            removeButton.gameObject.SetActive(activeIndex < images.Count - 1);
        }
    
    }

}
