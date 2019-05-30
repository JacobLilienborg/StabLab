using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    static  bool         show, clicked;
    static  string       currentInfo;
    private TMP_Text     text;
    private Image        image;

    void Start()
    {
        show  = false;
        text  = GetComponentInChildren<TMP_Text>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (show && (clicked || Settings.data.tooltipEnabled))
        {
            image.enabled = true;
            text.enabled  = true;
            text.text     = currentInfo;
        }else
        {
            image.enabled = false;
            text.enabled  = false;
        }
    }

    public static void Show(string info, bool click)
    {
        show        = true;
        clicked     = click;
        currentInfo = info; //Update textContent
    }

    public static void Hide()
    {
        show = false;
        clicked = false;
    }

}
