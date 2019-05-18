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
        InjuryManager.AddActivationListener(SetMetadata);
        InjuryManager.AddDeactivationListener(ResetMetadata);

        if(InjuryManager.activeInjury != null)
            text.text = InjuryManager.activeInjury.InfoText;
        InjuryManager.AddActivationListener(UpdateInjuryData);
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
