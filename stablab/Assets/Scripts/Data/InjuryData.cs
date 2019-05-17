using System;
using System.Collections.Generic;

[Serializable]
public class CameraData
{
    public TransformData transformData;
    public float fieldOfView;
}

[Serializable]
public class WeaponData
{
    public TransformData transformData;
    private float[] _color = new float[4];
    public string resourcePath;
    public string prefabName;
    public UnityEngine.Vector4 color
    {
        get
        {
            return new UnityEngine.Vector4(_color[0], _color[1], _color[2], _color[3]);
        }
        set
        {
            _color = new float[4] { value[0], value[1], value[2], value[3] };
        }
    }

}

[Serializable]
public class MarkerData
{
    public TransformData transformData;
    public string prefabName;
    public string iconName;
}


[Serializable]
public abstract class InjuryData
{
    public string name { get; set; }
    public Guid id { get; }
    public string infoText { get; set; }
    public string boneName { get; set; }
    public List<byte[]> images = new List<byte[]>();
    public List<TransformData> poseData = new List<TransformData>();
    public CameraData cameraData { get; set; }
    public MarkerData markerData;
    public WeaponData weaponData;

    protected InjuryData(Guid id)
    {
        this.id = id;
    }

    protected InjuryData(InjuryData injuryData)
    {
        name = injuryData.name;
        id = injuryData.id;
        infoText = injuryData.infoText;
        boneName = injuryData.boneName;
        images = injuryData.images;
        poseData = injuryData.poseData;
        cameraData = injuryData.cameraData;
    }
}
