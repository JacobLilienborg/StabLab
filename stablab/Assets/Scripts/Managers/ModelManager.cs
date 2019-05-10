using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModelManager : MonoBehaviour
{
    public static ModelManager instance;
    public ViewManager viewManager;

    public enum Type { man, woman, child, none};
    public ModelController activeModel = null;
    [SerializeField] private ModelController man;
    [SerializeField] private ModelController woman;
    [SerializeField] private ModelController child;

    void Start()
    {
        viewManager.onSceneChange.AddListener(Finished);
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

    public void AddOnClickPointListener(UnityAction<Vector3> action){
        activeModel.onClickPoint.AddListener(action);
    }
    public void AddOnClickBoneListener(UnityAction<Transform> action){
        activeModel.onClickBone.AddListener(action);
    }

    private void Finished(Scenes scene)
    {
        if(scene == Scenes.editing)
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
