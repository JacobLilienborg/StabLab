using UnityEngine;
using System;


[Serializable]
public class CameraSettings
{
    private float[] cameraData = new float[8];

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

    public void SetCamera(Camera cam)
    {
        cameraData[0] = cam.transform.position.x;
        cameraData[1] = cam.transform.position.y;
        cameraData[2] = cam.transform.position.z;
        cameraData[3] = cam.transform.rotation.x;
        cameraData[4] = cam.transform.rotation.y;
        cameraData[5] = cam.transform.rotation.z;
        cameraData[6] = cam.transform.rotation.w;
        cameraData[7] = cam.fieldOfView;
    }
}
