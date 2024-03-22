using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject sceneSwitcher;
    

    public AudioSource sloppywalk;
    public AudioSource happywalk;
    public AudioSource happyMusic;
    public AudioSource spookyMusic;
    Animator playerAnim;
    


    void Start()
    {
        
        
    }

    void Update()
    {
        bool currentlyPlayground = sceneSwitcher.GetComponent<SceneSwitchDebug>().currentlyPlayground;

        
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
        GameObject player = GameObject.FindWithTag("Player");
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

        happyMusic.volume = 1 ;
        spookyMusic.volume = 0 ;
        
        
    }

    void horrorAudio()
    {
        //Alvast sorry voor deze code
        GameObject player = GameObject.FindWithTag("Player");
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

        happyMusic.volume = 0;
        spookyMusic.volume = 1;
    }
}
