using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private void Awake()
    {

        // If instance doesn't exist set it to this, else destroy this
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        DontDestroyOnLoad(this);
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
