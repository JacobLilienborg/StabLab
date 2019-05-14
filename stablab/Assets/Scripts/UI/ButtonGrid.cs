using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGrid : MonoBehaviour
{
    public GameObject area;
    private int padding = 5;
    private FileInfo[] gridObjects;
    private float sizeOfbuttons;
    private int numCols = 5;

    private void Load()
    {
        RectTransform rectArea = (RectTransform)area.transform;
        float width = rectArea.rect.width;
        float height = rectArea.rect.height;

        sizeOfbuttons = (width / numCols) - (numCols + 1) * padding;
        LoadAllGridObjects();
        int nrW = 0;
        int nrH = 0;
        foreach(FileInfo o in gridObjects){
            nrW++; nrH++;
            CreateButtonForObject(o.Name,nrW,nrH);
            if (nrW >= numCols)
            {
                nrH++;
                nrW = 0;
            }
        }
        AddButtonsToGrid();
    }

    public void LoadAllGridObjects()
    {
        DirectoryInfo levelDirectoryPath = new DirectoryInfo(InjuryManager.activeInjury.GetModelPath());
        FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*.*", SearchOption.AllDirectories);
    }

    public void CreateButtonForObject(string path, int nrW,int nrH)
    {
        GameObject buttonObj = Instantiate((GameObject)Resources.Load("GridModel.prefab"));
        buttonObj.transform.position = new Vector3(padding + nrW*padding + nrW*sizeOfbuttons, padding + nrH * sizeOfbuttons + nrH * padding,0);
        //Texture2D image = new Texture2D(10,10);

        Button curButton = buttonObj.GetComponent<Button>();
        curButton.onClick.AddListener(delegate { SetNewModel(path); });

    }

    public void AddButtonsToGrid() { }

    public void SetNewModel(string path) {
        GameObject newModel = Instantiate((GameObject)Resources.Load(path));
        InjuryManager.activeInjury.AddModel(newModel);
        //InjuryManager.activeInjury.SetModelName(path);
    }


}
