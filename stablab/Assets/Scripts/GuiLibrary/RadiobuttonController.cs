using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadiobuttonController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public Sprite highlightedSprite;
    public Sprite disabledSprite;


    public GameObject image;

    private Image currentImage;

    private Mode mode = Mode.Inactive;

    private void Start()
    {
        currentImage = image.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.Inactive: currentImage.sprite = inactiveSprite; break;
            case Mode.Active: currentImage.sprite = activeSprite; break;
            case Mode.Highlighted: currentImage.sprite = highlightedSprite; break;
            case Mode.Disabled: currentImage.sprite = disabledSprite; break;
            default: currentImage.sprite = disabledSprite; break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mode == Mode.Highlighted) mode = Mode.Active;
        else if (mode == Mode.Active) mode = Mode.Highlighted;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mode == Mode.Inactive) mode = Mode.Highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mode == Mode.Highlighted) mode = Mode.Inactive;
    }
}
