using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightElbow : MonoBehaviour
{
    Vector3 pos;
    Transform b;

    // Start is called before the first frame update
    void Start()
    {
        b = this.transform;
        pos = b.eulerAngles;
        pos.x += 50;
        pos.y -= 50;
        

    }

    void OnMouseDown() {
        pos.x += 50;
        pos.y -= 50;

    }


    // Update is called once per frame
    void Update()
    {
       b.eulerAngles = pos;


    }

    public Vector3 getPos() {
        return pos;
    }

    public void setPos(Vector3 pos) {
        this.pos = pos;
    }

    
     

}
