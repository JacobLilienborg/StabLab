using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovementManager : MonoBehaviour
{
    bool rotate;
    float rotSpeed = 10;
    Transform currentPart,currentHover,lastHover;
    List<Collider> allColliders;
    bool pressed;
    Dictionary<string,string> meshParts = new Dictionary<string,string>(); 
    // Start is called before the first frame update
    void Start()
    {
        rotate = false;
        pressed = false;
        initDict(meshParts);
        //allColliders = getChildrenColliders(this.gameObject);
    }

    private void initDict(Dictionary<string, string> dict) {
        dict.Add("RightForeArm", "testMesh.005");
        dict.Add("RightArm", "testMesh.006");
        dict.Add("RightUpLeg", "testMesh.014");
        dict.Add("RightLeg", "testMesh.012");
        dict.Add("LeftUpLeg", "testMesh.015");
        dict.Add("LeftLeg", "testMesh.013");
    }

    private void MovePart(Transform curTransform)
    {
        if (!rotate)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            curTransform.RotateAround(Vector3.up, -rotX);
            curTransform.RotateAround(Vector3.right, rotY);
        }
        else
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            curTransform.RotateAround(transform.forward, rotX);
        }
    }

    private void MousePressed()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (currentPart == null)
        {
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Body")
            {
                currentPart = hit.transform;
            }
            else
            {
                return;
            }

        }
    }

    private void MouseHover() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform != currentHover) {
                lastHover = currentHover;
            }
            currentHover = hit.transform;
        }
        else {
            lastHover = currentHover;
            currentHover = null;
        }

        
    }

    private void PaintTransform(Transform transform, Color color) {
        string name = transform.name;
        string value;
        bool caught = meshParts.TryGetValue(name, out value);

        if (!caught) {
            return;
        }

        GameObject mesh = GameObject.Find(value);
        SkinnedMeshRenderer r = mesh.GetComponent<SkinnedMeshRenderer>();
        Material m = r.material;
        m.color = color;
        r.material = m;
    }

    // Update is called once per frame
    void Update()
    {
        MouseHover();
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotate = !rotate;
        }
        if (Input.GetMouseButtonDown(0)) {
            MousePressed();
            pressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
            if(currentPart != null)
            {
                PaintTransform(currentPart,Color.white);
            }   
            currentPart = null;
          
        }

        if (pressed) {
            if(currentPart != null)
            {
                PaintTransform(currentPart,Color.red);
                MovePart(currentPart);
                return;
            }
        }
        if (currentHover != null)
        {
            PaintTransform(currentHover,Color.red);
        }
        if(lastHover != null && !pressed) {
            PaintTransform(lastHover,Color.white);
            lastHover = null;
        }

    }
}
