using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class InjuryManager : MonoBehaviour
{
    public InjuryManager instance = null;

    // Scripts from the body model
    private static InjuryAdding injuryAdding;

    public static Injury activeInjury;
    private ModelController modelController;

    public static List<Injury> injuries = new List<Injury>();

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
        GameObject body = GameObject.FindWithTag("Player");
        injuryAdding = body.GetComponent<InjuryAdding>();
        modelController = body.GetComponent<ModelController>();

        LoadInjuries();
    }

    // Creates and add the new injury to the list of injuries.
    public void AddNewInjury()
    {
        Injury newInjury = new Injury(Guid.NewGuid());
        injuries.Add(newInjury);
        //injuryAdding.currentInjuryState = InjuryState.Add;
        Debug.Log(newInjury.Id);
    }

    // Remove the currently active injury
    public void RemoveInjury()
    {
        injuries.Remove(activeInjury);
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
        activeInjury = index == -1 ? null: injuries[index];
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
            if(injury.Marker != null)
            {
                injury.InjuryMarkerObj = injuryAdding.LoadMarker(injury);
            }
        }
    }

    // Save current pose
    public void SaveBodyPose()
    {
        activeInjury.BodyPose = modelController.GetBodyPose();
        activeInjury.Marker.MarkerUpdate(activeInjury.InjuryMarkerObj);
    }

    //Add an image to injury
    public void AddImage()
    {
        string imagePath = FileManager.OpenFileBrowser("png jpg");
        if(imagePath != "")
        {
            activeInjury.AddImage(FileManager.ReadBytes(imagePath));
        }
    }
}
