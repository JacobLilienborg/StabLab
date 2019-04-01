using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private const string BODYPART_TAG = "Body";

    public GameObject skeleton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBodyPose(BodyData data)
    {
        skeleton.transform.position = data.GetPosition();
        skeleton.transform.rotation = data.GetRotation();

        Transform[] children = skeleton.GetComponentsInChildren<Transform>();
        int bodyIndex = 0;
        foreach (Transform child in children)
        {
            if (child.tag == BODYPART_TAG)
            {
                child.position = data.bodyParts[bodyIndex].GetPosition();
                child.rotation = data.bodyParts[bodyIndex].GetRotation();
                bodyIndex++;
            }
        }

    }

    public BodyData GetBodyPose()
    {
        return new BodyData(skeleton);
    }
}
