/*
 * Created: Martin Jirenius
 * Last modified: Martin Jirenius
 */

using System.Collections.Generic;
using UnityEngine;

/*
 * Injury List Handler is the bar where you can add injuries in the injury mode.
 * This class is meant to spawn and remove injury buttons
 * 
 * It will also shrink and expand the list which in turn decides the amount of buttons in the list.
 * If there exists more injuries than the list can show, the previous/next button will turn blue and
 * the user can click to go through the list
 */

public class InjuryListHandler : MonoBehaviour
{
    public bool selectNewInjury;  // If true, whenever a new injury is added it will be selected. If false, it will not. 
    public bool selectScrolledInjury; // if true, the previous/next buttons will switch which injury is selected. If false, it scrolls the list. 

    [SerializeField] private Transform buttonArea; // The area where all the buttons will be spawned, including the green add button
    [SerializeField] private UnityEngine.UI.Button addButton; // The green add button
    [SerializeField] private InjuryButton injuryButton; // The button representing an injury
    [SerializeField] private UnityEngine.UI.Button previousButton; // A reference to the previous button
    [SerializeField] private UnityEngine.UI.Button nextButton; // A reference to the next button

    private List<InjuryButton> injuryButtons = new List<InjuryButton>(); // A list of all injury buttons present in the list 
    private int rightMostIndex = -1; // The right most index present in the list

    private float padding = 0; // The padding between each button in the button area, the value is calculated
    private float paddingThreshold = 5; // If the padding would be smaller than this value, a button will be removed and the padding will increase.
    private float buttonSize = 0; // The size of each side of the button
    private int totalButtonAmount = 0; // The amount of buttons which can be fit into the button area. 
    
    private Vector2 res; // The current screen resolution

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the padding, button size and the total button amount
        CalculateScreenAdjustments();

        //Spawn the add button and resize it according to the button area
        addButton = Instantiate(addButton, buttonArea);
        addButton.onClick.AddListener(AddInjury);
        RectTransform rtab = (RectTransform)addButton.transform;
        rtab.sizeDelta = new Vector2(buttonSize, buttonSize);
        rtab.anchoredPosition = new Vector2(buttonSize / 2, 0);

        // Set the correct size for the injury buttons
        RectTransform rtib = (RectTransform)injuryButton.transform;
        rtib.sizeDelta = new Vector2(buttonSize, buttonSize);

        res = new Vector2(Screen.width, Screen.height); // Save the start resolution

        LoadInjuries();

    }

    // Update is called once per frame
    void Update()
    {
        // If the window has changed in width, resize the list
        if (res.x != Screen.width)
        {
            Resize();
            res.x = Screen.width;
            res.y = Screen.height;

        }
    }

    private void LoadInjuries()
    {
        RollLeft();
        for (int i = 0; i < InjuryManager.injuries.Count; i++)
        {
            if (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
            {
                AddButton();
            }
            else
            {
                RollLeft();
            }
        }
    }

    void Resize()
    {
        // We need to calculate how many buttons can fit in this area
        CalculateScreenAdjustments();

        // Remove buttons if we don't have space
        while (totalButtonAmount < injuryButtons.Count)
        {
            RemoveButton();
        }

        // Reposition buttons
        RepositionButtons();


        // Add buttons if more buttons can be put in the area
        while (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
        {
            AddButton();
        }
    }

    void CalculateScreenAdjustments()
    {

        RectTransform rtba = (RectTransform)buttonArea;
        buttonSize = rtba.rect.height;
        float width = rtba.rect.width;
        totalButtonAmount = (int)Mathf.Floor(width / buttonSize);
        padding = (width % buttonSize) / (totalButtonAmount - 1);
        if (padding < paddingThreshold)
        {
            totalButtonAmount -= 1;
            padding = (width - buttonSize * totalButtonAmount) / (totalButtonAmount - 1);
        }

        totalButtonAmount -= 1; // Compensate for the add button

    }

    void RemoveButton()
    {
        InjuryButton ib = injuryButtons[injuryButtons.Count - 1];
        injuryButtons.Remove(ib);
        Destroy(ib.gameObject);
        rightMostIndex--;
        nextButton.interactable = true;
    }

    void RepositionButtons()
    {
        float xpos = buttonSize / 2;
        foreach (InjuryButton btn in injuryButtons)
        {
            RectTransform rt = (RectTransform)btn.transform;
            rt.anchoredPosition = new Vector2(xpos, 0);
            xpos += buttonSize + padding;
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

        addButton.transform.position += new Vector3(buttonSize + padding, 0, 0);

        if ((rightMostIndex++ + 1) == InjuryManager.injuries.Count) RollRight();

        ib.id = rightMostIndex;

        if (InjuryManager.injuries[ib.id] == InjuryManager.activeInjury)
        {
            ib.Checked(false);
        }
        else
        {
            ib.Unchecked(false);
        }

        injuryButtons.Add(ib);
        if ((rightMostIndex + 1) == InjuryManager.injuries.Count) nextButton.interactable = false;
        if ((rightMostIndex + 1) == (injuryButtons.Count)) previousButton.interactable = false;
    }

    public void AddInjury()
    {
        /*InjuryManager.AddNewInjury();
        if (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
        {
            AddButton();
        } else
        {
            RollLeft();
        }*/
    }

    public void ActivateInjury(int id)
    {
        if (id != activeInjury)
        {
            Debug.Log("Activated : " + id);
            activeInjury = id;
            InjuryManager.SetActiveInjury(id-1);
            //injurySettings.LoadActiveInjury();

        }
    }

    public void DeactivateInjury(int id)
    {
        if (id == activeInjury)
        {
            Debug.Log("Deactivated : " + id);
            activeInjury = -1;
            InjuryManager.SetActiveInjury(-1);
        }
    }

    public void RollLeft()
    {
        Debug.Log("Rolling Left");
        if ((++rightMostIndex + 1) == injuryCount) nextButton.interactable = false;
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
        if ((--rightMostIndex + 1) == totalButtonAmount) previousButton.interactable = false;
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

    public void DeactivateActiveInjury() {
        foreach (InjuryButton ib in injuryButtons)
        {
            ib.Unchecked();
        }
    }

}
