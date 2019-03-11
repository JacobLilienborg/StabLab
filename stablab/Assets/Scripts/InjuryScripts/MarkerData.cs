using System;
using UnityEngine;

[Serializable]
public class MarkerData
{
    [SerializeField]
    private InjuryType type;

    private float[] position = new float[3];
    public ModelData modelPose;


    public MarkerData(GameObject marker, ModelData pose) 
    {
        SetPosition(marker.transform);
        type = marker.GetComponent<MarkerHandler>().type;
        SetModelPos(pose);
    }

    private void SetPosition(Transform marker) 
    {
        position[0] = marker.position.x;
        position[1] = marker.position.y;
        position[2] = marker.position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    public InjuryType GetType()
    {
        return type;
    }

    public void SetModelPos(ModelData pose)
    {
        modelPose = pose;
    }
}
