﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public enum ModelType { man, woman, child, none};
public class ModelManager : MonoBehaviour
{
    public static ModelManager instance;
    public ModelController activeModel = null;
    [SerializeField] private ModelController man;
    [SerializeField] private ModelController woman;
    [SerializeField] private ModelController child;

    public UnityEvent modelEnabledEvent = new UnityEvent();
    public UnityEvent modelDisabledEvent = new UnityEvent();
    public UnityEvent heightChangedEvent = new UnityEvent();

    public ModelData modelData = null;
    private int activeModelType;


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

        if (SceneManager.GetActiveScene().name == "CharacterMode" && instance.activeModel)
        {
            Debug.Log("destroying");
            Destroy(instance.activeModel.gameObject);
            instance.activeModel = null;
        }
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
        if (activeModel == null) return;

        if (scene.name == "InjuryMode")
        {
            activeModel.BakeMesh();
        }
    }

    public void LoadModel(ModelData modelData)
    {
        SetActiveModel(modelData.type);
        activeModel.weight = modelData.weight;
        activeModel.muscles = modelData.muscles;
        activeModel.height = modelData.height;

        this.modelData = modelData;
        activeModel.BakeMesh();
    }

    // Sets the active model to either man, woman, child
    public void SetActiveModel(int type)
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
                    modelEnabledEvent.Invoke();
                    activeModel = Instantiate(man, transform);
                    break;
                }
            case ModelType.woman:
                {
                    modelEnabledEvent.Invoke();
                    activeModel = Instantiate(woman, transform);
                    break;
                }
            case ModelType.child:
                {
                    modelEnabledEvent.Invoke();
                    activeModel = Instantiate(child, transform);
                    break;
                }
            default:
                modelDisabledEvent.Invoke();
                break;
        }

        activeModelType = type;
    }

    public void AdjustWeight(Slider slider)
    {
        if (activeModel == null) return;
        activeModel.weight = slider.value;
    }

    public void AdjustMuscles(Slider slider)
    {
        if (activeModel == null) return;
        activeModel.muscles = slider.value;
    }
    public void AdjustHeight(InputField height)
    {
        if (activeModel == null)
        {
            return;
        }
        try
        {
            activeModel.height = System.Int32.Parse(height.text);
        }
        catch (System.FormatException)
        {
            activeModel.height = 0;
        }
        heightChangedEvent.Invoke();
    }

    public void CreateModelData()
    {
        modelData = new ModelData
        {
            type = activeModelType,
            height = activeModel.height,
            weight = activeModel.weight,
            muscles = activeModel.muscles,
            isModified = true
        };
    }
}
