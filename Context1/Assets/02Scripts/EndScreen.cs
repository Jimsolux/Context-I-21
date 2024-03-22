using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private bool delay = true;
    void Start()
    {
        Invoke("StopDelay", 0.1f);
    }

    void StopDelay()
    {
        delay = false;
    }

    void Update()
    {
        if (!delay)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }   
    }
}
