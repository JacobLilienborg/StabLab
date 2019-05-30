using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


enum MOVEMENTTYPE {
    PAN,
    ROTATE,
    ZOOM,
    RESET
};

public class MovementButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static bool  invertedControls = false;

    private Transform    target;
    public  int          directionNumber;
    public  int          typeNumber;
    private Vector3      direction;
    private int          speed          = 5,
                         rotationSpeed  = 60;
    private bool         move           = false;
    private MOVEMENTTYPE MoveType       = MOVEMENTTYPE.PAN;
    private Vector3      rotationCenter = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
        switch (typeNumber) {
            case 0: //pan
                MoveType = MOVEMENTTYPE.PAN;
                speed    = 2;
                switch (directionNumber) {
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
                break;
            case 1: //rotation
                MoveType = MOVEMENTTYPE.ROTATE;
                //speed    = 20;
                switch (directionNumber)
                {
                    case 1:
                        direction = Vector3.down;
                        break;
                    case 2:
                        direction = Vector3.up;
                        break;
                    case 3:
                        direction = Vector3.right;
                        break;
                    case 4:
                        direction = Vector3.left;
                        break;
                    default:
                        direction = Vector3.zero;
                        break;
                }
                break;
            case 2: // zoom
                MoveType = MOVEMENTTYPE.ZOOM;
                speed    = 20;
                break;
            case 3:
                MoveType = MOVEMENTTYPE.RESET;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 modelCenter;
        // Without a model (or an instance), we have nothing to rotate around.
        if (ModelManager.instance != null)
        { 
            if (ModelManager.instance.activeModel == null)
                return;
        }
        else
            return;

        if (move)
        {
            if ((MoveType == MOVEMENTTYPE.PAN))
            {   
                if(!invertedControls)
                    target.Translate(direction * Time.deltaTime * speed * Camera.main.gameObject.GetComponent<CameraController>().fov / 10);
                else
                    target.Translate(direction * -1 * Time.deltaTime * speed * Camera.main.gameObject.GetComponent<CameraController>().fov / 10);
            }
            else if (MoveType == MOVEMENTTYPE.ROTATE)
            {
                if (direction == Vector3.up || direction == Vector3.down)
                {
                    if(!invertedControls)
                       target.RotateAround(Vector3.zero, direction, Time.deltaTime * rotationSpeed);
                    else
                       target.RotateAround(Vector3.zero, direction * -1, Time.deltaTime * rotationSpeed);
                }
                else
                {
                    //We change the rotationCenter to be the approximate middle of the model.
                    //I do not know why this value is a good approximation.
                    rotationCenter = new Vector3(
                        0,
                        ModelManager.instance.activeModel.meshCollider.bounds.center.y * 5 / Camera.main.scaledPixelHeight
                    );
                    if (direction == Vector3.right ^ invertedControls)
                        target.RotateAround(rotationCenter, target.right, Time.deltaTime * rotationSpeed);
                    else
                        target.RotateAround(rotationCenter, target.right * -1, Time.deltaTime * rotationSpeed);
                }
            }
            else if (MoveType == MOVEMENTTYPE.ZOOM)
            {
                float fov;
                fov = Camera.main.fieldOfView;

                fov -= directionNumber * Time.deltaTime * speed;
                fov  = Mathf.Clamp(fov, 0, 100);
                Camera.main.fieldOfView = fov;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !ArrowKeysToggler.DeactivateArrowKeys){
            ResetView();
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (MoveType == MOVEMENTTYPE.RESET) ResetView();
        move = true;
    }

    public void OnPointerUp(PointerEventData e) {
        move = false;
    }

    private void ResetView() {
        target.position = Camera.main.gameObject.GetComponent<CameraController>().stdPos;
        target.rotation = Camera.main.gameObject.GetComponent<CameraController>().stdRot;
        Camera.main.fieldOfView = Camera.main.gameObject.GetComponent<CameraController>().stdFov;
    }
    
    public static void InvertedControls(bool isReversed)
    {
        invertedControls = isReversed;
    }

}

