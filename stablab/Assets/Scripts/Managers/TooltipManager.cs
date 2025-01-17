﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    static bool show;
    static string currentInfo;
    private Text text;
    private Image image;
    private bool onePulse = true;
    void Start()
    {
        show = false;
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            image.enabled = true;
            text.enabled = true;
            text.text = currentInfo;
        }else
        {
            image.enabled = false;
            text.enabled = false;
        }
    }

    public static void Show(string info)
    {
        show = true;
        currentInfo = info; //Update textContent
    }
    public static void Hide()
    {
        show = false;
    }
}
