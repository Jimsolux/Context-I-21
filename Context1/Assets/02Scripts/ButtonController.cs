using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance;
    GameObject[] players;
    int[] isPressedInts = new int[3];


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
        UpdateButtons(); // starts false

    }

    void FixedUpdate()
    {
        if(buttonsCurrentlyPressed == amountOfButtonsInLevel)
        {
            levelEvent.Invoke();
        }
    }

    public void UpdateButtons()
    {
        //try
        //{
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");// Makes Array with all active buttons
            //Debug.Log(buttons.Length);
            for (int i = 0; i < buttons.Count(); i++)
            {
                TestBotan buttonthing = buttons[i].GetComponent<TestBotan>(); // instance of that ButtonController
                int myInt = Convert.ToInt32(buttonthing.clicked);
                isPressedInts[i] = myInt;
                //Debug.Log(buttonthing.clicked);
            }

            buttonsCurrentlyPressed = isPressedInts[0] + isPressedInts[1] + isPressedInts[2];
        //}
        //catch (Exception e)
        //{
            //buttonsCurrentlyPressed = 0;
            //Debug.Log("Couldnt properly update buttons");
        //}

        UserInterfaceManager.instance.UpdateButtonCount(buttonsCurrentlyPressed, amountOfButtonsInLevel);
        //Debug.Log(buttonsCurrentlyPressed);
    }




}
