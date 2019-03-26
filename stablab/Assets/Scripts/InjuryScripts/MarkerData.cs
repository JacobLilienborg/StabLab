using System;
using UnityEngine;

[Serializable]
public class MarkerData
{
    [NonSerialized]
    private Vector3 position;

    public InjuryType Type { get; set; }
    public ModelData ModelPose { get; set; }
    private float[] serializedPos = new float[3];

    public Vector3 Position
    {   
        set
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
}
