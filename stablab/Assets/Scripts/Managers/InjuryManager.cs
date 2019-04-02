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

    private static Injury activeInjury;
    public static int numOfInjuries = 0;
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
        numOfInjuries++;
        Injury newInjury = new Injury(numOfInjuries - 1); //ID is numOfInjuries - 1
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
        foreach (Injury injury in injuries)
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
        //TestImage();
    }

    /*
    private void TestImage()
    {   
        GameObject image = Instantiate(emptyImagePrefab);
        image.GetComponent<RawImage>().texture = activeInjury.GetImageTexture(activeInjury.images.Count -1);
        image.transform.SetParent(canvas.transform);
        image.transform.position += new Vector3(100 + (activeInjury.images.Count - 1) * 100, 100, 0);
    }
    /*
    private void TestImages() 
    {
        if (injuries.Count == 0) return;
        for(int i = 0; i < injuries[0].images.Count; i++)
        {
            GameObject image = Instantiate(emptyImagePrefab);
            image.GetComponent<RawImage>().texture = injuries[0].GetImageTexture(i);
            image.transform.SetParent(canvas.transform);
            image.transform.position = new Vector3(100 + i * 100, 100, 0);
        }
    }
    */
}
