using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] UnityEvent levelEvent;
    // Amount of buttons pressed in.
    [SerializeField] private float amountOfButtonsInLevel = 3;
    public float buttonsCurrentlyPressed;

    void Start()
    {
        buttonsCurrentlyPressed = 0;
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
        if(pressed) { buttonsCurrentlyPressed += 1; }
        if(!pressed) { buttonsCurrentlyPressed -= 1; }
    }



}
