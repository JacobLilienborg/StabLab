using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryManager : MonoBehaviour
{
    public InjuryManager instance = null;

    [SerializeField]
    public GameObject bodyHelper;
    public static List<InjuryData> injuries = new List<InjuryData>();

    private InjuryData currentInjury;
    private static GameObject body;

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

        body = bodyHelper;
    }


    public void AddNewInjury() 
    {
        InjuryData newInjury = new InjuryData();
        currentInjury = newInjury;
        injuries.Add(currentInjury);
        body.GetComponent<InjuryAdding>().currentInjuryState = InjuryState.Add;
    }

    public void RemoveInjury(int index = -1)
    {
        if (index == -1) 
        {
            injuries.Remove(currentInjury);
        }
        else 
        {
            injuries.RemoveAt(index); 
        }
    }

    // OBS: this will not work and has to bee fixed later
    public void ChangeOrder(int oldIndex, int newIndex) 
    {
        InjuryData injury = injuries[oldIndex];
        injuries.RemoveAt(oldIndex);
        injuries.Insert(newIndex, injury);
    }

    public void AddInjuryMarker(GameObject markerObj)
    {
        currentInjury.InjuryMarkerObj = markerObj;
    }

    public static void LoadInjuries(List<InjuryData> injuriesList) 
    {
        injuries = injuriesList;

        foreach(InjuryData injury in injuries) 
        {
            Debug.Log("----" + injury.MarkerData.Position);
           injury.InjuryMarkerObj = body.GetComponent<InjuryAdding>().LoadMarker(injury.MarkerData);
        }
    }
}
