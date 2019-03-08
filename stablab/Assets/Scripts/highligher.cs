using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highligher : MonoBehaviour
{
    List<SkinnedMeshRenderer> meshParts = new List<SkinnedMeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (SkinnedMeshRenderer child in GetComponentsInChildren<SkinnedMeshRenderer>()) {
            meshParts.Add(child);
        }
    }

    private void OnMouseEnter()
    {
        foreach (SkinnedMeshRenderer mesh in meshParts) {
            Material m = mesh.material;
            m.color = Color.red;
            mesh.material = m;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
