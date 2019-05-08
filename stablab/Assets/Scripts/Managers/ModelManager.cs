using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
    [SerializeField] public enum Type { man, woman, child };
    public ModelController activeModel = null;
    [SerializeField] private ModelController man;
    [SerializeField] private ModelController woman;
    [SerializeField] private ModelController child;

    // Sets the active model to either man, woman, child
    public void setActiveModel(int type)
    {
        switch ((Type)type)
        {
            case Type.man:
                {
                    activeModel = Instantiate(man);
                    break;
                }
            case Type.woman:
                {
                    activeModel = Instantiate(woman);
                    break;
                }
            case Type.child:
                {
                    activeModel = Instantiate(child);
                    break;
                }
            default:
                {
                    Destroy(activeModel);
                    activeModel = null;
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
