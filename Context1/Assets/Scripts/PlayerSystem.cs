using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private float walkSpeed;

    private void Update()
    {
        UpdatePosition();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void UpdatePosition()
    {
        Vector3 v3Dir = direction;
        transform.position += v3Dir * walkSpeed * Time.deltaTime;
    }
}
