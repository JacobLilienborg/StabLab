﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum ModelType { man, woman, child, none};
public class ModelManager : MonoBehaviour
{
    public static ModelManager instance;
    public ModelController activeModel = null;
    [SerializeField] private ModelController man;
    [SerializeField] private ModelController woman;
    [SerializeField] private ModelController child;

    void Start()
    {
        SceneManager.sceneLoaded += Finished;
    }
    private void Awake()
    {
        // If instance doesn't exist set it to this, else destroy this
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void AddOnClickListener(UnityAction<Vector3, Transform> action)
    {
        activeModel.onClick.AddListener(action);
    }

    public void RemoveOnClickListener(UnityAction<Vector3, Transform> action)
    {
        activeModel.onClick.RemoveListener(action);
    }

    private void Finished(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "InjuryMode")
        {
            Debug.Log(scene.name);
            activeModel.BakeMesh();
        }
    }

    // Sets the active model to either man, woman, child
    public void setActiveModel(int type)
    {

        if (activeModel)
        {
            Destroy(activeModel.gameObject);
            activeModel = null;
        }

        switch ((ModelType)type)
        {
            case ModelType.man:
                {
                    activeModel = Instantiate(man, transform);
                    break;
                }
            case ModelType.woman:
                {
                    activeModel = Instantiate(woman, transform);
                    break;
                }
            case ModelType.child:
                {
                    activeModel = Instantiate(child, transform);
                    break;
                }
        }
    }

    public void adjustWeight(Slider slider)
    {
        activeModel.weight = slider.value;
    }

    public void adjustMuscles(Slider slider)
    {
        activeModel.muscles = slider.value;
    }


    // Set the pose to the BodyPose input
    public void SetBodyPose(BodyPose body)
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
    public BodyPose GetBodyPose()
    {
        return new BodyPose(new GameObject());
    }
}
