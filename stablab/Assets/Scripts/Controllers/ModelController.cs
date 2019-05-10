using UnityEngine;

/*
 * ModelController has functions to Set/get pose of body.
 */

public class ModelController : MonoBehaviour
{
    public SkinnedMeshRenderer smr;
    public Transform skeleton = null;
    private const string BODYPART_TAG = "Body";
    public GameObject skeletonNonStatic; // Needed to get skeleton from editor
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

    public void GetClosestBone(Transform hit){
        Transform closestBone = null;
        Transform[] bones = skeleton.GetComponentsInChildren<Transform>();
        foreach (Transform bone in bones){
            if (Vector3.Distance(hit.position, closestBone.position) > 
                Vector3.Distance(hit.position, bone.position)){
                closestBone = bone;
            }
        }
        if(closestBone){
            // Set gizmo
        }
    }

    // Set the pose to the BodyPose input
    public static void SetBodyPose(BodyPose body)
    {/*
        if (body == null) return; // set to a standard pose later

        skeleton.position = body.GetPosition();
        skeleton.rotation = body.GetRotation();

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
        */
    }

    // Return current pose
    public static BodyPose GetBodyPose()
    {
        return new BodyPose(new GameObject());
    }
}
