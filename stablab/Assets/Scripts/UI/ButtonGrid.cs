using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGrid : MonoBehaviour
{
    public GameObject area;
    private float areaX;
    private float areaY;
    private int padding = 5;
    private List<GameObject> gridObjects = new List<GameObject>();
    private float sizeOfbuttons;
    private int numCols = 5;
    public GameObject gizmoObj;
    private RuntimeGizmos.TransformGizmo gizmo;

    private void Start()
    {
        gizmo = gizmoObj.GetComponent<RuntimeGizmos.TransformGizmo>();
        areaX = ((RectTransform)area.transform).rect.x;
        areaY = ((RectTransform)area.transform).rect.y;
    }

    private void OnEnable()
    {
        Load();
    }

    private void Load()
    {
        RemoveAllButtons();
        RectTransform rectArea = area.GetComponent<RectTransform>();
        Vector3[] c = new Vector3[4];
        rectArea.GetWorldCorners(c);
        float width = (c[2] - c[1]).x;
        float height = (c[0] - c[1]).y;

        sizeOfbuttons = (width - (padding * (numCols +1))) / numCols;
        LoadAllGridObjects();
        int nrW = 0;
        int nrH = 0;
        foreach(GameObject o in gridObjects){
            if (nrW >= numCols)
            {
                nrH++;
                nrW = 0;
            }
            CreateButtonForObject(o, nrW, nrH);
            nrW++;
        }
        AddButtonsToGrid();
    }

    public void RemoveAllButtons() {
        foreach (Transform child in area.transform) {
            Destroy(child.gameObject);
        }
        gridObjects.Clear();
    }

    public void LoadAllGridObjects()
    {
        /*DirectoryInfo levelDirectoryPath = new DirectoryInfo(InjuryManager.activeInjury.GetModelPath());
        FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*.*", SearchOption.AllDirectories);*/
        foreach(GameObject weapon in Resources.LoadAll<GameObject>(InjuryManager.instance.activeInjury.injuryData.weaponData.resourcePath))
        {
            weapon.SetActive(false);
            gridObjects.Add(weapon);
        }
    }

    public void CreateButtonForObject(GameObject o, int nrW,int nrH)
    {
        GameObject buttonObj = Instantiate(Resources.Load<GameObject>("GridButton"));
        buttonObj.transform.parent = area.transform;
        RectTransform butRect = buttonObj.GetComponent<RectTransform>();
        butRect.sizeDelta = new Vector2(sizeOfbuttons * 2, sizeOfbuttons * 2);
        butRect.anchoredPosition = new Vector2(0, 0);
        buttonObj.transform.position = area.transform.position + new Vector3(padding * (nrW+1) + nrW * sizeOfbuttons, -1 * (padding * (nrH+1) + nrH * sizeOfbuttons),0);

        //buttonObj.GetComponent<RectTransform>().anchorMax = new Vector2(sizeOfbuttons, sizeOfbuttons);
        //buttonObj.GetComponent<RectTransform>().anchorMin = new Vector2(sizeOfbuttons, sizeOfbuttons);

        //Texture2D image = new Texture2D(10,10);

        Button curButton = buttonObj.GetComponent<Button>();
        o.transform.position = InjuryManager.instance.activeInjury.injuryData.weaponData.transformData.position;
        o.transform.rotation = InjuryManager.instance.activeInjury.injuryData.weaponData.transformData.rotation;
        curButton.onClick.AddListener(delegate { SetNewModel(o); });
        buttonObj.AddComponent<GridButtonProperies>().model = o;

    }

    public void AddButtonsToGrid() { }

    public void SetNewModel(GameObject o) {

        //InjuryManager.TransformActive(GetTypeFromObjectName(o),InjuryManager.activeInjury.isInHole);
        if (InjuryManager.instance.activeInjury)
        {
            string path = InjuryManager.instance.activeInjury.injuryData.weaponData.resourcePath;
            InjuryManager.instance.activeInjury.injuryData.weaponData.prefabName = path +"/"+ o.name;
            Debug.Log(o.name);

            Destroy(InjuryManager.instance.activeInjury.weaponObj);
            GameObject copy = Instantiate(o, o.transform.position, o.transform.rotation, o.transform.parent);
            InjuryManager.instance.activeInjury.weaponObj = copy;
            InjuryManager.instance.activeInjury.FetchMarkerWeapon();
            copy.SetActive(true);
        }
        //InjuryManager.activeInjury.SetModelName(path);
    }
}
