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

[System.Serializable] public class InjuryEvent : UnityEvent<InjuryController> { }
[System.Serializable] public class IndexEvent : UnityEvent<int> { }

public class InjuryManager : MonoBehaviour
{
    public static InjuryManager instance = null;
    public InjuryController injuryController = null;
    public List<InjuryController> injuries = new List<InjuryController>();
    public InjuryController activeInjury = null;
    public int activeIndex { get; private set; }


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
        //LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void CreateInjury()
    {
        InjuryData injuryData = new UndefinedInjuryData(Guid.NewGuid());
        CreateInjury(injuryData);
    }


    public void CreateInjury(InjuryData injuryData)
    {
        GameObject go = new GameObject("injury" + injuries.Count, typeof(InjuryController));
        go.transform.SetParent(transform);
        InjuryController ic = go.GetComponent<InjuryController>();
        ic.injuryData = injuryData;
        injuries.Add(ic);
        ActivateInjury(ic);
    }


    // Remove the currently active injury
    public void RemoveInjury()
    {
        if (!activeInjury) return;
        InjuryController ic = activeInjury;
        DeactivateInjury(activeInjury);
        Destroy(ic.gameObject);
        injuries.Remove(ic);
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
            if (activeInjury)
            {
                activeInjury.ToggleWeapon(false);
                activeInjury.RemoveGizmo();
            }
            activeInjury = injuries[index];
            activeIndex = index;
            activeInjury.ToggleWeapon(true);
            InjuryActivationEvent.Invoke(activeInjury);
            IndexActivationEvent.Invoke(index);
            ActivationEvent.Invoke();
            activeInjury.FetchCamera();
            activeInjury.FetchPose();
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
        if (index != -1)
        {
            if (activeInjury) activeInjury.ToggleWeapon(false);
            InjuryDeactivationEvent.Invoke(activeInjury);
            IndexDeactivationEvent.Invoke(index);
            DeactivationEvent.Invoke();
            activeInjury = null;
            activeIndex = -1;
            OnChange.Invoke();
        }
    }
    public void DeactivateInjury(InjuryController injury)
    {
        DeactivateInjury(injuries.FindIndex(x => x == injury));
    }

    // Change order of injuri in the list.
    public void ChangeOrderOfActive(int steps)
    {
        int newPos = activeIndex + steps;
        if (newPos >= injuries.Count || newPos < 0) return;

        InjuryController injury = injuries[newPos];
        injuries[newPos] = activeInjury;
        injuries[activeIndex] = injury;
        activeIndex += steps;
        OnChange.Invoke();
    }

    // Load all injuries from the list in to the scene.
    public void LoadInjuries(List<InjuryData> injuryDatas)
    {
        if (injuryDatas == null) return;

        foreach (InjuryController injury in injuries)
        {
            Destroy(injury.gameObject);
        }
        injuries.Clear();
        activeInjury = null;

        Transform[] bones = ModelManager.instance.activeModel.skeleton.GetComponentsInChildren<Transform>();
        foreach (InjuryData data in injuryDatas)
        {
            CreateInjury(data);

            if (!string.IsNullOrEmpty(data.boneName))
            {
                foreach (Transform bone in bones) 
                {
                    if (bone.name == data.boneName)
                    {
                        activeInjury.PlaceInjury(data.markerData.transformData.position, bone);
                        activeInjury.FetchMarkerWeapon();
                    }
                }
            }
        }

        OnChange.Invoke();
    }

    // Load all injuries from the list in to the scene.
    public List<InjuryData> GetListOfInjuryData()
    {
        List<InjuryData> injuryDatas = new List<InjuryData>();
        foreach (InjuryController injury in injuries)
        {
            injuryDatas.Add(injury.injuryData);
        }
        return injuryDatas;
    }
}
