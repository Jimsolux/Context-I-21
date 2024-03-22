using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCheck : MonoBehaviour
{
    GameObject curFloor = null;
    public bool value;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            curFloor = other.gameObject;
            value = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(curFloor != null)
        {
            if(other.gameObject == curFloor)
            {
                value = false;
            }
        }
    }
}
