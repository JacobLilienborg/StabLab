using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyPositioning : MonoBehaviour
{
    
    private Camera cam;
    private static int MAXDIFF = 30;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseEnter() {
        GameObject.Find("forearm.R").transform.position = getMousePos();
        /*
        Vector3 elbowPos = GameObject.Find("forearm.R").transform.position;
        Debug.Log(elbowPos);

        if (isInRange(getMousePos(), elbowPos)) {
            GameObject.Find("forearm.R").transform.position = getMousePos();
        }*/
    }
    private Vector3 getMousePos () {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos;

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        //Debug.Log(mousePos);


        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        return point;
    }

    private bool isInRange(Vector3 point, Vector3 target) {
        if (point.x + MAXDIFF < target.x &&
            point.x - MAXDIFF > target.x &&
            point.y + MAXDIFF < target.y &&
            point.y - MAXDIFF > target.y &&
            point.z + MAXDIFF < target.z &&
            point.z - MAXDIFF > target.z)
            return true;
  
        return false;
    }


}
