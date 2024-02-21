using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperAbilities : MonoBehaviour
{

    int gravityDirection = 0;
    float gravity = 9.81f;
    float rot = 0;
    public enum AbilitiesEnum
    {
        gravitySwap,

    }


    void Start()
    {
        
    }


    void Update()
    {
        Debug.Log(Physics.gravity);
        SwapGravity();
    }


        Vector3    up = new Vector3(0,   9.81f, 0);
        Vector3 right = new Vector3(9.81f,  0,    0);
        Vector3  down = new Vector3(0,  -9.81f, 0);
        Vector3  left = new Vector3(-9.81f, 0,    0);
    public void SwapGravity()
    {

        //float gravity;
        //Vector3 test =new Vector3(0, -1.0f, 0)

        switch(gravityDirection)
        {
            case 0: Physics.gravity = down; break;
            case 1: Physics.gravity = left; break;
            case 2: Physics.gravity = up; break;
            case 3: Physics.gravity = right; break;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            gravityDirection++;

            rot -= 90;

            gameObject.transform.rotation = Quaternion.Euler(0, 0, rot);
        }

        gravityDirection %= 4;
    }

}
