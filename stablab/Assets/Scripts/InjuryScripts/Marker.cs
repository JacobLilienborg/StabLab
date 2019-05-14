using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/*
 * Serializable class to save marker data such as position, rotation and which body part it is attached to.
 */

[Serializable]
public class Marker
{
    [NonSerialized]
    private GameObject weaponModel;

    public bool activeInPresentation = false;
    public string BodyPartParent { get; protected set; }
    private float[] serializedPosMarker = new float[3];
    private float[] serializedRotMarker = new float[4];
    private float[] serializedPosModel = new float[3];
    private float[] serializedRotModel = new float[4];

    public int modelColorIndex = -1;

    public Marker(GameObject markerObj) 
    {
        MarkerDataUpdate(markerObj);
    }

    // Copy data from input object
    public void MarkerDataUpdate(GameObject markerObj) 
    {
        MarkerPosition = markerObj.transform.position;
        MarkerRotation = markerObj.transform.rotation;
        BodyPartParent = markerObj.transform.parent.name;
    }

    // Copy data from input object
    public void ModelDataUpdate()
    {
        if(weaponModel != null) 
        {
            ModelPosition = weaponModel.transform.position;
            ModelRotation = weaponModel.transform.rotation;
        }
    }

    public Vector3 MarkerPosition
    {
        protected set
        {
            serializedPosMarker[0] = value.x;
            serializedPosMarker[1] = value.y;
            serializedPosMarker[2] = value.z;
        }

        get
        {
            return new Vector3(serializedPosMarker[0], serializedPosMarker[1], serializedPosMarker[2]);
        }
    }

    public Quaternion MarkerRotation
    {
        protected set
        {
            serializedRotMarker[0] = value.x;
            serializedRotMarker[1] = value.y;
            serializedRotMarker[2] = value.z;
            serializedRotMarker[3] = value.w;
        }

        get
        {
            return new Quaternion(serializedRotMarker[0], serializedRotMarker[1], 
                                  serializedRotMarker[2], serializedRotMarker[3]);
        }
    }

    public Vector3 ModelPosition
    {
        set
        {
            serializedPosModel[0] = value.x;
            serializedPosModel[1] = value.y;
            serializedPosModel[2] = value.z;
        }

        get
        {
            return new Vector3(serializedPosModel[0], serializedPosModel[1], serializedPosModel[2]);
        }
    }

    public Quaternion ModelRotation
    {
        set
        {
            serializedRotModel[0] = value.x;
            serializedRotModel[1] = value.y;
            serializedRotModel[2] = value.z;
            serializedRotModel[3] = value.w;
        }

        get
        {
            return new Quaternion(serializedRotModel[0], serializedRotModel[1],
                                  serializedRotModel[2], serializedRotModel[3]);
        }
    }

    public void RemoveModel() {
        serializedPosModel = null;
        serializedPosModel = null;
        UnityEngine.Object.Destroy(weaponModel);
    }

    public void UpdateModel() {
        weaponModel.transform.position = ModelPosition;
        weaponModel.transform.rotation = ModelRotation;

    }

    public void RemoveMarker() {
        serializedPosMarker = null;
        serializedRotMarker = null;
        RemoveModel();
    }

    public void SetModelRotation(Quaternion rotation) {
        ModelRotation = rotation;
        UpdateModel();
    }

    public void SetModelPosition(Vector3 position)
    {
        ModelPosition = position;
        UpdateModel();
    }

    public void SetWeaponModel(GameObject model) {
        if (weaponModel != null && model != weaponModel)
        {
            UnityEngine.Object.Destroy(weaponModel);
        }
        weaponModel = model;
        model.SetActive(activeInPresentation);
        ModelDataUpdate();
    }

    public GameObject GetWeaponModel() {
        return weaponModel;
    }
}
