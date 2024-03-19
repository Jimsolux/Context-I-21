using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Debug.Log(rb.velocity);
            StartCoroutine(HandleAnim());

            float fallDir = 0;

            switch (GameManager.instance.gravityDirection)
            {
                case GravityDirectionEnum.Down:
                    fallDir = rb.velocity.y;
                    break;
                case GravityDirectionEnum.Up:
                    fallDir = -rb.velocity.y;
                    break;
                case GravityDirectionEnum.Left:
                    fallDir = rb.velocity.x;
                    break;
                case GravityDirectionEnum.Right:
                    fallDir = -rb.velocity.x;
                    break;
            }

            if(fallDir < 0)
            {
                float fallSpeed = Mathf.Abs(fallDir);
                fallSpeed *= 1.5f;
                fallSpeed = Mathf.Clamp(fallSpeed, 5, 10);

                rb.velocity = other.transform.up * fallSpeed;
            }
        }
    }

    IEnumerator HandleAnim()
    {

        GetComponent<Animator>().SetBool("Boing", true);
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("Boing", false);
    }
}
