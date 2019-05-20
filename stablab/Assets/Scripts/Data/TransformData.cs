using UnityEngine;

public class TransformData 
{
    private float[] _position = new float[3];
    private float[] _rotation = new float[4];
    public TransformData(){}
    public TransformData(Transform data)
    {
        position = data.position;
        rotation = data.rotation;
    }
    public Vector3 position
    {   
        set
        {
            _position[0] = value.x;
            _position[1] = value.y;
            _position[2] = value.z;
        }

        get
        {
            return new Vector3(_position[0], _position[1], _position[2]);
        }
    }

    public Quaternion rotation
    {   
        set
        {
            _rotation[0] = value.x;
            _rotation[1] = value.y;
            _rotation[2] = value.z;
            _rotation[3] = value.w;
        }

        get
        {
            return new Quaternion(_rotation[0], _rotation[1],
                                  _rotation[2], _rotation[3]);
        }
    }
}
