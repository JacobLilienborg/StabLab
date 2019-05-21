using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeightTracker : MonoBehaviour
{
    private int modelHeight;
    private string prefabPath = "Text";
    private GameObject textObj;
    public GameObject baseObject;
    private Vector3 basePos;
    private Text text;
    private Vector3 textOffset = new Vector3(10, 10, 0);
    private double scalingCoeff = 11.5;
    bool trackingOn = false;
    bool trackingActivated = true;
    // Start is called before the first frame update

    private void Start()
    {
        Debug.Log(transform.name);
        basePos = ModelManager.instance.activeModel.skeleton.transform.position;
        if (ModelManager.instance.activeModel != null)
        {
            modelHeight = ModelManager.instance.activeModel.height;
        }
        Settings.AddSettingsConfirmedListener(InactivateTracking);
    }

    public void InactivateTracking()
    {
        trackingActivated = Settings.data.hightTrackerActivated;
    }

    public void StartTrackingHeight()
    {
        textObj = Instantiate(Resources.Load<GameObject>(prefabPath));
        textObj.transform.parent = transform;
        text = textObj.GetComponent<Text>();
        textObj.SetActive(true);
        trackingOn = true;
    }

    public void QuitTrackingHeight()
    {
        Destroy(textObj);
        text = null; textObj = null;
        trackingOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingOn && trackingActivated)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == ModelManager.instance.activeModel.meshCollider)
            {
                Vector3 curDiffrence = hit.point - basePos;
                float meshHeight = ModelManager.instance.activeModel.meshCollider.bounds.size.y;
                double heightIfStandardHeight = Math.Round(curDiffrence.y, 1, MidpointRounding.ToEven) * scalingCoeff;
                double correctHeight = (modelHeight / meshHeight) * curDiffrence.y;
                text.text = Math.Round(correctHeight).ToString();
                textObj.transform.position = Input.mousePosition + textOffset;
            }
            else
            {
                text.text = "";
            }
        }
        
    }
}
