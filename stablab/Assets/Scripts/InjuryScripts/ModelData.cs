using System;
using UnityEngine;

[Serializable]
public class ModelData
{
    private float[] position = new float[3];
    private float[] rotation = new float[4];

    public ModelData(GameObject model) 
    {
        SetPosition(model.transform);
        SetRotation(model.transform);
    }

    private void SetPosition(Transform model)
    {
        position[0] = model.position.x;
        position[1] = model.position.y;
        position[2] = model.position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    private void SetRotation(Transform model)
    {
        rotation[0] = model.rotation.x;
        rotation[1] = model.rotation.y;
        rotation[2] = model.rotation.z;
        rotation[3] = model.rotation.w;
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }

}
