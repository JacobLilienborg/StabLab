using System.Collections.Generic;
using UnityEngine;
using System;

public class InjuryManager : MonoBehaviour
{
    public InjuryManager instance = null;

    public static List<InjuryData> injuries = new List<InjuryData>();
    private static InjuryAdding injuryAdding;
    private static InjuryData activeInjury;
    public static int numOfInjuries = 0;

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

        //Don't destroy when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    // Find the model and load markers in to the scene
    public void Start()
    {
        injuryAdding = GameObject.FindWithTag("Player").GetComponent<InjuryAdding>();
        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void AddNewInjury() 
    {
        numOfInjuries++;
        InjuryData newInjury = new InjuryData(numOfInjuries - 1); //ID is numOfInjuries - 1
        activeInjury = newInjury;
        injuries.Add(activeInjury);
        injuryAdding.currentInjuryState = InjuryState.Add;
    }

    // Remove the currently active injury
    public void RemoveInjury()
    {
        injuries.Remove(activeInjury);
    }

    // Sets the active injury. Is called from the marker that is clicked
    public static void SetActiveInjury(int index = -1, int id = -1)
    {
        if (index != -1)
        {
            activeInjury = injuries[index];
            return;
        }
        foreach (InjuryData injury in injuries)
        {
            if (injury.Id == id)
            {
                activeInjury = injury;
                return;
            }
        }
    }

    // Change order of injuri in the list.
    public void ChangeOrder(int oldIndex, int newIndex) 
    {
        InjuryData injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
    }

    // Add a marker to the injury.
    public void AddInjuryMarker(GameObject markerObj)
    {
        activeInjury.InjuryMarkerObj = markerObj;
        foreach (InjuryData injury in injuries)
        {
            Debug.Log("Injury id: " + injury.Id);
        }
    }

    // Load all injuries from the list in to the scene.
    public static void LoadInjuries() 
    {
        foreach (InjuryData injury in injuries) 
        {
            injury.InjuryMarkerObj = injuryAdding.LoadMarker(injury.MarkerData);
        }
    }
}
