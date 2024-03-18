using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    private GameObject[] players;

    [SerializeField] private GameObject partnerBalloon;

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        float minDistance = float.MaxValue;
        try
        {
            foreach (GameObject p in players)
            {
                float curDistance = Vector3.Distance(transform.position, p.transform.position);
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    target = p.transform;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);

            transform.LookAt(target.position + new Vector3(0, -90, 0));
        }
        catch { }
    }

    public void SceneSwap()
    {
        transform.position = partnerBalloon.transform.position;
    }

    public void Die()
    {

    }
}
