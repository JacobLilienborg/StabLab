using System;
using UnityEngine;
using System.Collections.Generic;



[Serializable]
public class BodyPart
{
    private float[] position = new float[3];
    private float[] rotation = new float[4];

    public BodyPart(Transform part)
    {
        SetPosition(part);
        SetRotation(part);
    }

    private void SetPosition(Transform part)
    {
        position[0] = part.position.x;
        position[1] = part.position.y;
        position[2] = part.position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    private void SetRotation(Transform part)
    {
        rotation[0] = part.rotation.x;
        rotation[1] = part.rotation.y;
        rotation[2] = part.rotation.z;
        rotation[3] = part.rotation.w;
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }

}

[Serializable]
public class BodyData
{
    private float[] position = new float[3];
    private float[] rotation = new float[4];

    public List<BodyPart> bodyParts = new List<BodyPart>();

    public BodyData(GameObject body) 
    {
        SetPosition(body.transform);
        SetRotation(body.transform);
        SetBodyParts(body);
    }

    private void SetPosition(Transform body)
    {
        position[0] = body.position.x;
        position[1] = body.position.y;
        position[2] = body.position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    private void SetRotation(Transform body)
    {
        rotation[0] = body.rotation.x;
        rotation[1] = body.rotation.y;
        rotation[2] = body.rotation.z;
        rotation[3] = body.rotation.w;
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }

    private void SetBodyParts(GameObject body) 
    {
        Transform[] children = body.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].tag == "Body")
            {
                bodyParts.Add(new BodyPart(children[i]));
            }
        }
    }

}
