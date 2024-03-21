using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    private GameObject[] players;

    [SerializeField] private GameObject partnerBalloon;
    [SerializeField] private Transform rotatePivot;
    [SerializeField] private Transform model;
    private AIDestinationSetter destinationTarget;

    private void Start()
    {
        destinationTarget = GetComponent<AIDestinationSetter>();
    }

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

            //transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);

            if (destinationTarget != null)
            {
                destinationTarget.target = target;

                rotatePivot.LookAt(destinationTarget.target);
            }
            else
            {
                rotatePivot.LookAt(target);
            }

            if(Vector3.Distance(transform.position, target.position) < 1)
            {
                target.GetComponent<PlayerSystem>().Die();
            }
            Debug.Log(Vector3.Distance(transform.position, target.position));

            Vector3 rotationV3 = rotatePivot.rotation.eulerAngles;
            rotationV3.x += 90;
            Debug.DrawRay(rotatePivot.position, rotatePivot.forward * 10, Color.red);
            model.rotation = Quaternion.Euler(rotationV3);
        }
        catch { }
    }

    public void SceneSwap()
    {
        Vector3 fixedPos = partnerBalloon.transform.position;
        fixedPos.z = 0;
        transform.position = fixedPos;
    }

    public void Die()
    {
        SceneSwitchDebug.instance.RemoveEye(this);
        SceneSwitchDebug.instance.RemoveBalloon(partnerBalloon.GetComponent<BalloonFlight>());

        Destroy(partnerBalloon);
        Destroy(gameObject);
    }
}
