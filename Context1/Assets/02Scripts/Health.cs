using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private int maxHealth = 100;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Dead");
            //Destroy(gameObject);
        }
    }


}
