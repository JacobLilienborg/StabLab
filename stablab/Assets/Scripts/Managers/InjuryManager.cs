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
    private ModelController modelController;

    public static List<Injury> injuries = new List<Injury>();
    private Injury activeInjury;

    public GameObject testObj;

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
        Injury newInjury = new Injury(DateTime.Now);
        activeInjury = newInjury;
        injuries.Add(activeInjury);
        injuryAdding.currentInjuryState = InjuryState.Add;
    }

    // Remove the currently active injury
    public void RemoveInjury()
    {
        injuries.Remove(activeInjury);
    }

    public void SetActiveInjury(int index = -1, int id = -1)
    {
       //activeInjury = 
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
            injury.InjuryMarkerObj = injuryAdding.LoadMarker(injury);
        }
    }

    public void SaveBodyPose()
    {
        activeInjury.BodyPose = modelController.GetBodyPose();
        activeInjury.Marker.MarkerUpdate(activeInjury.InjuryMarkerObj);
    }

    public void AddImage()
    {
        string imagePath = FileManager.OpenFileBrowser("png");
        activeInjury.AddImage(FileManager.ReadBytes(imagePath));
        Texture2D img = new Texture2D(2, 2);
        img.LoadImage(activeInjury.images[0]);
        testObj.GetComponent<RawImage>().texture = img;
    }
}
