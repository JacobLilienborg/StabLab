using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum ButtonType
{
    typeButton, positionButton, modelButton, colorButton
};

[System.Serializable]
public class ButtonEvent : UnityEvent<ButtonType>
{
}

public class DisableButton : MonoBehaviour
{

    public Button positionButton;
    public Button modelButton;
    public Button colorButton;

    public Slider weightSlider;
    public Slider muscleSlider;

    private bool heightChanged = false;
    private bool modelActive = false;

    public Button sceneChangeButton;

    public Button positionDoneButton;

    private List<Button> standardDisabled = new List<Button>();

    private ButtonEvent typeEvent = new ButtonEvent();
    private UnityEvent positionEvent = new UnityEvent();
    private ButtonEvent modelEvent = new ButtonEvent();
    private ButtonEvent colorEvent = new ButtonEvent();

    private void Start()
    {

        ModelManager.instance.heightChangedEvent.AddListener(HeightChanged);
        ModelManager.instance.modelEnabledEvent.AddListener(OnModelActive);
        ModelManager.instance.modelDisabledEvent.AddListener(OnModelInActive);

        if (InjuryManager.instance != null)
            /*Only in injuryMode */
        {
            standardDisabled.Add(colorButton);
            standardDisabled.Add(modelButton);
            standardDisabled.Add(positionDoneButton);
            positionDoneButton.onClick.AddListener(OnPositionSet);

            //InjuryManager.instance.AddActivationListener(OnNewActiveInjury);
            //InjuryManager.instance.AddDeactivationListener(OnDisableActiveInjury);
            SetButtonInteractability();
        }
    }

    private void OnNewActiveInjury(InjuryController activeInjury)
    {
        activeInjury.positionSetEvent.AddListener(PositionConfirmEnable);
        activeInjury.positionResetEvent.AddListener(PositionConfirmDisable);
        SetButtonInteractability();
    }

    private void OnDisableActiveInjury(InjuryController activeInjury)
    {
        activeInjury.positionSetEvent.RemoveListener(PositionConfirmEnable);
        activeInjury.positionResetEvent.RemoveListener(PositionConfirmDisable);
    }

    private void SetButtonInteractability()
    {
        if (InjuryManager.instance.activeInjury != null && InjuryManager.instance.activeInjury.HasMarker())
        {
            foreach (Button b in standardDisabled)
            {
                ShowButton(b);
            }
        }
        else
        {
            foreach (Button b in standardDisabled)
            {
                HideButton(b);
            }
        }
    }

    public void DisableAndResetSliders()
    {

    }

    public void HideButton(Button button)
    {
        button.interactable = false;
    }

    public void ShowButton(Button button)
    {
        button.interactable = true;
    }

    private void OnPositionSet()
    {
        ShowButton(modelButton);
        ShowButton(colorButton);
    }

    private void PositionConfirmEnable()
    {
        ShowButton(positionDoneButton);
    }

    private void PositionConfirmDisable()
    {
        HideButton(positionDoneButton);
    }

    private void HeightChanged()
    {
        heightChanged = true;
        if (modelActive)
        {
            if (ModelManager.instance.activeModel.height > 0) ShowButton(sceneChangeButton);
            else
            {
                HideButton(sceneChangeButton);
                heightChanged = false;
            }
        }
    }

    private void OnModelActive()
    {
        weightSlider.interactable = true;
        muscleSlider.interactable = true;
        if(heightChanged) ShowButton(sceneChangeButton);
        ResetSliders();
        modelActive = true;
    }

    private void OnModelInActive()
    {
        muscleSlider.interactable = false;
        weightSlider.interactable = false;
        HideButton(sceneChangeButton);
        ResetSliders();
        modelActive = false;
    }

    private void ResetSliders()
    {
        weightSlider.value = 0;
        muscleSlider.value = 0;
    }


}

