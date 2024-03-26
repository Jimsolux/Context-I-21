using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SceneSwitchDebug sceneSwitcher;

    //SFX
    public AudioSource sloppywalk1;
    public AudioSource sloppywalk2;
    public AudioSource sloppywalk3;
    public AudioSource happywalk1;
    public AudioSource happywalk2;
    public AudioSource happywalk3;
    public AudioSource balloonPop;
    public AudioSource eyePop;
    //Music
    public AudioSource happyMusic;
    public AudioSource spookyMusic;
    Animator playerAnim;

    private bool currentlyPlayground;



    void Start()
    {
        sceneSwitcher = SceneSwitchDebug.instance;
        currentlyPlayground = sceneSwitcher.currentlyPlayground;

    }

    private void FixedUpdate()
    {
        currentlyPlayground = sceneSwitcher.currentlyPlayground;
    }

    void Update()
    {


        if (currentlyPlayground == true)
        {
            playgroundAudio();
            sloppywalk1.volume = 0;
            sloppywalk2.volume = 0;
            sloppywalk3.volume = 0;
        }
        else
        {
            horrorAudio();
            happywalk1.volume = 0;
            happywalk2.volume = 0;
            happywalk3.volume = 0;
        }
    }

    void playgroundAudio()
    {
        //Alvast sorry voor deze code
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            happywalk1.volume = 0;
            happywalk2.volume = 0;
            happywalk3.volume = 0;
        }

        for(int i = 0; i < players.Length; i++)
        {
            AudioSource source = happywalk1;
            if (i == 1) source = happywalk2;
            if (i == 2) source = happywalk3;
            playerAnim = players[i].GetComponent<Animator>();
            bool walking = playerAnim.GetBool("Walking");
            if (walking == true)
            {
                source.volume = 1;
            }
            else
            {
                source.volume = 0;
            }

        }

        happyMusic.volume = 0f;
        spookyMusic.volume = 0;
    }

    void horrorAudio()
    {
        //Alvast sorry voor deze code
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            sloppywalk1.volume = 0;
            sloppywalk2.volume = 0;
            sloppywalk3.volume = 0;
        }

        for (int i = 0; i < players.Length; i++)
        {
            AudioSource source = sloppywalk1;
            if (i == 1) source = sloppywalk2;
            if (i == 2) source = sloppywalk3;
            playerAnim = players[i].GetComponent<Animator>();
            bool walking = playerAnim.GetBool("Walking");
            if (walking == true)
            {
                source.volume = 1;
            }
            else
            {
                source.volume = 0;
            }
        }

        happyMusic.volume = 0;
        spookyMusic.volume = 0f;
    }

    public void PopSound()
    {
        if (currentlyPlayground) { balloonPop.Play(); }
        if (!currentlyPlayground) { eyePop.Play(); }
    }
}
