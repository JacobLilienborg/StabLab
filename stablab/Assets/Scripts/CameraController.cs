using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This camera will behave like the unity editor camera
public class CameraController : MonoBehaviour
{

    // variables
    public bool ORotation = true;

    public float mouseSensitivity = 1.0f;
    public float zoomSensitivity = 20f;
    public float rotationSensitivity = 5f;
    private Vector3 lastPosition;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float fov;

    // Start is called before the first frame update
    void Start()
    {
        fov = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
        Pan();
        Rotate();
        RotateO();
    }
    void Pan()
    {
        // Middle button pressed 
        if (Input.GetMouseButtonDown(2)) { lastPosition = Input.mousePosition; }

        // Middle button held
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = lastPosition - Input.mousePosition;
            transform.Translate(delta.x * mouseSensitivity * fov / 50, delta.y * mouseSensitivity * fov / 50, 0);
            lastPosition = Input.mousePosition;
        }
    }
    void Scroll()
    {
        if (Input.mouseScrollDelta.y < 0) { fov += zoomSensitivity * Time.deltaTime * 10f; }
        if (Input.mouseScrollDelta.y > 0) { fov -= zoomSensitivity * Time.deltaTime * 10f; }

        fov = Mathf.Clamp(fov, 0, 100);
        Camera.main.fieldOfView = fov;
    }
    void Rotate()
    {
        // Secondary button pressed 
        if (Input.GetMouseButtonDown(1)){ lastPosition = Input.mousePosition; }

        // Secondary button held
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            
            Vector3 delta = lastPosition - Input.mousePosition;
            transform.Translate(delta.x * mouseSensitivity * fov / 100, delta.y * mouseSensitivity * fov / 100, 0);
            lastPosition = Input.mousePosition;
        }
    }

    void RotateO()
    {
        if (!ORotation) { return; }

        // Primary button pressed 
        if (Input.GetMouseButtonDown(0)) { lastPosition = Input.mousePosition; }

        // Primary button held
        if (Input.GetMouseButton(0))
        {

            float rotationAmount = rotationSensitivity * Input.GetAxis("Mouse X");
            transform.RotateAround(Vector3.zero, Vector3.up, rotationAmount);
            lastPosition = Input.mousePosition;
            pitch = transform.eulerAngles.x;
            yaw = transform.eulerAngles.y;
        }
    }
}