using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCubeAnimationSpeed : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().speed = Random.Range(0.5f, 2f);
    }

}
