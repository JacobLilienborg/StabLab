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
    private double standardHeight;
    bool trackingOn = false;
    bool trackingActivated = true;
    // Start is called before the first frame update

    private void Start()
    {
        basePos = baseObject.transform.position;
        if (ModelManager.instance != null)
        {
            modelHeight = ModelManager.instance.GetHeight();
            standardHeight = ModelManager.instance.GetStandardHeight();
        }
        else {
            standardHeight = 180;
            modelHeight = 180;
        }
        Settings.AddSettingsConfirmedListener(InactivateTracking);
    }

    public void InactivateTracking()
    {
        trackingActivated = Settings.hightTrackerActivated;
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

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Body")
            {
                Vector3 curDiffrence = hit.point - basePos;
                double heightIfStandardHeight = Math.Round(curDiffrence.y, 1, MidpointRounding.ToEven) * scalingCoeff;
                double correctHeight = (modelHeight / standardHeight) * heightIfStandardHeight;
                text.text = Math.Round(correctHeight).ToString();
                textObj.transform.position = Input.mousePosition + textOffset;
            }
            else
            {
                text.text = "";
            }
        }
        
    }

    private void SetStandardHeight(int stdHeight) {
        standardHeight = stdHeight;
    }
}
