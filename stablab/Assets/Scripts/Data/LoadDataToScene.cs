using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadDataToScene : MonoBehaviour
{
    public GameObject imageAreaObj;
    private ImagesHandler imageHandeler;
    public GameObject textObj;
    private TextMeshProUGUI text;
    void Start()
    {
        imageHandeler = imageAreaObj.GetComponent<ImagesHandler>();
        text = textObj.GetComponent<TextMeshProUGUI>();
        text.SetText("THIS IS TEXT");
        InjuryManager.instance.AddActivationListener(SetMetadata);
        InjuryManager.instance.AddDeactivationListener(ResetMetadata);
    }

    void SetMetadata(InjuryController activeInjury)
    {
        imageHandeler.LoadAllImages();
        text.SetText(activeInjury.injuryData.infoText);
    }

    void ResetMetadata()
    {
        text.SetText("");
        imageHandeler.LoadAllImages();
    }
}
