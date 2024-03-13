using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBotan : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    bool clicked;
    void Update()
    {
        anim.SetBool("Pressed", clicked);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clicked = !clicked;
        }
    }
}
