using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlight : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Physics.gravity * -0.3f;
    }
}
