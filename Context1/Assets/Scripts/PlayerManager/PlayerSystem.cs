using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{

    // General stuff
    [SerializeField] private Transform targetCamera;
    private PlayerRole role;
    private int ID;

    // physics stuff
    private Rigidbody2D rb;

    // Mechanics stuff
    // Walking
    private Vector2 direction;

    // Jumping
    private bool jumping;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 0.6f;
    [SerializeField] private float coyoteTime = 0.1f; // The time the player can still jump after they are no longer grounded
    private float currentCoyoteTime = 0f;
    private bool onCoyoteTime = false;

    private void Start()
    {
        targetCamera.parent = null;
        GameManager.instance.AddPlayer(this);
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(PlayerRole myRole, int myID)
    {
        Debug.Log("New player connected; " + role + ", with ID " + ID);
        role = myRole;
        ID = myID;
    }

    private void Update()
    {
        UpdatePosition(); // this updates player position according to movement input

        CoyoteTime();
    }

    #region movement
    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void UpdatePosition()
    {
        float dirX = direction.x;
        Vector3 v3Dir = new(dirX, 0, 0);
        transform.position += v3Dir * Variables.GetPlayerSpeed() * Time.deltaTime;
    }
    #endregion

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            switch (Variables.GetActiveAction())
            {
                case PlayerAction.Jump:
                    Jump();
                    break;
            }
        }
    }

    #region jumping
    private void Jump()
    {
        if (onCoyoteTime && !jumping)
        {
            rb.AddForce(transform.up * Variables.GetJumpHeight(), ForceMode2D.Impulse);
            currentCoyoteTime = 0;
            jumping = true;
        }
    }

    private void CoyoteTime()
    {
        if (IsGrounded())
        {
            jumping = false;
            currentCoyoteTime = coyoteTime;
        }
        else
        {
            currentCoyoteTime -= Time.deltaTime;
        }
        if (currentCoyoteTime > 0) onCoyoteTime = true;
        else onCoyoteTime = false;
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, transform.up * -1, groundDistance, groundMask))
        {
            return true;
        }
        return false;
    }
    #endregion
}
