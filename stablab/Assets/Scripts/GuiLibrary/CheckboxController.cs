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

    protected virtual void Start()
    {
        currentImage = image.GetComponent<Image>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (mode)
        {
            case Mode.Inactive: currentImage.sprite = inactiveSprite; break;
            case Mode.Active: currentImage.sprite = activeSprite; break;
            case Mode.Highlighted: currentImage.sprite = highlightedSprite; break;
            case Mode.Disabled: currentImage.sprite = disabledSprite; break;
            default: break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (mode)
        {
            case Mode.Highlighted: Checked(); break;
            case Mode.Active: Unchecked(); break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mode == Mode.Inactive) mode = Mode.Highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mode == Mode.Highlighted) mode = Mode.Inactive;
    }

    public virtual void Checked(bool trigger = true)
    {
        mode = Mode.Active;
        if(trigger) OnChecked.Invoke();
    }
    public virtual void Unchecked(bool trigger = true)
    {
        mode = Mode.Highlighted;
        if(trigger) OnUnchecked.Invoke();
    }
}
