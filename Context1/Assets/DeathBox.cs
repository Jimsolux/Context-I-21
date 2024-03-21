using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlayerSystem>(out PlayerSystem system);
        if (system != null) { system.Die(); }
    }
}
