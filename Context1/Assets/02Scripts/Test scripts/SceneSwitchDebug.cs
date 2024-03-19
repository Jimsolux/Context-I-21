using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchDebug : MonoBehaviour
{
    // let niet op de namen van de variables, dit is letterlijk gwn voor testen

    public GameObject playGround;
    public GameObject bioHorror;

    bool currentlyPlayground;

    private void Start()
    {
        playGround.SetActive(true);
        bioHorror.SetActive(false);
        currentlyPlayground = true;
    }


    public void SwapMood()
    {
        switch (currentlyPlayground)    // Checks if its playground and swaps it with Biohorror or the other way around
        {
            case false: // Its not playground --> becomes playGround
                playGround.SetActive(true);
                bioHorror.SetActive(false);
                currentlyPlayground =true; 
                break;
            case true:  // Its playground --> Becomes biohorror
                playGround.SetActive(false);
                bioHorror.SetActive(true);
                currentlyPlayground =false; 
                break;
        }
    }
}
