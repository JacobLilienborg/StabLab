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
    private float[] serializedPos = new float[3];
    private float[] serializedRot = new float[4];
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
        Position = markerObj.transform.position;
        Rotation = markerObj.transform.rotation;
        BodyPartParent = markerObj.transform.parent.name;
        //Type = markerObj.GetComponent<MarkerHandler>().type;
    }

    public Vector3 Position
    {
        protected set
        {
            serializedPos[0] = value.x;
            serializedPos[1] = value.y;
            serializedPos[2] = value.z;
        }

        get
        {
            return new Vector3(serializedPos[0], serializedPos[1], serializedPos[2]);
        }
    }

    public Quaternion Rotation
    {
        protected set
        {
            serializedRot[0] = value.x;
            serializedRot[1] = value.y;
            serializedRot[2] = value.z;
            serializedRot[3] = value.w;
        }

        get
        {
            return new Quaternion(serializedRot[0], serializedRot[1], 
                                  serializedRot[2], serializedRot[3]);
        }
    }

    public void InsertModel() {
        parent.SetActive(true);

        GameObject positioningObject = new GameObject("emptyPositioningObject");
        positioningObject.transform.position = new Vector3(serializedPos[0],serializedPos[1],serializedPos[2]);
        positioningObject.transform.parent = GameObject.Find(BodyPartParent).transform;
        parent.transform.parent = positioningObject.transform;
        UpdateModel();
        //model.transform.rotation = new Quaternion(30, 30, 30, 30);
    }

    public void RemoveModel() {
        GameObject.Destroy(parent);
    }

    public void UpdateModel() {
        parent.transform.position = new Vector3(serializedPos[0], serializedPos[1], serializedPos[2]);
        parent.transform.rotation = new Quaternion(serializedRot[0], serializedRot[1], serializedRot[2],serializedRot[3]);

    }

    public void RemoveMarker() {
        serializedPos = null;
        serializedRot = null;
        RemoveModel();
    }

    public void SetRotation(Quaternion rotation) {
        Rotation = rotation;
        UpdateModel();
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
        UpdateModel();
    }

    public void SetParent(GameObject parent) {
        this.parent = parent;
        Position = parent.transform.position;
        Rotation = parent.transform.rotation;

    }

    public GameObject GetParent() {
        return parent;
    }
}
