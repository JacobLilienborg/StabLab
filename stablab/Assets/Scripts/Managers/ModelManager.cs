﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class ModelManager : MonoBehaviour
{
    public static ModelManager instance;
    public enum Type { man, woman, child, none};
    private int modelHeight = 0;
    private int referenceHeightValue;
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
        Debug.Log(scene.name);
        if(scene.name == "InjuryMode")
        {
            Mesh mesh = new Mesh();
            activeModel.smr.BakeMesh(mesh);
            activeModel.meshCollider.sharedMesh = mesh;
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

        switch ((Type)type)
        {
            case Type.man:
                {
                    activeModel = Instantiate(man, transform);
                    referenceHeightValue = 180;
                    break;
                }
            case Type.woman:
                {
                    activeModel = Instantiate(woman, transform);
                    referenceHeightValue = 162;
                    break;
                }
            case Type.child:
                {
                    activeModel = Instantiate(child, transform);
                    referenceHeightValue = 108;
                    break;
                }
        }
    }

    public void adjustWeight(Slider slider)
    {
        if (activeModel == null) return;
        activeModel.weight = slider.value;
    }

    public void adjustMuscles(Slider slider)
    {
        if (activeModel == null) return;
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

    public void adjustHeight(InputField height)
    {
        try
        {
            modelHeight = Int32.Parse(height.text);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{height.text}'");
        }

    }

    public int GetHeight()
    {
        if (modelHeight == 0) return referenceHeightValue;
        return modelHeight;
    }

    public int GetStandardHeight()
    {
        return referenceHeightValue;
    }
}
