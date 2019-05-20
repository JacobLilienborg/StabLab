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

public class DisableButtons : MonoBehaviour
{

    private Button typeButton;
    private Button positionButton;
    private Button modelButton;
    private Button colorButton;

    private Button sceneChangeButton;

    private Button positionDoneButton;

    public List<Button> standardDisabled = new List<Button>();

    private ButtonEvent typeEvent = new ButtonEvent();
    private ButtonEvent positionEvent = new ButtonEvent();
    private ButtonEvent modelEvent = new ButtonEvent();
    private ButtonEvent colorEvent = new ButtonEvent();

    private UnityEvent ModelEnabledEvent = new UnityEvent();
    private UnityEvent ModelDisabledEvent = new UnityEvent();

    private void Start()
    {
        ModelManager.instance.modelEnabledEvent.AddListener(EnableSceneChange);
        ModelManager.instance.modelDisabledEvent.AddListener(DisableSceneChange);

        ModelDisabledEvent.AddListener(DisableSceneChange);
        typeEvent.AddListener(ButtonPressed);
        positionEvent.AddListener(ButtonPressed);

        if(typeButton != null) typeButton.onClick.AddListener(InvokeTypeButton);

        if (positionButton != null)
        {
            standardDisabled.Add(positionDoneButton);
            positionButton.onClick.AddListener(InvokePositionButton);
        }

        if (modelButton != null)
        {
            standardDisabled.Add(modelButton);
            modelButton.onClick.AddListener(InvokeModelButton);
        }

        if (colorButton != null)
        {
            standardDisabled.Add(colorButton);
            colorButton.onClick.AddListener(InvokeColorButton);
        }

        InjuryAdding.AddPositionChangeListener(PositionConfirmEnable);
        InjuryAdding.AddPositionResetListener(PositionConfirmDisable);
        InjuryManager.AddActivationListener(SetButtonInteractability);

        SetButtonInteractability();
    }

    private void SetButtonInteractability()
    {
        if (InjuryManager.activeInjury != null && InjuryManager.activeInjury.HasMarker())
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

    public void InvokeTypeButton()
    {
        typeEvent.Invoke(ButtonType.typeButton);
    }

    public void InvokePositionButton()
    {
        positionEvent.Invoke(ButtonType.positionButton);
    }

    public void InvokeModelButton()
    {
        modelEvent.Invoke(ButtonType.modelButton);
    }

    public void InvokeColorButton()
    {
        colorEvent.Invoke(ButtonType.colorButton);
    }

    public void HideButton(Button button)
    {
        button.interactable = false;
    }

    public void ShowButton(Button button)
    {
        button.interactable = true;
    }

    private void ButtonPressed(ButtonType button)
    {
        switch (button) {
            case ButtonType.typeButton:
                //ShowButton(positionButton);
                break;
            case ButtonType.positionButton:
                ShowButton(modelButton);
                ShowButton(colorButton);
                break;
        }
    }

    private void PositionConfirmEnable()
    {
        ShowButton(positionDoneButton);
    }

    private void PositionConfirmDisable()
    {
        HideButton(positionDoneButton);
    }

    private void EnableSceneChange()
    {
        ShowButton(sceneChangeButton);
    }

    private void DisableSceneChange()
    {
        HideButton(sceneChangeButton);
    }


}
