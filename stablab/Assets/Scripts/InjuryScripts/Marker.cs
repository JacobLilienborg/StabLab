using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/*
 * Serializable class to save marker data such as position, rotation and which body part it is attached to.
 */

[Serializable]
public class Marker
{
    [NonSerialized]
    private GameObject parent;
    
    public InjuryType Type { get; set; }
    public bool activeInPresentation = false;
    public string BodyPartParent { get; protected set; }
    private float[] serializedPosMarker = new float[3];
    private float[] serializedRotMarker = new float[4];
    private float[] serializedPosModel = new float[3];
    private float[] serializedRotModel = new float[4];
    public int modelColorIndex = -1;

    public Marker(GameObject markerObj, InjuryType type) 
    {
        MarkerUpdate(markerObj);
        this.Type = type;
        /*stabModel = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Models/STAB", typeof(GameObject));
        cutModel = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Models/CUT", typeof(GameObject));
        shotModel = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Models/SHOT", typeof(GameObject));
        crushModel = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Models/CRUSH", typeof(GameObject));*/
    }

    // Copy data from input object
    public void MarkerUpdate(GameObject markerObj) 
    {
        MarkerPosition = markerObj.transform.position;
        MarkerRotation = markerObj.transform.rotation;
        BodyPartParent = markerObj.transform.parent.name;
        //Type = markerObj.GetComponent<MarkerHandler>().type;
    }

    public Vector3 MarkerPosition
    {
        protected set
        {
            serializedPosMarker[0] = value.x;
            serializedPosMarker[1] = value.y;
            serializedPosMarker[2] = value.z;
        }

        get
        {
            return new Vector3(serializedPosMarker[0], serializedPosMarker[1], serializedPosMarker[2]);
        }
    }

    public Quaternion MarkerRotation
    {
        protected set
        {
            serializedRotMarker[0] = value.x;
            serializedRotMarker[1] = value.y;
            serializedRotMarker[2] = value.z;
            serializedRotMarker[3] = value.w;
        }

        get
        {
            return new Quaternion(serializedRotMarker[0], serializedRotMarker[1], 
                                  serializedRotMarker[2], serializedRotMarker[3]);
        }
    }

    public Vector3 ModelPosition
    {
        set
        {
            serializedPosModel[0] = value.x;
            serializedPosModel[1] = value.y;
            serializedPosModel[2] = value.z;
        }

        get
        {
            return new Vector3(serializedPosModel[0], serializedPosModel[1], serializedPosModel[2]);
        }
    }

    public Quaternion ModelRotation
    {
        set
        {
            serializedRotModel[0] = value.x;
            serializedRotModel[1] = value.y;
            serializedRotModel[2] = value.z;
            serializedRotModel[3] = value.w;
        }

        get
        {
            return new Quaternion(serializedRotModel[0], serializedRotModel[1],
                                  serializedRotModel[2], serializedRotModel[3]);
        }
    }

    public void RemoveModel() {
        serializedPosModel = null;
        serializedPosModel = null;
        GameObject.Destroy(parent);
    }

    public void UpdateModel() {
        parent.transform.position = ModelPosition;
        parent.transform.rotation = ModelRotation;

    }

    public void RemoveMarker() {
        serializedPosMarker = null;
        serializedRotMarker = null;
        RemoveModel();
    }

    public void SetModelRotation(Quaternion rotation) {
        ModelRotation = rotation;
        UpdateModel();
    }

    public void SetModelPosition(Vector3 position)
    {
        ModelPosition = position;
        UpdateModel();
    }

    public void SetParent(GameObject parent) {
        this.parent = parent;
        SetModelPosition(parent.transform.position);
        SetModelRotation(parent.transform.rotation);
    }

    public GameObject GetParent() {
        return parent;
    }
}
