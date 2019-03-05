using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UserInput
{
    public delegate bool trigger();
    private trigger inputFunction;
    public UnityEvent eventfunctions;

    public UserInput(trigger t)
    {
        inputFunction = t;
    }

    public bool Triggered()
    {
        return inputFunction();
    }

}

public class InputHandler : MonoBehaviour
{    
    public List<UserInput> userInputs;
    
    void Update()
    {
        foreach(UserInput userInput in userInputs)
        {
            if (userInput.Triggered()) { userInput.eventfunctions.Invoke(); }
        }    
    }
}
