using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverBodyParts : MonoBehaviour
{
    public GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        Renderer[] rs = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            Material m = r.material;
            m.color = Color.red;
            r.material = m;
        }

    }

    private void OnMouseExit()

    {
        Renderer[] rs = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            Material m = r.material;
            m.color = Color.white;
            r.material = m;
        }

    }

    // Update is called once per frame
    void Update()
    {
      

    }

}
