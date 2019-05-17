﻿﻿using System.Collections.Generic;
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

[System.Serializable] public class InjuryEvent : UnityEvent<InjuryController>{}
[System.Serializable] public class IndexEvent : UnityEvent<int>{}

public class InjuryManager : MonoBehaviour
{
    public static InjuryManager instance = null;
    public InjuryController injuryController = null;
    public List<InjuryController> injuries = new List<InjuryController>();
    public InjuryController activeInjury = null;

    // Setting up the listeners system. There are optional events depending of what return type you want
    private InjuryEvent InjuryActivationEvent = new InjuryEvent();
    private InjuryEvent InjuryDeactivationEvent = new InjuryEvent();
    private IndexEvent IndexActivationEvent = new IndexEvent();
    private IndexEvent IndexDeactivationEvent = new IndexEvent();
    private UnityEvent ActivationEvent = new UnityEvent();
    private UnityEvent DeactivationEvent = new UnityEvent();
    private UnityEvent OnChange = new UnityEvent();
    public void AddActivationListener(UnityAction<InjuryController> action)
    {
        InjuryActivationEvent.AddListener(action);
    }
    public void AddDeactivationListener(UnityAction<InjuryController> action)
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
        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void CreateInjury()
    {
        InjuryData injuryData = new UndefinedInjuryData(Guid.NewGuid());
        InjuryController ic = Instantiate(injuryController, transform);
        ic.injuryData = injuryData;
        injuries.Add(ic);
        OnChange.Invoke();
    }
    public void LoadInjury(InjuryData newInjury)
    {
        /*
        InjuryController ic = Instantiate(injuryController, transform);
        ic.injury = newInjury;
        //ic.PlaceInjury()
        injuries.Add(ic);
        OnChange.Invoke();
        */
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
            if (injuryController.injuryData.id == id)
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
            //if (activeInjury.injury.HasMarker())
            //{
            //    activeInjury.injury.Marker.GetParent().SetActive(Settings.IsActiveModel(true));
            //}
        }

    }

    // Set the active injury
    public void ActivateInjury(InjuryController injuryController)
    {
        //int activeIndex = injuries.IndexOf(activeInjury);
        //injuries[activeIndex].injury = newInjury;
        //activeInjury = newInjury;

    }

    // Needed this code to be listener
    public void DeactivateInjury(int index)
    {
        if (injuries[index] == activeInjury)
        {
            IndexDeactivationEvent.Invoke(index);
            DeactivationEvent.Invoke();
            //if (activeInjury.injury.HasMarker()) activeInjury.injury.Marker.GetParent().SetActive(Settings.IsActiveModel(false));
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
            //if(injury.injury.Marker != null)
            //{

            //}
        }
    }
}
