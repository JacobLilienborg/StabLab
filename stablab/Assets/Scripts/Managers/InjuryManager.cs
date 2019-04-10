using System.Collections.Generic;
using UnityEngine;
using System;

public class InjuryManager : MonoBehaviour, IInjuryManager
{
    public InjuryManager instance = null;

    // Scripts from the body model
    private static InjuryAdding injuryAdding;

    public static Injury activeInjury;
    private ModelController modelController;

    public static List<Injury> injuries { get; set; }

    private InjuryManagerLogic injuryManagerLogic;
    


    // Setup instance of ProjectManager
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
        if (injuryManagerLogic == null) { injuryManagerLogic = new InjuryManagerLogic(); }

        //Don't destroy when reloading scene
        DontDestroyOnLoad(gameObject);
        injuries = new List<Injury>();

    }

    // Find the model and load markers in to the scene
    public void Start()
    {
        GameObject body = GameObject.FindWithTag("Player");
        injuryAdding = body.GetComponent<InjuryAdding>();
        modelController = body.GetComponent<ModelController>();

        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void AddNewInjury()
    {
        Injury newInjury = new Injury(Guid.NewGuid());
        activeInjury = newInjury;
        injuries.Add(activeInjury);
        injuryAdding.currentInjuryState = InjuryState.Add;
        Debug.Log(activeInjury.Id);
    }

    // Sets the active injury by id. Is called from the marker that is clicked
    public static void SetActiveInjury(Guid id)
    {
        foreach (Injury injury in injuries)
        {
            if (injury.Id == id)
            {
                activeInjury = injury;
                return;
            }
        }
    }

    // Sets the active injury by index. Is called from the marker that is clicked
    public static void SetActiveInjury(int index)
    {
        activeInjury = injuries[index];
    }

    // Change order of injuri in the list.
    public void ChangeOrder(int oldIndex, int newIndex)
    {
        Injury injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
    }

    // Add a marker to the injury.
    public void AddInjuryMarker(GameObject markerObj)
    {
        activeInjury.InjuryMarkerObj = markerObj;
        SaveBodyPose();
    }

    // Load all injuries from the list in to the scene.
    public static void LoadInjuries()
    {
        foreach (Injury injury in injuries)
        {
            activeInjury = injury;
            injury.InjuryMarkerObj = injuryAdding.LoadMarker(injury);
        }
    }

    public void SaveBodyPose()
    {
        activeInjury.BodyPose = modelController.GetBodyPose();
        activeInjury.Marker.MarkerUpdate(activeInjury.InjuryMarkerObj);
    }

    public void RemoveInjury()
    {
        injuryManagerLogic.RemoveInjury(injuries, activeInjury);
    }
}

public class InjuryManagerLogic
{
    // Remove the currently active injury
    public void RemoveInjury(List<Injury> injuries, Injury activeInjury)
    {
        injuries.Remove(activeInjury);
    }
}