using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
    public static ModelManager instance;

    public enum Type { man, woman, child, none};
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
                    break;
                }
            case Type.woman:
                {
                    activeModel = Instantiate(woman, transform);
                    break;
                }
            case Type.child:
                {
                    activeModel = Instantiate(child, transform);
                    break;
                }
        }
    }

    public void adjustHeight(Slider slider)
    {
        activeModel.height = slider.value;
    }
    
    public void adjustWeight(Slider slider)
    {
        activeModel.weight = slider.value;
    }

    public void adjustMuscles(Slider slider)
    {
        activeModel.muscles = slider.value;
    }
}
