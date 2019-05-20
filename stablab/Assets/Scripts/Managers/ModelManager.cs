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
        if(activeModel == null) return;
        try 
        {
            activeModel.height = System.Int32.Parse(height.text);
        }
        catch (System.FormatException)
        {
            Debug.Log($"Unable to parse '{height.text}'");
        }
    }
}
