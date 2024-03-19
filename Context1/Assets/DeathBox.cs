using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerSystem>().Die();
    }
}
