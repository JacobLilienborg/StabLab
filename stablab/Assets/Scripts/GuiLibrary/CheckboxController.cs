using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems; 

public enum Mode
{
    Active, Inactive, Highlighted, Disabled
}

public class CheckboxController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public Sprite highlightedSprite;
    public Sprite disabledSprite;

    public GameObject image;

    public UnityEvent OnChecked;
    public UnityEvent OnUnchecked;

    protected Mode mode = Mode.Inactive;
    private Image currentImage;

    protected void Start()
    {
        currentImage = image.GetComponent<Image>();
    }

    // Update is called once per frame
    protected void Update()
    {
        switch (mode)
        {
            case Mode.Inactive:     currentImage.sprite = inactiveSprite;       break;
            case Mode.Active:       currentImage.sprite = activeSprite;         break;
            case Mode.Highlighted:  currentImage.sprite = highlightedSprite;    break;
            case Mode.Disabled:     currentImage.sprite = disabledSprite;       break;
            default:                currentImage.sprite = disabledSprite;       break;
        }
    }
    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (mode == Mode.Highlighted)
        {
            Debug.Log("sd;jfha");
            mode = Mode.Active;
            OnChecked.Invoke();
        }
        else if (mode == Mode.Active)
        {
            mode = Mode.Highlighted;
            OnUnchecked.Invoke();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (mode == Mode.Inactive) mode = Mode.Highlighted;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (mode == Mode.Highlighted) mode = Mode.Inactive;
    }
}
