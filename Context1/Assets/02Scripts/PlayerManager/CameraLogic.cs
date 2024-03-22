using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float chaseSpeed = 5;
    Quaternion baseRot;

    private void Awake()
    {
        baseRot = transform.rotation;
        target = transform.parent;
        SceneSwitchDebug.instance.AddCam(transform.GetComponent<Camera>());
    }

    void Update()
    {
        transform.rotation = baseRot;
        if(target != null)
        {
            Vector3 targetPos = new(target.position.x, target.position.y + 0.5f, transform.position.z); // keep own z offset cuz 2D camera logic

            transform.position = Vector3.MoveTowards(transform.position, targetPos, chaseSpeed);
        }
    }
}
