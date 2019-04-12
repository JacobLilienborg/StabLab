using UnityEngine;

/*
 * ModelController has functions to Set/get pose of body.
 */

public class ModelController : MonoBehaviour
{
    private const string BODYPART_TAG = "Body";

    public GameObject skeletonNonStatic; // Needed to get skeleton from editor
    public static GameObject skeleton;

    // Start is called before the first frame update
    void Awake()
    {
        skeleton = skeletonNonStatic;
    }

    // Update is called once per frame
    void Update()
    {
        
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
