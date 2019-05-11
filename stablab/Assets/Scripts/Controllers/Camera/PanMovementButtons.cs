using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanMovementButtons : MonoBehaviour,IPointerClickHandler
{
    private Transform target;
    public int directionNumber;
    private Vector3 direction;
    private int speed = 1;
    private bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
        switch(directionNumber){
            case 1:
                direction = Vector3.right;
                break;
            case 2:
                direction = Vector3.left;
                break;
            case 3:
                direction = Vector3.up;
                break;
            case 4:
                direction = Vector3.down;
                break;
            default:
                direction = Vector3.zero;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(move) target.Translate(direction * Time.deltaTime * speed);
    }

    public void OnPointerClick(PointerEventData e)
    {
        move = true;
    }
}
