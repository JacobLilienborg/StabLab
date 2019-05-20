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

    public Button typeButton;
    public Button positionButton;
    public Button modelButton;
    public Button colorButton;

    public Button positionDoneButton;

    public List<Button> standardDisabled = new List<Button>();

    public ButtonEvent typeEvent = new ButtonEvent();
    public ButtonEvent positionEvent = new ButtonEvent();
    public ButtonEvent modelEvent = new ButtonEvent();
    public ButtonEvent colorEvent = new ButtonEvent();

    private void Start()
    {
        standardDisabled.Add(positionDoneButton);
        standardDisabled.Add(modelButton);
        standardDisabled.Add(colorButton);

        typeEvent.AddListener(ButtonPressed);
        positionEvent.AddListener(ButtonPressed);
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
}
