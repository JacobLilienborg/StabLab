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
    public UnityEvent OnChange = new UnityEvent();
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
        GameObject go = new GameObject("injury" + injuries.Count, typeof(InjuryController));
        go.transform.SetParent(transform);
        InjuryController ic = go.GetComponent<InjuryController>();
        ic.injuryData = injuryData;
        injuries.Add(ic);
        ActivateInjury(ic.injuryData.id);
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
        if(!activeInjury) return;
        injuries.Remove(activeInjury);
        InjuryController ic = activeInjury;
        DeactivateInjury(activeInjury);
        Destroy(ic.gameObject);
        OnChange.Invoke();
    }

    // Sets the active injury by id. Is called from the marker that is clicked
    public void ActivateInjury(Guid id)
    {
        ActivateInjury(injuries.FindIndex(x => x.injuryData.id == id));
    }

    // Sets the active injury by index.
    public void ActivateInjury(int index)
    {
        if (index != -1 && activeInjury != injuries[index])
        {
            if(activeInjury) activeInjury.ToggleWeapon(false);
            activeInjury = injuries[index];
            activeInjury.ToggleWeapon(true);
            IndexActivationEvent.Invoke(index);
            ActivationEvent.Invoke();
            OnChange.Invoke();
        }

    }

    // Set the active injury
    public void ActivateInjury(InjuryController injuryController)
    {
        ActivateInjury(injuries.FindIndex(x => x == injuryController));
    }

    public void DeactivateInjury(int index)
    {
        if (index != -1 && injuries[index] == activeInjury)
        {
            activeInjury = null;
            IndexDeactivationEvent.Invoke(index);
            DeactivationEvent.Invoke();
            OnChange.Invoke();
        }
    }
    public void DeactivateInjury(InjuryController injury)
    {
        DeactivateInjury(injuries.FindIndex(x => x == injuryController));
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
        }
    }
}
