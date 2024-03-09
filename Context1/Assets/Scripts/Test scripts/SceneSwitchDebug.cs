using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchDebug : MonoBehaviour
{
    // let niet op de namen van de variables, dit is letterlijk gwn voor testen

    public GameObject cuteUwU;
    public GameObject spoopyOwO;

    bool activeCute;
    void Update()
    {
        if(activeCute)
        {
            cuteUwU.SetActive(true);
            spoopyOwO.SetActive(false);
        }
        else
        {
            cuteUwU.SetActive(false);
            spoopyOwO.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeCute = !activeCute;
        }
    }
}
