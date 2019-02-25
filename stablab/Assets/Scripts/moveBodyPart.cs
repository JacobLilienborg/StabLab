using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBodyPart : MonoBehaviour
{
    bool rotate;
    float rotSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rotate = false;
    }

    void OnMouseDrag()
    {
        if (!rotate)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
        else {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(transform.up, rotX);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            rotate = !rotate;
        }
        
    }
    
     

}
