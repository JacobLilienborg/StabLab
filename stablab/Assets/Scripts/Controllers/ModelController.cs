using UnityEngine;

/*
 * ModelController has functions to Set/get pose of body.
 */

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

    public GameObject skeletonNonStatic; // Needed to get skeleton from editor
    public static GameObject skeleton;

    private void morph()
    {
        smr.SetBlendShapeWeight(3, Mathf.Min(muscles, weight));
        smr.SetBlendShapeWeight(muscles > weight ? 1 : 2, Mathf.Abs(muscles - weight));

    }

    // Set the pose to the BodyPose input
    public static void SetBodyPose(BodyPose body)
    {
        if (body == null) return; // set to a standard pose later

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

    // Return current pose
    public static BodyPose GetBodyPose()
    {
        return new BodyPose(skeleton);
    }
}
