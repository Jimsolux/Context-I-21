using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlight : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private GameObject partnerEye;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Physics.gravity * -0.3f;
    }
    public void SceneSwap()
    {
        try
        {
            Vector3 fixedPos = partnerEye.transform.position;
            fixedPos.z = 0;
            transform.position = fixedPos;
        }
        catch {
            Vector3 fixedPos = transform.position;
            fixedPos.z = 0;
            transform.position = fixedPos;
        }
    }
}
