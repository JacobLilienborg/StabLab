using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] private ModelController man;
    [SerializeField] private ModelController women;
    [SerializeField] private ModelController child;

    private ModelController activeModel = null;

    [SerializeField] private List<Button> radiobuttons;
    [SerializeField] private Sprite checkedButton;
    [SerializeField] private Sprite uncheckedButton;

    // Update is called once per frame
    void Update()
    {
        if (radiobuttons[0].gameObject.GetComponent<Image>().sprite == checkedButton)
        {
            if (activeModel == null)
            {
                activeModel = Instantiate(man);
                
            } else if (activeModel.name != man.gameObject.name)
            {
                Destroy(activeModel.gameObject);
                activeModel = Instantiate(man);
            }
        }
        else if (radiobuttons[1].gameObject.GetComponent<Image>().sprite == checkedButton)
        {
            if (activeModel == null)
            {
                activeModel = Instantiate(women);

            }
            else if (activeModel.name != women.gameObject.name)
            {
                Destroy(activeModel.gameObject);
                activeModel = Instantiate(women);
            }
        }
        else if (radiobuttons[2].gameObject.GetComponent<Image>().sprite == checkedButton)
        {
            if (activeModel == null)
            {
                activeModel = Instantiate(child);

            }
            else if (activeModel.name != child.gameObject.name)
            {
                Destroy(activeModel.gameObject);
                activeModel = Instantiate(child);
            }
        }
    }

    public void CheckRadiobutton(int button)
    {
        for(int i = 0; i < radiobuttons.Count; i++)
        {
            if (i == button)
            {
                radiobuttons[i].gameObject.GetComponent<Image>().sprite = checkedButton;
            }
            else
            {
                radiobuttons[i].gameObject.GetComponent<Image>().sprite = uncheckedButton;
            }
            
        }
    }
}
