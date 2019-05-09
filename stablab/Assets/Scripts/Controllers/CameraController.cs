using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This camera will behave like the unity editor camera
public class CameraController : MonoBehaviour
{

    // variables

    [SerializeField][Range(0.0f, 1.0f)] private float panSensitivity = 0.5f;
    [SerializeField][Range(0.0f, 1.0f)] private float zoomSensitivity = 0.5f;
    [SerializeField][Range(0.0f, 1.0f)] private float rotationSensitivity = 0.5f;

    private float panConstant = 0.1f;
    private float zoomConstant = 80f;
    private float rotationConstant = 10f;
    
    private Vector3 lastPosition;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float fov;

    float panMoveConstant = 5;
    float clickZoomConstant = 5;
    float clickRotationConstant = 10;

    // Start is called before the first frame update
    void Start()
    {
        fov = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        Pan();
        Rotate();
    }

    // Check if the middle button is pressed and starts paning the camera in a intuitive way
    void Pan()
    {
        // Middle button pressed 
        if (Input.GetMouseButtonDown(2)) {
            lastPosition = Input.mousePosition;
        }

        // Middle button held
        if (Input.GetMouseButton(2))
        {
            float panAmount = panSensitivity * panConstant * Mathf.Log(fov + 1);
            Vector3 delta = lastPosition - Input.mousePosition;
            transform.Translate(delta.x * panAmount, delta.y * panAmount, 0);
            lastPosition = Input.mousePosition;
        }
    }

    // Check if the scroll wheel is scrolled and starts zooming the camera in a intuitive way
    void Zoom()
    {
        fov = Camera.main.fieldOfView;
        if(Input.mouseScrollDelta.y == 0) { return; }
        float zoomAmount = zoomSensitivity * zoomConstant * Mathf.Log(fov + 1);

        if (Input.mouseScrollDelta.y < 0) { fov += zoomAmount * Time.deltaTime; }
        if (Input.mouseScrollDelta.y > 0) { fov -= zoomAmount * Time.deltaTime; }
        
        fov = Mathf.Clamp(fov, 0, 100);
        Camera.main.fieldOfView = fov;
    }

    // Check if the right button is pressed and starts rotate the camera around the center
    void Rotate()
    {
        // Secondary button pressed 
        if (Input.GetMouseButtonDown(1)) { lastPosition = Input.mousePosition; }

        // Secondary button held
        if (Input.GetMouseButton(1))
        {
            float rotationAmount = rotationSensitivity * rotationConstant * Mathf.Log(fov + 1);
            transform.RotateAround(Vector3.zero, Vector3.up, rotationAmount * Input.GetAxis("Mouse X"));
            lastPosition = Input.mousePosition;
            pitch = transform.eulerAngles.x;
            yaw = transform.eulerAngles.y;
        }
    }

    public void ClickPanHorizontal(bool directionRight) {
        if (directionRight) transform.Translate(-panMoveConstant, 0, 0);
        else transform.Translate(panMoveConstant, 0, 0);
    }

    public void ClickPanVertical(bool up) {
        if (up) transform.Translate(0, -panMoveConstant, 0);
        else transform.Translate(0, panMoveConstant, 0);
    }

    public void ClickZoom(bool zoomIn) {
        if (zoomIn) fov += clickZoomConstant;
        else fov -= clickZoomConstant;
    }

    public void ClickRotateHorizontal(bool rotateRight) {
        if (rotateRight) transform.RotateAround(Vector3.zero, Vector3.up, clickRotationConstant);
        else transform.RotateAround(Vector3.zero, Vector3.up, clickRotationConstant);
    }

    public void ClickRotateVertical(bool rotateUp)
    {
        if (rotateUp) transform.RotateAround(Vector3.zero, Vector3.right, clickRotationConstant);
        else transform.RotateAround(Vector3.zero, Vector3.right, clickRotationConstant);
    }
}