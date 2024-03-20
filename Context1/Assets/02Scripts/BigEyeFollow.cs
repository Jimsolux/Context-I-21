using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyeFollow : MonoBehaviour
{
    Transform target;
    void Start()
    {
        InvokeRepeating("ChangeTarget", 1, 1);
    }

    private void Update()
    {
        transform.LookAt(target);
    }


    private void ChangeTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length > 0 )
        {
            target = players[Random.Range(0, players.Length)].transform;
        }    
    }
}
