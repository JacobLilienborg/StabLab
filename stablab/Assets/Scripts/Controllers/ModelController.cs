using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public SkinnedMeshRenderer smr;
    public float height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
        }
    }
    public float weight
    {
        get
        {
            return weight;
        }
        set
        {
            weight = value;
        }
    }
    public float muscles
    {
        get
        {
            return muscles;
        }
        set
        {
            muscles = value;
        }
    }

    private const string BODYPART_TAG = "Body";

    public GameObject skeleton;

    public void SetBodyPose(BodyPose body)
    {
        skeleton.transform.position = body.GetPosition();
        skeleton.transform.rotation = body.GetRotation();

        Transform[] children = skeleton.GetComponentsInChildren<Transform>();
        int bodyIndex = 0;
        foreach (Transform child in children)
        {
            if (child.tag == BODYPART_TAG)
            {
                child.position = body.bodyParts[bodyIndex].GetPosition();
                child.rotation = body.bodyParts[bodyIndex].GetRotation();
                bodyIndex++;
            }
        }

    }

    public BodyPose GetBodyPose()
    {
        return new BodyPose(skeleton);
    }
}
