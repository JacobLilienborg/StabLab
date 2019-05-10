using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDataToScene : MonoBehaviour
{
    public GameObject imageAreaObj;
    private ImagesHandler imageHandeler;
    public GameObject textObj;
    private Text text;
    void Start()
    {
        imageHandeler = imageAreaObj.GetComponent<ImagesHandler>();
        text = textObj.GetComponent<Text>();
        imageHandeler.LoadAllImages();
        //if(text != null) Debug.Log("asd");
        text.text = InjuryManager.activeInjury.InfoText;
        InjuryManager.AddActivationListener(UpdateInjuryData);
    }
    void UpdateInjuryData(Injury injury)
    {
        text.text = injury.InfoText;
        imageHandeler.LoadAllImages();
    }

    void Update(){
        //UpdateDataOnNewActiveInjury();
    }
}
