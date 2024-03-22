using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject sceneSwitcher;
    
    //SFX
    public AudioSource sloppywalk;
    public AudioSource happywalk;
    public AudioSource balloonPop;
    public AudioSource eyePop;
    //Music
    public AudioSource happyMusic;
    public AudioSource spookyMusic;
    Animator playerAnim;

    private bool currentlyPlayground;



    void Awake()
    {
        currentlyPlayground = sceneSwitcher.GetComponent<SceneSwitchDebug>().currentlyPlayground;

    }

    private void FixedUpdate()
    {
       currentlyPlayground = sceneSwitcher.GetComponent<SceneSwitchDebug>().currentlyPlayground;
    }

    void Update()
    {

        
        if (currentlyPlayground == true)
        {
            playgroundAudio();
            sloppywalk.volume = 0;
        }
        else
        {
            horrorAudio();
            happywalk.volume = 0;
        }
    }

    void playgroundAudio()
    {
        //Alvast sorry voor deze code
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject player in players)
        {
        playerAnim = player.GetComponent<Animator>();
        bool walking = playerAnim.GetBool("Walking");
        if ( walking == true)
        {
            happywalk.volume = 1 ;
        }
        else
        {
            happywalk.volume = 0;
        }
            
        }

        happyMusic.volume = 1 ;
        spookyMusic.volume = 0 ;
    }

    void horrorAudio()
    {
        //Alvast sorry voor deze code
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            playerAnim = player.GetComponent<Animator>();
            bool walking = playerAnim.GetBool("Walking");
            if (walking == true)
            {
                sloppywalk.volume = 1;
            }
            else
            {
                sloppywalk.volume = 0;
            }
        }

        happyMusic.volume = 0;
        spookyMusic.volume = 1;
    }

    public void PopSound()
    {
        if(currentlyPlayground)  { balloonPop.Play(); }
        if(!currentlyPlayground) { eyePop.Play(); }
    }
}
