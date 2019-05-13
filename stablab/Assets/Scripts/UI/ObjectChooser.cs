using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChooser : MonoBehaviour
{
    public List<GameObject> objects;

    public void ShowObject(GameObject objec)
    {
        foreach (GameObject single in objects)
        {
            if (single == objec)
            {
                single.SetActive(true);
            }
            else {
                single.SetActive(false);
            }
        }
    }

}
