﻿using System.Collections.Generic;
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
public class IndexEvent : UnityEvent<int>{}

public class InjuryManager : MonoBehaviour
{
    public static InjuryManager instance = null;
    public InjuryController injuryController = null;
    public List<InjuryController> injuries = new List<InjuryController>();
    public InjuryController activeInjury = null;

    // Setting up the listeners system. There are optional events depending of what return type you want
    private IndexEvent IndexActivationEvent = new IndexEvent();
    private IndexEvent IndexDeactivationEvent = new IndexEvent();
    private UnityEvent ActivationEvent = new UnityEvent();
    private UnityEvent DeactivationEvent = new UnityEvent();
    private UnityEvent OnChange = new UnityEvent();

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
        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void CreateInjury()
    {
        Injury injury = new UndefinedInjury(Guid.NewGuid());
        InjuryController ic = Instantiate(injuryController, transform);
        ic.injury = injury;
        injuries.Add(ic);
        OnChange.Invoke();
    }
    public void LoadInjury(Injury newInjury)
    {
        InjuryController ic = Instantiate(injuryController, transform);
        ic.injury = newInjury;
        //ic.PlaceInjury()
        injuries.Add(ic);
        OnChange.Invoke();
    }

    public void PlaceInjury(Transform mesh, Transform bone)
    {
        // If no active injury is present, create one
        if(mesh == null || bone == null)
        {
            return;
        }
        if(activeInjury.markerCollider == null)
        {
            activeInjury.injury.InjuryMarkerObj = Instantiate((GameObject)Resources.Load(activeInjury.injury.ModelName), mesh.position, Quaternion.identity, bone);
        }
        else 
        {
            activeInjury.injury.InjuryMarkerObj.transform.position = mesh.position;
            activeInjury.injury.InjuryMarkerObj.transform.parent = bone;
        }
    }

    // Remove the currently active injury
    public void RemoveInjury()
    {
        injuries.Remove(activeInjury);
        OnChange.Invoke();
    }

    // Sets the active injury by id. Is called from the marker that is clicked
    public void ActivateInjury(Guid id)
    {
        for(int index = 0; index < injuries.Count; index++)
        {
            InjuryController injuryController = injuries[index];
            if (injuryController.injury.Id == id)
            {
                if (activeInjury != injuryController)
                {
                    activeInjury = injuryController;
                    IndexActivationEvent.Invoke(index);
                    ActivationEvent.Invoke();
                    return;
                }
            }
        }
    }

    // Sets the active injury by index.
    public void ActivateInjury(int index)
    {
        if(activeInjury != injuries[index])
        {
            activeInjury = injuries[index];
            IndexActivationEvent.Invoke(index);
            ActivationEvent.Invoke();
            if (activeInjury.injury.HasMarker())
            {
                activeInjury.injury.Marker.GetParent().SetActive(Settings.IsActiveModel(true));
            }
        }
        
    }

    // Set the active injury 
    public void ActivateInjury(Injury newInjury)
    {
        //int activeIndex = injuries.IndexOf(activeInjury);
        //injuries[activeIndex].injury = newInjury;
        //activeInjury = newInjury;

    }

    // Needed this code to be listener
    public void DeselectInjury(int index)
    {
        if (injuries[index] == activeInjury)
        {
            IndexDeactivationEvent.Invoke(index);
            DeactivationEvent.Invoke();
            if (activeInjury.injury.HasMarker()) activeInjury.injury.Marker.GetParent().SetActive(Settings.IsActiveModel(false));
            activeInjury = null;
             //invoke null? 
        }
    }

    // Change order of injuri in the list.
    public void ChangeOrder(int oldIndex, int newIndex)
    {
        InjuryController injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
        OnChange.Invoke();
    }

    // Load all injuries from the list in to the scene.
    public void LoadInjuries()
    {
        foreach (InjuryController injury in injuries)
        {
            activeInjury = injury;
            if(injury.injury.Marker != null)
            {

            }
        }
    }


    public void TransformActive(InjuryType type) 
    {
        Injury newInjury;

        switch(type) 
        {
            case InjuryType.Shot:
                newInjury = new ShotInjury(activeInjury.injury);
                break;
            case InjuryType.Crush:
                newInjury = new CrushInjury(activeInjury.injury);
                break;
            case InjuryType.Cut:
                newInjury = new CutInjury(activeInjury.injury);
                break;
            case InjuryType.Stab:
                newInjury = new StabInjury(activeInjury.injury);
                break;
            case InjuryType.Undefined:
                newInjury = new UndefinedInjury(activeInjury.injury);
                break;
            default:
                newInjury = new UndefinedInjury(activeInjury.injury);
                Debug.Log("Unknown type");
                break;

        }

        ActivateInjury(newInjury);
    }

}
