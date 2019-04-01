using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryListHandler : MonoBehaviour
{

    [SerializeField] private Transform injuryArea;
    [SerializeField] private UnityEngine.UI.Button addButton;
    [SerializeField] private InjuryButton injuryButton;
    [SerializeField] private UnityEngine.UI.Button previousButton;
    [SerializeField] private UnityEngine.UI.Button nextButton;

    [SerializeField] private InjuryManager injuryManager;
    private int injuryCount = 0; // Debugging purpose!!
    private int activeInjury = -1; // Debugging purpose!!
    
    private List<InjuryButton> injuryButtons = new List<InjuryButton>();
    private int highestId = 0;

    private float border;
    private float borderThreshold = 5;
    private float buttonSide;
    private int buttonCount;

    // Start is called before the first frame update
    void Start()
    {
        // We need to calculate how many buttons can fit in this area
        RectTransform rt = (RectTransform)transform;
        RectTransform rtia = (RectTransform)injuryArea;
        RectTransform rtab = (RectTransform)addButton.transform;
        //buttonSide = rt.sizeDelta.y + rtba.sizeDelta.y; // CALCULATED SIZE
        buttonSide = rtab.sizeDelta.y;
        float width = rt.sizeDelta.x + rtia.sizeDelta.x;
        buttonCount = (int)Mathf.Floor(width / buttonSide);
        border = (width % buttonSide) / (buttonCount - 1);
        if(border < borderThreshold)
        {
            buttonCount -= 1;
            border = (width - buttonSide*buttonCount) / (buttonCount - 1);
        }

        // Compensate for the add button
        
        buttonCount -= 1;
        rt = (RectTransform)addButton.transform;
        rt.sizeDelta = new Vector2(buttonSide, buttonSide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInjury()
    {
        injuryCount++;
        if (buttonCount - injuryCount >= 0)
        {
            InjuryButton ib = Instantiate(injuryButton, injuryArea);
            ib.OnCheckedInjury.AddListener(ActivateInjury);
            ib.OnUncheckedInjury.AddListener(DeactivateInjury);
            ib.id = highestId++;
            ib.transform.position = addButton.transform.position;

            RectTransform rt = (RectTransform)ib.transform;
            rt.sizeDelta = new Vector2(buttonSide, buttonSide);

            injuryButtons.Add(ib);
            addButton.transform.position += new Vector3(buttonSide + border, 0, 0);
        } else
        {
            RollLeft();
        }
    }

    public void ActivateInjury(int id)
    {
        Debug.Log("Activated : " + id);
        activeInjury = id;
    }

    public void DeactivateInjury(int id)
    {
        Debug.Log("Deactivated : " + id);
        activeInjury = -1;
    }

    public void RollLeft()
    {
        Debug.Log("Rolling Left");
        if (++highestId == injuryCount) nextButton.interactable = false;
        previousButton.interactable = true;

        foreach (InjuryButton ib in injuryButtons)
        {
            ib.id++;
            if (ib.id == activeInjury) {
                ib.Checked(false);
            } else
            {
                ib.Unchecked(false);
            }
        }
    }

    public void RollRight()
    {
        Debug.Log("Rolling Right");
        if (--highestId == buttonCount) previousButton.interactable = false;
        nextButton.interactable = true;

        foreach (InjuryButton ib in injuryButtons)
        {
            ib.id--;
            if (ib.id == activeInjury)
            {
                ib.Checked(false);
            }
            else
            {
                ib.Unchecked(false);
            }
        }
    }

}
