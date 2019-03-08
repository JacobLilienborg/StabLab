﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryManager : MonoBehaviour
{
    public InjuryManager instance = null;
    public List<InjuryData> injuries;

    public GameObject model;

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


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOrder(int oldIndex, int newIndex) 
    {
        InjuryData injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
    }

    public void AddInjuryMarker(GameObject markerObj, ModelData model)
    {
        MarkerData marker = new MarkerData(markerObj, model);
        InjuryData injuryData = new InjuryData();
        injuryData.markerData = marker;
        injuryData.injuryMarkerObj = markerObj;
        injuries.Add(injuryData);
    }

    public void LoadInjuries(List<InjuryData> injuriesList) 
    {
        injuries = injuriesList;

        foreach(InjuryData injury in injuries) 
        {
            MarkerData markerData = injury.markerData;
            model.transform.position = markerData.modelPose.GetPosition();
            model.transform.rotation = markerData.modelPose.GetRotation();
            injury.injuryMarkerObj = model.GetComponent<InjuryAdding>().MakeMarker(markerData.type, markerData.GetPosition());
        }
    }
}
