using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] UnityEvent levelEvent;
    // Amount of buttons pressed in.
    [SerializeField] private int amountOfButtonsInLevel = 3;
    public int buttonsCurrentlyPressed;
    [SerializeField] public LayerMask buttonLayer;

    void Start()
    {
        buttonsCurrentlyPressed = 0;
        UpdateButtons(false); // starts false
    }

    void FixedUpdate()
    {
        if(buttonsCurrentlyPressed == amountOfButtonsInLevel)
        {
            levelEvent.Invoke();
        }
    }

    public void UpdateButtons(bool pressed)
    {
        if(pressed && buttonsCurrentlyPressed < 4) { buttonsCurrentlyPressed += 1; }
        if(!pressed&& buttonsCurrentlyPressed > 0) { buttonsCurrentlyPressed -= 1; }
        UserInterfaceManager.instance.UpdateButtonCount(buttonsCurrentlyPressed, amountOfButtonsInLevel);
        Debug.Log(buttonsCurrentlyPressed);
    }



}
