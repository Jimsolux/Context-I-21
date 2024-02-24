using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private float walkSpeed;
    [SerializeField] private Transform targetCamera;
    private PlayerRole role;
    private int ID;
    private bool doAction;

    private void Start()
    {
        targetCamera.parent = null;
        GameManager.instance.AddPlayer(this);
    }

    public void Setup(PlayerRole myRole, int myID)
    {
        Debug.Log("New player connected; " + role + ", with ID " + ID);
        role = myRole;
        ID = myID;
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        doAction = context.action.WasPressedThisFrame();
    }

    private void UpdatePosition()
    {
        float dirX = direction.x;
        Vector3 v3Dir = new(dirX, 0, 0);
        transform.position += v3Dir * walkSpeed * Time.deltaTime;
    }
}
