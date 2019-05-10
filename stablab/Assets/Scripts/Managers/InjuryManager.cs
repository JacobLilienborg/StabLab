using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;


public enum InjuryType
{
    Undefined,
    Crush,
    Cut,
    Shot,
    Stab,
}


/*
 * InjuryManager manages a list of injuries such as adding and removing injuries.
 * It allso takes care of wich injury, if any, is the currently active injury.
 */

[System.Serializable]
public class InjuryEvent : UnityEvent<Injury>
{
}

[System.Serializable]
public class IndexEvent : UnityEvent<int>
{
}

public class InjuryManager : MonoBehaviour
{
    public static InjuryManager instance = null;

    // Scripts from the body model
    private InjuryAdding injuryAdding;
    public List<Injury> injuries = new List<Injury>();
    public Injury activeInjury = null;

    // Setting up the listeners system. There are optional events depending of what return type you want
    private InjuryEvent InjuryActivationEvent = new InjuryEvent();
    private InjuryEvent InjuryDeactivationEvent = new InjuryEvent();
    private IndexEvent IndexActivationEvent = new IndexEvent();
    private IndexEvent IndexDeactivationEvent = new IndexEvent();
    private UnityEvent ActivationEvent = new UnityEvent();
    private UnityEvent DeactivationEvent = new UnityEvent();

    public void AddActivationListener(UnityAction<Injury> action)
    {
        InjuryActivationEvent.AddListener(action);
    }
    public void AddDeactivationListener(UnityAction<Injury> action)
    {
        InjuryDeactivationEvent.AddListener(action);
    }
    public void AddActivationListener(UnityAction<int> action)
    {
        IndexActivationEvent.AddListener(action);
    }
    public void AddDeactivationListener(UnityAction<int> action)
    {
        IndexDeactivationEvent.AddListener(action);
    }
    public void AddActivationListener(UnityAction action)
    {
        ActivationEvent.AddListener(action);
    }
    public void AddDeactivationListener(UnityAction action)
    {
        DeactivationEvent.AddListener(action);
    }


    // Setup instance of Injury Manager
    void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Don't destroy when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    // Find the model and load markers in to the scene
    public void Start()
    {
        GameObject body = GameObject.FindWithTag("Player");
        injuryAdding = body.GetComponent<InjuryAdding>();
        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void AddNewInjury()
    {
        Injury newInjury = new UndefinedInjury(Guid.NewGuid());
        injuries.Add(newInjury);
    }

    // Remove the currently active injury
    public void RemoveInjury()
    {
        injuries.Remove(activeInjury);
    }

    // Sets the active injury by id. Is called from the marker that is clicked
    public void SetActiveInjury(Guid id)
    {
        for(int index = 0; index < injuries.Count; index++)
        {
            Injury injury = injuries[index];
            if (injury.Id == id)
            {
                if (activeInjury != injury)
                {
                    activeInjury = injury;
                    InjuryActivationEvent.Invoke(activeInjury);
                    IndexActivationEvent.Invoke(index);
                    ActivationEvent.Invoke();
                    return;
                }
            }
        }
    }

    // Sets the active injury by index.
    public void SetActiveInjury(int index)
    {
        if(activeInjury != injuries[index])
        {

            activeInjury = injuries[index];
            InjuryActivationEvent.Invoke(activeInjury);
            IndexActivationEvent.Invoke(index);
            ActivationEvent.Invoke();
            if (activeInjury.HasMarker())
            {
                activeInjury.Marker.GetParent().SetActive(Settings.IsActiveModel(true));
            }
        }
        
    }

    // Set the active injury 
    public void SetActiveInjury(Injury newInjury)
    {
        int activeIndex = injuries.IndexOf(activeInjury);
        injuries[activeIndex] = newInjury;
        activeInjury = newInjury;

    }

    // Needed this code to be listener
    public void DeselectInjury(int index)
    {
        if (injuries[index] == activeInjury)
        {
            InjuryDeactivationEvent.Invoke(activeInjury);
            IndexDeactivationEvent.Invoke(index);
            DeactivationEvent.Invoke();
            if (activeInjury.HasMarker()) activeInjury.Marker.GetParent().SetActive(Settings.IsActiveModel(false));
            activeInjury = null;
             //invoke null? 
        }
    }

    // Change order of injuri in the list.
    public void ChangeOrder(int oldIndex, int newIndex)
    {
        Injury injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
    }

    // Load all injuries from the list in to the scene.
    public void LoadInjuries()
    {
        foreach (Injury injury in injuries)
        {
            activeInjury = injury;
            if(injury.Marker != null)
            {
                injury.InjuryMarkerObj = injuryAdding.LoadMarker(injury);
                injury.AddModel(injuryAdding.LoadModel(injury));
            }
        }
    }


    public void TransformActive(InjuryType type) 
    {
        Injury newInjury;

        switch(type) 
        {
            case InjuryType.Shot:
                newInjury = new ShotInjury(activeInjury);
                break;
            case InjuryType.Crush:
                newInjury = new CrushInjury(activeInjury);
                break;
            case InjuryType.Cut:
                newInjury = new CutInjury(activeInjury);
                break;
            case InjuryType.Stab:
                newInjury = new StabInjury(activeInjury);
                break;
            case InjuryType.Undefined:
                newInjury = new UndefinedInjury(activeInjury);
                break;
            default:
                newInjury = new UndefinedInjury(activeInjury);
                Debug.Log("Unknown type");
                break;

        }

        SetActiveInjury(newInjury);
    }

}
