using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryListHandler : MonoBehaviour
{

    [SerializeField] private Transform buttonArea;
    [SerializeField] private UnityEngine.UI.Button addButton;
    [SerializeField] private InjuryButton injuryButton;
    [SerializeField] private UnityEngine.UI.Button previousButton;
    [SerializeField] private UnityEngine.UI.Button nextButton;

    [SerializeField] private LeftPanelAnimation panel;

    [SerializeField] private InjuryManager injuryManager;

    private int injuryCount = 0; // Debugging purpose!!
    private int activeInjury = -1; // Debugging purpose!!
    
    private List<InjuryButton> injuryButtons = new List<InjuryButton>();
    private int highestId = -1;

    private float border = 0;
    private float borderThreshold = 5;
    private float buttonSide = 0;
    private int buttonCount = 0;
    private int MARIGIN = 10;

    private Vector2 res;

    // Start is called before the first frame update
    void Start()
    {
        Calculate();

        //Spawn the add button and resize it according to the button area
        UnityEngine.UI.Button btn = Instantiate(addButton, buttonArea);
        btn.onClick.AddListener(AddInjury);
        RectTransform rtab = (RectTransform)btn.transform;
        rtab.sizeDelta = new Vector2(buttonSide, buttonSide);
        rtab.anchoredPosition = new Vector2(buttonSide / 2, 0);
        addButton = btn;

        // Set the correct size for the injury buttons
        RectTransform rtib = (RectTransform)injuryButton.transform;
        rtib.sizeDelta = new Vector2(buttonSide, buttonSide);

        res = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (res.x != Screen.width)
        {
            Resize();
            res.x = Screen.width;
            res.y = Screen.height;

        }
    }

    void Resize()
    {
        // We need to calculate how many buttons can fit in this area
        Calculate();

        // Remove buttons if we don't have space
        while (buttonCount < injuryButtons.Count)
        {
            RemoveButton();
        }

        // Reposition buttons
        RepositionButtons();


        // Add buttons if more buttons can be put in the area
        while (buttonCount > injuryButtons.Count && injuryButtons.Count < injuryCount)
        {
            AddButton();
        }
    }

    void Calculate()
    {
        
        RectTransform rtba = (RectTransform)buttonArea;
        buttonSide = rtba.rect.height - MARIGIN;
        float width = rtba.rect.width - MARIGIN;
        buttonCount = (int)Mathf.Floor(width / buttonSide);
        border = (width % buttonSide) / (buttonCount - 1);
        if (border < borderThreshold)
        {
            buttonCount -= 1;
            border = (width - buttonSide * buttonCount) / (buttonCount - 1);
        }

        buttonCount -= 1; // Compensate for the add button
    }

    void RemoveButton()
    {
        InjuryButton ib = injuryButtons[injuryButtons.Count - 1];
        injuryButtons.Remove(ib);
        Destroy(ib.gameObject);
        highestId--;
        nextButton.interactable = true;
    }

    void RepositionButtons()
    {
        float xpos = buttonSide / 2;
        foreach (InjuryButton btn in injuryButtons)
        {
            RectTransform rt = (RectTransform)btn.transform;
            rt.anchoredPosition = new Vector2(xpos, 0);
            xpos += buttonSide + border;
        }
        RectTransform rtab = (RectTransform)addButton.transform;
        rtab.anchoredPosition = new Vector2(xpos, 0);
    }

    void AddButton()
    {
        InjuryButton ib = Instantiate(injuryButton, buttonArea);

        ib.OnCheckedInjury.AddListener(ActivateInjury);
        ib.OnUncheckedInjury.AddListener(DeactivateInjury);

        ib.transform.position = addButton.transform.position;

        addButton.transform.position += new Vector3(buttonSide + border, 0, 0);

        if ((highestId++ + 1) == injuryCount) RollRight();

        ib.id = highestId;

        if (ib.id == activeInjury)
        {
            ib.Checked(false);
        }
        else
        {
            ib.Unchecked(false);
        }

        injuryButtons.Add(ib);
        if ((highestId + 1) == injuryCount) nextButton.interactable = false;
        if ((highestId + 1) == (injuryButtons.Count)) previousButton.interactable = false;
    }

    public void AddInjury()
    {
        injuryCount++;
        if (buttonCount > injuryButtons.Count && injuryButtons.Count < injuryCount)
        {
            AddButton();
        } else
        {
            RollLeft();
        }
    }

    public void ActivateInjury(int id)
    {
        if (id != activeInjury)
        {
            Debug.Log("Activated : " + id);
            activeInjury = id;
            panel.OpenPanel();
        }
    }

    public void DeactivateInjury(int id)
    {
        if (id == activeInjury)
        {
            Debug.Log("Deactivated : " + id);
            activeInjury = -1;
            panel.ClosePanel();
        }
    }

    public void RollLeft()
    {
        Debug.Log("Rolling Left");
        if ((++highestId + 1) == injuryCount) nextButton.interactable = false;
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
        if ((--highestId + 1) == buttonCount) previousButton.interactable = false;
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

    public int getActiveInjuryButton() {
        return activeInjury;
    }

}
