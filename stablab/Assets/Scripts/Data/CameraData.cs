using UnityEngine;
using System;


[Serializable]
public class CameraData : AData
{
    private float[] cameraData = new float[8];
    [NonSerialized]
    private Camera cam;

    public CameraData() : base("camera", "Data")
    {
        //new CameraData(new Camera());
    }

    public CameraData(Camera cam) : base("camera", "Data")
    {
    }

    public Vector3 GetPosition() 
    {
        return new Vector3(cameraData[0], cameraData[1], cameraData[2]);
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(cameraData[3], cameraData[4], cameraData[5], cameraData[6]);
    }

    public float GetFieldOfView()
    {
        return cameraData[7];
    }

    public override void Update()
    {
        cameraData[0] = Camera.main.transform.position.x;
        cameraData[1] = Camera.main.transform.position.y;
        cameraData[2] = Camera.main.transform.position.z;
        cameraData[3] = Camera.main.transform.rotation.x;
        cameraData[4] = Camera.main.transform.rotation.y;
        cameraData[5] = Camera.main.transform.rotation.z;
        cameraData[6] = Camera.main.transform.rotation.w;
        cameraData[7] = Camera.main.fieldOfView;
    }

    public override void Load()
    {
        Camera.main.transform.position = GetPosition();
        Camera.main.transform.rotation = GetRotation();
        Camera.main.fieldOfView = GetFieldOfView();
    }
}
