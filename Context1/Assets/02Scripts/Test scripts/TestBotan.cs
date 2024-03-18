using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TestBotan : MonoBehaviour
{
    Animator anim;

    List<Transform> playersInRange;
    Transform activePlayer;


    void Start()
    {
        anim = GetComponent<Animator>();
        List<Transform> playersInRange = new List<Transform>();

    }

    public bool clicked;



    void Update()
    {
        anim.SetBool("Pressed", clicked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerSystem activePlayerSystem = other.GetComponent<PlayerSystem>();
            activePlayerSystem.canPressButton = true;
            activePlayerSystem.activeButton = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerSystem activePlayerSystem = other.GetComponent<PlayerSystem>();
            activePlayerSystem.canPressButton = false;
            activePlayerSystem.activeButton = null;
        }
    }


    public void PressButton()
    {
        clicked = true;
    }
    public void UnPressButton()
    {
        clicked = false;
    }
}
