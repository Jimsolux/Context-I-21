using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance;
    GameObject[] players;
    int[] isPressedInts = new int[3];


    private void Awake()
    {
        instance = this;
    }

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
            ChangeSceneWao();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeSceneWao();
        }
    }

    public void ChangeSceneWao()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in players)
        {
            g.GetComponent<PlayerSystem>().SceneSwitch();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void WipeButtons()
    {
        isPressedInts = new int[3];
    }

    int currentlyClicked = 0;
    public void UpdateButtons()
    {
        //try
        //{
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");// Makes Array with all active buttons
            Debug.Log(buttons.Length);
        currentlyClicked = 0;
            for (int i = 0; i < buttons.Count(); i++)
            {
                TestBotan buttonthing = buttons[i].GetComponent<TestBotan>(); // instance of that ButtonController
                int myInt = Convert.ToInt32(buttonthing.clicked);
            Debug.Log(myInt);
            if (myInt == 1)
                currentlyClicked++;
                //Debug.Log(buttonthing.clicked);
            }

        if (currentlyClicked == 3) ChangeSceneWao();
            //buttonsCurrentlyPressed = isPressedInts[0] + isPressedInts[1] + isPressedInts[2];
            //}
            //catch (Exception e)
            //{
            //buttonsCurrentlyPressed = 0;
            //Debug.Log("Couldnt properly update buttons");
            //}

        UserInterfaceManager.instance.UpdateButtonCount(currentlyClicked, amountOfButtonsInLevel);
        //Debug.Log(buttonsCurrentlyPressed);
    }




}
