﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnClickEvent : UnityEvent<InjuryController>
{
}
public class InjuryController : MonoBehaviour
{
    // For later implementations
    //OnClickEvent onClickWeapon = new OnClickEvent();
    OnClickEvent onClick = new OnClickEvent();
    public InjuryData injuryData;
    public GameObject markerObj;
    public GameObject weaponObj;
    private RuntimeGizmos.TransformGizmo gizmo;
    // Start is called before the first frame update
    void Start()
    {
        //onClick.AddListener(InjuryManager.instance.ActivateInjury);
        gizmo = Camera.main.GetComponent<RuntimeGizmos.TransformGizmo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray from mouseclick on screen
            RaycastHit hit;  //Where the ray hits (the injury position)

            if (Physics.Raycast(ray, out hit))
            {
                // The marker was hit
                if (hit.collider == markerObj || hit.collider == weaponObj)
                {
                    onClick.Invoke(this);
                }
            }
        }
    }

    public void PlaceInjury(Vector3 point, Transform bone)
    {
        // If no active injury is present, create one
        if (bone == null)
        {
            return;
        }
        if (markerObj == null && weaponObj == null)
        {
            markerObj = Instantiate((GameObject)Resources.Load(
                injuryData.markerData.prefabName),
                point,
                Quaternion.identity);
            markerObj.transform.SetParent(bone
            );

            weaponObj = Instantiate((GameObject)Resources.Load(
                injuryData.weaponData.prefabName),
                point,
                Quaternion.identity
            );
            weaponObj.transform.SetParent(bone);
        }
        else
        {
            markerObj.transform.position = point;
            markerObj.transform.parent = bone;

            weaponObj.transform.position = point;
            weaponObj.transform.parent = bone;
        }
        weaponObj.transform.rotation = Quaternion.FromToRotation(Vector3.left, Camera.main.transform.position - weaponObj.transform.position);
    }

    public void ToggleWeapon(bool active)
    {
        weaponObj.SetActive(active);
    }

    public void SetColor(Color color)
    {
        weaponObj.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    public void AddGizmo()
    {
        if(gizmo.isTransforming) return;
        gizmo = Camera.main.GetComponent<RuntimeGizmos.TransformGizmo>();
        gizmo.ClearTargets();
        gizmo.AddTarget(weaponObj.transform);
        gizmo.enabled = true;
    }

    public void RemoveGizmo()
    {
        gizmo.ClearTargets();
    }

    public void TransformActive(InjuryType type)
        {
            switch(type)
            {
                case InjuryType.Shot:
                    injuryData = new ShotInjuryData(injuryData);
                    break;
                case InjuryType.Crush:
                    injuryData = new CrushInjuryData(injuryData);
                    break;
                case InjuryType.Cut:
                    injuryData = new CutInjuryData(injuryData);
                    break;
                case InjuryType.Stab:
                    injuryData = new StabInjuryData(injuryData);
                    break;
                default:
                    injuryData = new UndefinedInjuryData(injuryData);
                    break;
            }
    }

    public void UpdateData()
    {
        Transform[] bones = ModelManager.instance.activeModel.skeleton.GetComponentsInChildren<Transform>();
        Array.ForEach(bones, bone => injuryData.poseData.Add(new TransformData(bone)));

        injuryData.markerData.transformData.position = markerObj.transform.position;
        injuryData.markerData.transformData.rotation = markerObj.transform.rotation;

        injuryData.weaponData.transformData.position = weaponObj.transform.position;
        injuryData.weaponData.transformData.rotation = weaponObj.transform.rotation;
        injuryData.weaponData.color = weaponObj.GetComponentInChildren<MeshRenderer>().material.color;
    }

    public void RevertData()
    {
        markerObj.transform.position = injuryData.markerData.transformData.position;
        markerObj.transform.rotation = injuryData.markerData.transformData.rotation;

        weaponObj.transform.position = injuryData.weaponData.transformData.position;
        weaponObj.transform.rotation = injuryData.weaponData.transformData.rotation;
        weaponObj.GetComponentInChildren<MeshRenderer>().material.color = injuryData.weaponData.color;
    }

    public Texture GetIcon()
    {
        return (Texture)Resources.Load(injuryData.markerData.iconName);
    }
}
