﻿using System.Collections;
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

    public void SetBodyPose(BodyPose body)
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

    public BodyPose GetBodyPose()
    {
        return new BodyPose(skeleton);
    }
}
