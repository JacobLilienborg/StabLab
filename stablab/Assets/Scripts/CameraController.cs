using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This camera will behave like the unity editor camera
public class CameraController : MonoBehaviour
{

    // variables
    private bool ORotation = true;

    [SerializeField] private float mouseSensitivity = 1.0f;
    [SerializeField] private float zoomSensitivity = 20f;
    [SerializeField] private float rotationSensitivity = 5f;
    [SerializeField] private float speedH = 2.0f;
    [SerializeField] private float speedV = 2.0f;

    private Vector3 lastPosition;

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
        ORotation = Input.GetKeyDown("r") ? !ORotation : ORotation;
        if (ORotation)
        {
            RotateO();
        }
        else
        {
            Rotate();
        }
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
        // Secondary button pressed 
        if (Input.GetMouseButtonDown(1)) { lastPosition = Input.mousePosition; }

        // Secondary button held
        if (Input.GetMouseButton(1))
        {

            float rotationAmount = rotationSensitivity * Input.GetAxis("Mouse X");
            transform.RotateAround(Vector3.zero, Vector3.up, rotationAmount);
            lastPosition = Input.mousePosition;
            pitch = transform.eulerAngles.x;
            yaw = transform.eulerAngles.y;
        }
    }
}