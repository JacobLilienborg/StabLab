using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour
{
    [SerializeField] Color highlightColor;
    public Color standardColor;
    public Dictionary<string, string> meshParts = new Dictionary<string, string>();
    private Transform currentPart;

    private void Start()
    {
        InitDict(meshParts);
        getStandardColor();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) {
            //PaintParts();
        }
    }

    public void PaintParts(Transform transform, bool highlight)
    {
        string val;
        SkinnedMeshRenderer r;
        Material m;
        //THIS IS AN COMMENT
        string name = transform.gameObject.name;
        meshParts.TryGetValue(name, out val);
        GameObject mesh = GameObject.Find(val);

        if (highlight)
        {
            r = mesh.GetComponent<SkinnedMeshRenderer>();
            m = r.material;
            m.color = highlightColor;
            r.material = m;
        }
        else
        {
            r = mesh.GetComponent<SkinnedMeshRenderer>();
            m = r.material;
            m.color = standardColor;
            r.material = m;
        }
    }


    private void InitDict(Dictionary<string, string> dict)
    {
        dict.Add("RightForeArm", "testMesh.005");
        dict.Add("RightArm", "testMesh.006");
        dict.Add("RightUpLeg", "testMesh.014");
        dict.Add("RightLeg", "testMesh.012");
        dict.Add("LeftUpLeg", "testMesh.015");
        dict.Add("LeftLeg", "testMesh.013");
    }

    private void getStandardColor() {
        SkinnedMeshRenderer r;
        Material m;
        foreach (string name in meshParts.Keys)
        {
            string val;
            meshParts.TryGetValue(name, out val);
            GameObject mesh = GameObject.Find(val);
            r = mesh.GetComponent<SkinnedMeshRenderer>();
            m = r.material;
            standardColor = m.color;
        }
    }

}
