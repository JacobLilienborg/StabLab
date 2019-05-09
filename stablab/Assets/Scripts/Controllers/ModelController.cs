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
            //_height = value;
            //smr.SetBlendShapeWeight(0, _height);
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
        /*
         * 0:Buff
         * 1.Overweight
         * 2.Shredded
         * 3.Skinny
         * 4.WeightPos
         * 5.WeightNeg
         * 6.MusclePos
         * 7.MuscleNeg  
         */
        if (muscles > 0 && weight > 0)
        {
            smr.SetBlendShapeWeight(0, Mathf.Min(muscles, weight));
            smr.SetBlendShapeWeight(muscles > weight ? 6 : 4, Mathf.Abs(muscles - weight));
        }

        else if (muscles > 0 && weight < 0)
        {
            smr.SetBlendShapeWeight(2, Mathf.Min(muscles, Mathf.Abs(weight)));
            smr.SetBlendShapeWeight(muscles > Mathf.Abs(weight) ? 6 : 5, Mathf.Abs(muscles - Mathf.Abs(weight)));
        }
        else if (muscles < 0 && weight > 0)
        {
            smr.SetBlendShapeWeight(1, Mathf.Min(Mathf.Abs(muscles), weight));
            smr.SetBlendShapeWeight(Mathf.Abs(muscles) > weight ? 7 : 4, Mathf.Abs(Mathf.Abs(muscles) - weight));
        }
        else if (muscles < 0 && weight < 0)
        {
            smr.SetBlendShapeWeight(3, Mathf.Min(Mathf.Abs(muscles), Mathf.Abs(weight)));
            smr.SetBlendShapeWeight(Mathf.Abs(muscles) > Mathf.Abs(weight) ? 7 : 5, Mathf.Abs(Mathf.Abs(muscles) - Mathf.Abs(weight)));
        }
        else
        {
            smr.SetBlendShapeWeight(muscles > 0 ? 6 : 7, Mathf.Abs(muscles));
            smr.SetBlendShapeWeight(weight > 0 ? 4 : 5, Mathf.Abs(weight));
        }




        
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
