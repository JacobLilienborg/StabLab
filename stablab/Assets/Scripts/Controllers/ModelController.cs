using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public SkinnedMeshRenderer smr;
    private float _height = 0;
    public float height
    {
        get { return _height; }
        set
        {
            _height = value;
            smr.SetBlendShapeWeight(0, _height);
        }
    }
    private float _muscles = 0;
    public float muscles
    {
        get { return _muscles; }
        set
        {
            _muscles = value;
            morph();
        }
    }
    private float _weight = 0;
    public float weight
    {
        get { return _weight; }
        set
        {
            _weight = value;
            morph();
        }
    }

    private const string BODYPART_TAG = "Body";

    public GameObject skeleton;

    private void morph()
    {
        smr.SetBlendShapeWeight(3, Mathf.Min(muscles, weight));
        smr.SetBlendShapeWeight(muscles > weight ? 1 : 2, Mathf.Abs(muscles - weight));
        
    }

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
