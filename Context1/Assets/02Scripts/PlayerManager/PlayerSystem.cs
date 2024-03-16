using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{

    // General stuff
    [SerializeField] private Transform targetCamera;
    private PlayerRole role;
    private int ID;
    [SerializeField] private GameObject colliderArtist;
    [SerializeField] private GameObject colliderDeveloper;
    [SerializeField] private GameObject colliderDesigner;

    // physics stuff
    private Rigidbody rb;
    [SerializeField] private float rotateSpeed = 200;
    //ButtonInteraction
    Collider[] nearbyButtonColliders;
    [SerializeField] private float interactSphereRadius = 100;
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask buttonLayer;
    GameObject Buttons;
    ButtonController buttonController;
    //ButtonText
    /*
    [SerializeField] private TextMeshProUGUI textBox;
    private string displayText;
    [SerializeField] private Vector3 textOffSet;*/

    // Mechanics stuff
    // Walking
    private Vector2 direction;

    // Jumping
    private bool jumping;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 0.51f;
    [SerializeField] private float coyoteTime = 0.1f; // The time the player can still jump after they are no longer grounded
    private float currentCoyoteTime = 0f;
    private bool onCoyoteTime = false;

    private void Start()
    {
        targetCamera.parent = null;
        GameManager.instance.AddPlayer(this);
        rb = GetComponent<Rigidbody>();
    }

    public void Setup(PlayerRole myRole, int myID)
    {

        Debug.Log("New player connected; " + role + ", with ID " + ID);
        role = myRole;
        ID = myID;
        if (GetComponent<PlayerRoleOverwrite>() != null)
        {
            role = GetComponent<PlayerRoleOverwrite>().role;
        }

        try
        {
            switch (role)
            {
                case PlayerRole.Artist:
                    Instantiate(colliderArtist, parent: transform);
                    break;
                case PlayerRole.Designer:
                    Instantiate(colliderDesigner, parent: transform);
                    break;
                case PlayerRole.Developer:
                    Instantiate(colliderDeveloper, parent: transform);
                    break;
            }
        }
        catch
        {
            Debug.LogWarning("No collider object found, my role is " + role);
        }

        Buttons = GameObject.Find("Buttons");
        if (Buttons != null)
        {
            buttonController = Buttons.GetComponent<ButtonController>();
            ReadButtonsSphere(); // Reads all the buttons in the level
        }

    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        UpdatePosition(); // this updates player position according to movement input

        RotatePlayer();

        CoyoteTime();
    }

    #region movement
    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void UpdatePosition()
    {
        if (GameManager.instance.sidewaysControls)
        {
            float dirY = direction.y;
            Vector3 v3Dir = new(0, dirY, 0);
            transform.position += v3Dir * Variables.GetPlayerSpeed() * Time.deltaTime;
        }
        else
        {
            float dirX = direction.x;
            Vector3 v3Dir = new(dirX, 0, 0);
            transform.position += v3Dir * Variables.GetPlayerSpeed() * Time.deltaTime;
        }
    }
    #endregion

    private void RotatePlayer()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, GameManager.instance.targetRotation, Time.deltaTime * rotateSpeed);
    }


    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            switch (GameManager.instance.desAbilities)
            {
                case DesAbilitiesEnum.Jump:
                    Jump();
                    break;
                case DesAbilitiesEnum.Attack:
                    Attack();
                    break;
                    //case DesAbilitiesEnum.Interact:
                    //    DesInteract();
                    //    break;
            }
        }
    }

    public void NextAbility(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            GameManager.instance.ChangeAbility(role, 1);
        }
    }
    public void PreviousAbility(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            GameManager.instance.ChangeAbility(role, -1);
        }
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            GameManager.instance.UseAbility(role);
        }
    }

    #region jumping
    private void Jump()
    {
        if (onCoyoteTime && !jumping)
        {
            rb.AddForce(transform.up * Variables.GetJumpHeight(), ForceMode.Impulse);
            currentCoyoteTime = 0;
            onCoyoteTime = false;
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
        if (Physics.Raycast(transform.position, transform.up * -1, groundDistance, groundMask))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region other des Abilities
    private void Attack()
    {

    }

    #endregion


    #region InteractSphere
    public void ReadButtonsSphere()
    {
        Collider[] nearbyButtonColliders = Physics.OverlapSphere(transform.position, interactSphereRadius, buttonLayer);

    }

    public void CalculateButtonDistance()
    {
        foreach (Collider collider in nearbyButtonColliders)
        {
            float thisDistance = Vector3.Distance(transform.position, collider.transform.position);
            if (thisDistance < interactDistance)
            {
                ShowInteractUI();
            }
            else { buttonController.UpdateButtons(false); } // No currentPress of this player

        }
    }

    private void ShowInteractUI()
    {
        /*
        //Textstuff
        textBox.text = buttonController.buttonsCurrentlyPressed + " / 3";
        textBox.color = Color.white;
        textBox.fontSize = 18;
        */


        //E to interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            buttonController.UpdateButtons(IsGrounded());
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            buttonController.UpdateButtons(false);
        }
    }

    #endregion
}
