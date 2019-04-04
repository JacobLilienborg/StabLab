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
                break;
            }
        }

        CheckInteractability();
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
        // If the injury with the highest index is not visible, the shrinking excluded it and the next button will be active 
        if ((rightMostIndex + 1) < InjuryManager.injuries.Count) nextButton.interactable = true;

        // Reposition the buttons according to the adjustments
        RepositionButtons();


        // Add buttons if more buttons can be put in the button area
        while (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
        {
            // If the injury with the highest index is visible, the expansion will include the previous injuries
            if ((rightMostIndex + 1) == InjuryManager.injuries.Count) GoToPrevious();
            AddButton();
        }

        // Since we have changed the size of the list the right most index may have changed which would affect the next/previous buttons
        CheckInteractability();
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

    InjuryButton AddButton()
    {
        // Spawn a new injury button
        InjuryButton ib = Instantiate(injuryButton, buttonArea);

        // Make Injury Manager a listener if the buttons are pressed
        ib.OnCheckedInjury.AddListener(InjuryManager.SetActiveInjury);
        ib.OnUncheckedInjury.AddListener(InjuryManager.DeselectInjury);

        // We want to know if we are going to activate the previous/next buttons
        ib.OnChecked.AddListener(CheckInteractability);
        ib.OnUnchecked.AddListener(CheckInteractability);

        // We position the button where the green add button is and reposition the add button
        ib.transform.position = addButton.transform.position;
        addButton.transform.position += new Vector3(buttonSize + padding, 0, 0);

        // The button id will be the new rightmost index in the list
        ib.id = ++rightMostIndex;

        // Add the button to the list of injury buttons
        injuryButtons.Add(ib);

        return ib;
    }

    // And a new injury and set is as the active
    public void AddInjury()
    {
        InjuryManager.AddNewInjury();
        if (totalButtonAmount > injuryButtons.Count && injuryButtons.Count < InjuryManager.injuries.Count)
        {
            AddButton().Checked();
            CheckInteractability();
        }
        else
        {
            GoToNext(true);
        }
    }

    // Check if the previous/next button are going to be interactable
    private void CheckInteractability()
    {

        previousButton.interactable = ((rightMostIndex + 1) != injuryButtons.Count);
        nextButton.interactable = ((rightMostIndex + 1) != InjuryManager.injuries.Count);

        // If active injury
        if (InjuryManager.activeInjury != null)
        {
            previousButton.interactable |= InjuryManager.activeInjury != InjuryManager.injuries[injuryButtons[0].id];
            nextButton.interactable |= InjuryManager.activeInjury != InjuryManager.injuries[injuryButtons[injuryButtons.Count - 1].id];
        }
    }

    // This will "move" the entire list one step to the right except if an injury is active then it will select the next injury instead
    public void GoToNext(bool selectRightMost = false)
    {
        if (InjuryManager.activeInjury == null || InjuryManager.injuries[injuryButtons[injuryButtons.Count - 1].id] == InjuryManager.activeInjury)
        {
            foreach (InjuryButton button in injuryButtons) { button.id++; }
            rightMostIndex++;
            if (InjuryManager.activeInjury != null || selectRightMost) injuryButtons[injuryButtons.Count - 1].Checked();
        }
        else
        {
            for (int i = 0; i < injuryButtons.Count; i++) {
                if (InjuryManager.injuries[injuryButtons[i].id] == InjuryManager.activeInjury)
                {
                    injuryButtons[i+1].Checked();
                    break;
                }
            }
        }
        CheckInteractability();
    }

    // This will "move" the entire list one step to the left except if an injury is active then it will select the previous injury instead
    public void GoToPrevious()
    {
        if (InjuryManager.activeInjury == null || InjuryManager.injuries[injuryButtons[0].id] == InjuryManager.activeInjury)
        {
            foreach (InjuryButton button in injuryButtons) { button.id--; }
            rightMostIndex--;
            if (InjuryManager.activeInjury != null) injuryButtons[0].Checked();
        }
        else
        {
            for (int i = 0; i < injuryButtons.Count; i++)
            {
                if (InjuryManager.injuries[injuryButtons[i].id] == InjuryManager.activeInjury)
                {
                    injuryButtons[i-1].Checked();
                    break;
                }
            }
        }
        CheckInteractability();
    }
}
