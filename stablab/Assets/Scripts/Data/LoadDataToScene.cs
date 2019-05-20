using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadDataToScene : MonoBehaviour
{
    public  GameObject    imageAreaObj;
    private ImagesHandler imageHandeler;
    public  GameObject    textObj;
    private TMP_Text      text;

    void Start()
    {
        imageHandeler = imageAreaObj.GetComponent<ImagesHandler>();
        text          = textObj.GetComponent<TMP_Text>();
        InjuryManager.AddActivationListener(SetMetadata);
        InjuryManager.AddDeactivationListener(ResetMetadata);
    }

    void SetMetadata()
    {
        imageHandeler.LoadAllImages();
        text.text = InjuryManager.activeInjury.InfoText;
    }

    void ResetMetadata()
    {
        text.text = "";
        imageHandeler.LoadAllImages();
    }
}
