using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TestBotan : MonoBehaviour
{
    Animator anim;
    List<GameObject> inside = new List<GameObject>();
    [SerializeField] private GameObject buttonShow;
    [SerializeField] private GameObject triangle;
    void Start()
    {
        buttonShow.SetActive(false);
        anim = GetComponent<Animator>();
        List<Transform> playersInRange = new List<Transform>();

    }

    public bool clicked;

    void Update()
    {
        if(triangle.transform.localRotation.eulerAngles != Vector3.zero)
            triangle.transform.localRotation = Quaternion.Euler(Vector3.zero);
        anim.SetBool("Pressed", clicked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            buttonShow.SetActive(true);
            inside.Add(other.gameObject);
            PlayerSystem activePlayerSystem = other.GetComponent<PlayerSystem>();
            //activePlayerSystem.canPressButton = true;
            activePlayerSystem.activeButton = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inside.Remove(other.gameObject);
            if(inside.Count == 0)
            buttonShow.SetActive(false);
            PlayerSystem activePlayerSystem = other.GetComponent<PlayerSystem>();
            activePlayerSystem.CheckButtonInteract(false);
           // activePlayerSystem.canPressButton = false;
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
