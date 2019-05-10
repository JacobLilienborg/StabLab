using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class OnClickPoint : UnityEvent<Vector3>{}
[System.Serializable] public class OnClickBone : UnityEvent<Transform>{}


/*
 * ModelController has functions to Set/get pose of body.
 */

public class ModelController : MonoBehaviour
{
    public OnClickPoint onClickPoint = new OnClickPoint();
    public OnClickBone onClickBone = new OnClickBone();
    public SkinnedMeshRenderer smr = null;
    public MeshCollider meshCollider = null;
    private Mesh mesh = null; 
    public Transform skeleton = null;
    private float _muscles = 0;
    public float muscles
    {
        get { return _muscles; }
        set
        {
            _muscles = value;
            Morph();
        }
    }
    private float _weight = 0;
    public float weight
    {
        get { return _weight; }
        set
        {
            _weight = value;
            Morph();
        }
    }

    void Update(){
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray from mouseclick on screen
            RaycastHit hit;  //Where the ray hits (the injury position)

            if (Physics.Raycast(ray, out hit) && (hit.collider == meshCollider)){
                onClickPoint.Invoke(hit.point);
                onClickBone.Invoke(GetClosestBone(hit.transform));
            }
        }
    }

    // There is a bug, if the sliders are changed to fast the blend shapes will fuck up
    private void Morph()
    {
        /*
         * 0:Buff
         * 1.Overweight
         * 2.Shredded
         * 3.Skinny
         * 4.WeightPos
         * 5.WeightNeg
         * 6.MusclePos
         * 7.MuscleNeg
         */

        if (muscles > 0 && weight > 0)
        {
            
            smr.SetBlendShapeWeight(0, Mathf.Min(muscles, weight));
            smr.SetBlendShapeWeight(muscles > weight ? 6 : 4, Mathf.Abs(muscles - weight));
        }

        else if (muscles > 0 && weight < 0)
        {
            smr.SetBlendShapeWeight(2, Mathf.Min(muscles, Mathf.Abs(weight)));
            smr.SetBlendShapeWeight(muscles > Mathf.Abs(weight) ? 6 : 5, Mathf.Abs(muscles - Mathf.Abs(weight)));
        }
        else if (muscles < 0 && weight > 0)
        {
            smr.SetBlendShapeWeight(1, Mathf.Min(Mathf.Abs(muscles), weight));
            smr.SetBlendShapeWeight(Mathf.Abs(muscles) > weight ? 7 : 4, Mathf.Abs(Mathf.Abs(muscles) - weight));
        }
        else if (muscles < 0 && weight < 0)
        {
            smr.SetBlendShapeWeight(3, Mathf.Min(Mathf.Abs(muscles), Mathf.Abs(weight)));
            smr.SetBlendShapeWeight(Mathf.Abs(muscles) > Mathf.Abs(weight) ? 7 : 5, Mathf.Abs(Mathf.Abs(muscles) - Mathf.Abs(weight)));
        }
        else
        {
            smr.SetBlendShapeWeight(muscles > 0 ? 6 : 7, Mathf.Abs(muscles));
            smr.SetBlendShapeWeight(weight > 0 ? 4 : 5, Mathf.Abs(weight));
        }
        
    }
    public void BakeMesh(){
        mesh = new Mesh();
        smr.BakeMesh(mesh);
        meshCollider.sharedMesh = mesh;
    }



    public Transform GetClosestBone(Transform hit){
        Transform closestBone = null;
        float closestDistance = Mathf.Infinity;
        Transform[] bones = skeleton.GetComponentsInChildren<Transform>();
        foreach (Transform bone in bones){
            if (closestDistance > Vector3.Distance(hit.position, bone.position)){
                closestBone = bone;
                closestDistance = Vector3.Distance(hit.position, bone.position);
            }
        }
        return closestBone;
    }
}
