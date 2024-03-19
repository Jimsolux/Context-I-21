using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUpdater : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player")){
            collision.gameObject.TryGetComponent<PlayerSystem>(out PlayerSystem controller);
            controller.UpdateCheckPoint(gameObject.transform);
        }
    }
}
