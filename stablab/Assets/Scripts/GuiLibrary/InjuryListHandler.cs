/*
 * Created: Martin Jirenius
 * Last modified: Martin Jirenius
 */

using System.Collections.Generic;
using UnityEngine;

/*
 * Injury List Handler is the bar where you can add injuries in the injury mode.
 * This class is meant to spawn and remove injury buttons which in turn can invoke the activation of injuries
 * 
 * It will also shrink and expand the list which in turn decides the amount of buttons in the list.
 * If there exists more injuries than the list can show, the previous/next button will turn blue and
 * the user can click to go through the list
 */

public class InjuryListHandler : MonoBehaviour
{
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

        // Spawn the add button and resize it according to the button area
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

    // Load in already existing injuries if the injury manager has any
    public void LoadInjuries()
    {
        // Make the list start from the beginning without removing
        rightMostIndex = -1;
        foreach(InjuryButton button in injuryButtons)
        {
            button.id = ++rightMostIndex;
        }

        // Remove if we have to many buttons, add if to few and enable next button if there are more injuries available
        foreach (Injury injury in InjuryManager.injuries)
        {
            if (injuryButtons.Count > InjuryManager.injuries.Count)
            {
                RemoveButton();
            }
            else if (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
            {
                AddButton();
            }
            else
            {
                if (injuryButtons.Count < InjuryManager.injuries.Count) nextButton.interactable = true;
                return;
            }
        }
    }
    
    // Calculates screen adjusments, remove/add buttons if neccesary and position them correctly
    void Resize()
    {
        // Calculate the padding, button size and the total button amount
        CalculateScreenAdjustments();

        // Remove buttons if we don't have enough space
        while (totalButtonAmount < injuryButtons.Count)
        {
            RemoveButton();
        }

        // Reposition the buttons according to the adjustments
        RepositionButtons();


        // Add buttons if more buttons can be put in the button area
        while (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
        {
            AddButton();
        }
    }

    // Calculate the padding, button size and the total button amount
    void CalculateScreenAdjustments()
    {
        RectTransform rtba = (RectTransform)buttonArea; // Simulate an rectangle to calculate on
        buttonSize = rtba.rect.height;  // Each side on the button will have the same size as the height of the button area
        float width = rtba.rect.width;
        totalButtonAmount = (int)Mathf.Floor(width / buttonSize); // Integer division between width and button size
        // To get an consistent padding we take the remainder of the space available and divide it by the amount of buttons - 1
        padding = (width % buttonSize) / (totalButtonAmount - 1);
        if (padding < paddingThreshold) // If the padding is to small, "remove" a button and recalculate
        {
            totalButtonAmount -= 1;
            padding = (width - buttonSize * totalButtonAmount) / (totalButtonAmount - 1);
        }

        totalButtonAmount -= 1; // Since the add button is already in the button area
    }

    // Removes the right most buttons from the list 
    void RemoveButton() 
    {
        InjuryButton ib = injuryButtons[injuryButtons.Count - 1];
        injuryButtons.Remove(ib);
        Destroy(ib.gameObject);
        rightMostIndex--;
        nextButton.interactable = true;
    }

    // Repositioning the placed buttons according to the calculated buttons size and the padding
    void RepositionButtons()
    {
        float xpos = buttonSize / 2; // Since (assuming) the pivot point is in the middle of the button
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

        if ((rightMostIndex++ + 1) == InjuryManager.injuries.Count) ScrollRight();

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
        InjuryManager.AddNewInjury();
        if (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
        {
            AddButton();
        } else
        {
            ScrollLeft();
        }
    }

    public void ActivateInjury(int id)
    {
        InjuryManager.SetActiveInjury(id-1);
    }

    public void DeactivateInjury(int id)
    {
        if (InjuryManager.injuries[id] == InjuryManager.activeInjury)
        {
            InjuryManager.SetActiveInjury(-1);
        }
    }

    public void ScrollLeft()
    {
        Debug.Log("Scrolling Left");
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

    public void ScrollRight()
    {
        Debug.Log("Scrolling Right");
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
