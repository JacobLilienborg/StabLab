﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnClickEvent : UnityEvent<Injury>
{
}
public class InjuryController : MonoBehaviour
{

    OnClickEvent onClickMarker = new OnClickEvent();
    OnClickEvent onClickWeapon = new OnClickEvent();
    OnClickEvent onClick = new OnClickEvent();
    public Injury injury;
    public Collider markerCollider;
    public Collider weaponCollider;
    // Start is called before the first frame update
    void Start()
    {
        onClick.AddListener(InjuryManager.instance.ActivateInjury);
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
                if (hit.collider == markerCollider)
                {
                    onClickMarker.Invoke(injury);
                    onClick.Invoke(injury);
                }
                else if (hit.collider == weaponCollider)
                {
                    onClickMarker.Invoke(injury);
                    onClick.Invoke(injury);
                }
            }
        }
    }
}
