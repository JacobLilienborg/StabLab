using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanel : MonoBehaviour
{
    public LeftPanelAnimation panelAnimation;
    public GameObject woundTypehooser;
    public GameObject injurySettings;
    public GameObject positionChooser;
    public GameObject certantyVhooser;
    public GameObject PoseChooser;

    private List<GameObject> states = new List<GameObject>();

    public void Start()
    {
        states.Add(woundTypehooser);
        states.Add(injurySettings);
        states.Add(positionChooser);
        states.Add(certantyVhooser);
        states.Add(PoseChooser);
    }

    public void OpenPanel()
    {
        UpdatePanel();
        panelAnimation.OpenPanel();
    }

    public void ClosePanel()
    {
        panelAnimation.ClosePanel();
    }

    private void UpdatePanel() 
    {
        Injury injury = InjuryManager.activeInjury;

        if (injury.Type == InjuryType.Null) 
        {
            SetState(woundTypehooser);
        }
        else
        {
            SetState(injurySettings);
        }
    }

    public void SetState(GameObject newState)
    {
        foreach(GameObject state in states)
        {
            state.SetActive(false);
        }
        newState.SetActive(true);
    }
}
