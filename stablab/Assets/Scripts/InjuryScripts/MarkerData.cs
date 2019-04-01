using System;
using UnityEngine;

[Serializable]
public class MarkerData
{
    //[NonSerialized]
    //private Vector3 position;

    public InjuryType Type { get; protected set; }
    public string BodyPartParent { get; protected set; }
    private float[] serializedPos = new float[3];
    private float[] serializedRot = new float[4];

    public MarkerData(GameObject markerObj) 
    {
        MarkerUpdate(markerObj);
    }

    public void MarkerUpdate(GameObject markerObj) 
    {
        Position = markerObj.transform.position;
        Rotation = markerObj.transform.rotation;
        BodyPartParent = markerObj.transform.parent.name;
        Type = markerObj.GetComponent<MarkerHandler>().type;

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
}
