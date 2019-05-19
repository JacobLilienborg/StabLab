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
    public InjuryManager instance = null;

    // Scripts from the body model
    private static InjuryAdding injuryAdding;
    public static List<Injury> injuries = new List<Injury>();
    public static Injury activeInjury = null;

    // Setting up the listeners system. There are optional events depending of what return type you want
    private static InjuryEvent InjuryActivationEvent = new InjuryEvent();
    private static InjuryEvent InjuryDeactivationEvent = new InjuryEvent();
    private static IndexEvent IndexActivationEvent = new IndexEvent();
    private static IndexEvent IndexDeactivationEvent = new IndexEvent();
    private static UnityEvent ActivationEvent = new UnityEvent();
    private static UnityEvent DeactivationEvent = new UnityEvent();

    public static void AddActivationListener(UnityAction<Injury> action)
    {
        InjuryActivationEvent.AddListener(action);
    }
    public static void AddDeactivationListener(UnityAction<Injury> action)
    {
        InjuryDeactivationEvent.AddListener(action);
    }
    public static void AddActivationListener(UnityAction<int> action)
    {
        IndexActivationEvent.AddListener(action);
    }
    public static void AddDeactivationListener(UnityAction<int> action)
    {
        IndexDeactivationEvent.AddListener(action);
    }
    public static void AddActivationListener(UnityAction action)
    {
        ActivationEvent.AddListener(action);
    }
    public static void AddDeactivationListener(UnityAction action)
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
        if(body == null) return;
        injuryAdding = body.GetComponent<InjuryAdding>();
        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public static void AddNewInjury()
    {
        Injury newInjury = new UndefinedInjury(Guid.NewGuid());
        injuries.Add(newInjury);
    }

    // Remove the currently active injury
    public static void RemoveInjury()
    {
        injuries.Remove(activeInjury);
    }

    // Sets the active injury by id. Is called from the marker that is clicked
    public static void SetActiveInjury(Guid id)
    {
        for (int index = 0; index < injuries.Count; index++)
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
    public static void SetActiveInjury(int index)
    {
        if (activeInjury != injuries[index])
        {
            activeInjury = injuries[index];
            InjuryActivationEvent.Invoke(activeInjury);
            IndexActivationEvent.Invoke(index);
            ActivationEvent.Invoke();
            if (activeInjury.HasMarker())
            {
                activeInjury.Marker.GetWeaponModel().SetActive(Settings.IsActiveModel(true));
            }
        }
        
    }

    // Change the active injury.
    public static void SetActiveInjury(Injury newInjury)
    {
        int activeIndex = injuries.IndexOf(activeInjury);
        injuries[activeIndex] = newInjury;
        activeInjury = newInjury;
        InjuryActivationEvent.Invoke(activeInjury);
        IndexActivationEvent.Invoke(activeIndex);
        ActivationEvent.Invoke();
    }

    // Needed this code to be listener
    public static void DeselectInjury(int index)
    {
        if (injuries[index] == activeInjury)
        {
            InjuryDeactivationEvent.Invoke(activeInjury);
            if (activeInjury.HasMarker()) activeInjury.Marker.GetWeaponModel().SetActive(Settings.IsActiveModel(false));
            activeInjury = null;
            IndexDeactivationEvent.Invoke(index);
            DeactivationEvent.Invoke();
        }
    }

    // Change order of injury in the list.
    public static void ChangeOrder(int oldIndex, int newIndex)
    {
        Injury injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
    }

    // Load all injuries from the list in to the scene.
    public static void LoadInjuries()
    {
        foreach (Injury injury in injuries)
        {
            //activeInjury = injury;
            if(injury.Marker != null)
            {
                //ModelController.SetBodyPose(injury.BodyPose);
                injury.InjuryMarkerObj = injuryAdding.LoadMarker(injury);
                injury.Marker.SetWeaponModel(injuryAdding.LoadModel(injury));
            }
        }
    }


    public static void TransformActive(InjuryType type, bool inHole) 
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
        newInjury.SetInHole(inHole);
        SetActiveInjury(newInjury);
    }

    public static Injury GetActiveInjury()
    {
        return activeInjury;
    }

}
