using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCheck : MonoBehaviour
{
    public bool value;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            value = true;
        }
        else
            value = false;
    }
}
