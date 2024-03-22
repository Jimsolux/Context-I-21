using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{

    // General stuff
    [SerializeField] private Transform targetCamera;
    private PlayerRole role;
    private int ID;
    [SerializeField] private CapsuleCollider colliderArtist;
    [SerializeField] private CapsuleCollider colliderDeveloper;
    [SerializeField] private CapsuleCollider colliderDesigner;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool canUseAbility = true;
    [SerializeField] private float abilityCooldown = 0.2f;

    // physics stuff
    private Rigidbody rb;
    [SerializeField] private float rotateSpeed = 200;
    private bool useGravity = true;
    private bool freezeStickiness = false;
    private bool slidy = false;
    private bool ballpit = false;

    //ButtonInteraction
    Collider[] nearbyButtonColliders;
    [SerializeField] private float interactSphereRadius = 100;
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask buttonLayer;
    GameObject Buttons;
    ButtonController buttonController;

    //Checkpoint
    public Transform lastCheckPoint;    //last collided checkpoint.
    //Attack
    [SerializeField] private GameObject AttackCloudPrefab;

    // Mechanics stuff
    // Walking
    private Vector2 direction;
    private TubeTransparency activeTube; // gets the tube the player is currently in

    // Jumping
    private bool jumping;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f; // The time the player can still jump after they are no longer grounded
    private float currentCoyoteTime = 0f;
    private bool onCoyoteTime = false;

    private void Start()
    {
        targetCamera.parent = null;
        GameManager.instance.AddPlayer(this);
        buttonController = ButtonController.instance;
    }

    private bool instantiated = false;
    public void Setup(PlayerRole myRole, int myID)
    {
        role = myRole;
        ID = myID;
        if (GetComponent<PlayerRoleOverwrite>() != null)
        {
            role = GetComponent<PlayerRoleOverwrite>().role;
        }

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        float blend = 0;

        switch (role)
        {
            case PlayerRole.Artist:
                blend = 1;
                break;
            case PlayerRole.Developer:
                blend = 0.5f;
                break;
            case PlayerRole.Designer:
                blend = 0;
                groundDistance = 0.2f;
                break;
        }

        animator.SetFloat("Blend", blend);

        try
        {
            switch (role)
            {
                case PlayerRole.Artist:
                    colliderArtist.enabled = true;
                    colliderDesigner.enabled = false;
                    colliderDeveloper.enabled = false;
                    break;
                case PlayerRole.Designer:
                    colliderArtist.enabled = false;
                    colliderDesigner.enabled = true;
                    colliderDeveloper.enabled = false;
                    break;
                case PlayerRole.Developer:
                    colliderArtist.enabled = false;
                    colliderDesigner.enabled = false;
                    colliderDeveloper.enabled = true;
                    break;
            }
        }
        catch
        {
            Debug.LogWarning("No collider object found, my role is " + role);
        }

        lastCheckPoint = GameObject.FindGameObjectWithTag("StartCheckPoint").transform;// Sets initial checkpoint
        UserInterfaceManager.instance.UpdateUI();
        instantiated = true;
    }

    private void FixedUpdate()
    {
        rb.useGravity = useGravity;
    }

    private void Update()
    {
        UpdatePosition(); // this updates player position according to movement input

        if (useGravity)
        {
            RotatePlayer();
        }

        CheckSpecialState();
        CoyoteTime();
    }



    #region movement
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (instantiated)
        {
            direction = context.ReadValue<Vector2>();
        }
        if (animator != null)
        {
            if (direction == Vector2.zero)
                animator.SetBool("Walking", false);
            else
            {
                if (direction.x < 0)
                {
                    if (!spriteRenderer.flipX)
                        spriteRenderer.flipX = true;
                }
                else
                {
                    if (spriteRenderer.flipX)
                        spriteRenderer.flipX = false;
                }
                animator.SetBool("Walking", true);
            }
        }

    }

    bool storedSideways = false;
    Vector3 storedDir = Vector3.zero;
    float iceSpeed = 0;
    private void UpdatePosition()
    {
        bool currentSideways = GameManager.instance.sidewaysControls;

        /// De logica hierachter is miss beetje moeilijk te begrijpen zonder uitleg, dus;
        /// Al is er een actieve tube gebruik je de movement pattern van deze, dus de up, down, left right movement
        /// Al gebruik je geen gravity kijk je naar de *opgeslagen* waarde van de sideways variable. 
        ///     Dit zorgt er voor dat je niet opeens andere movement patroon gaat krijgen ook al is dit voor de speler zelf niet nodig.
        /// Als de speler wel gravity gebruikt, wil je gewoon kijken naar de huidige waarde uit de GameManager, aangezien je dan wilt updaten met de gravity richting   

        float speed = Variables.GetPlayerSpeed();
        if (ballpit) speed *= 0.65f;

        if (!slidy)
            storedDir = Vector3.zero;

        #region ice physics stuff
        if (direction != Vector2.zero)
        {
            iceSpeed = speed;
        }
        else
        {
            iceSpeed -= Time.deltaTime * 6;
            iceSpeed = Mathf.Clamp(iceSpeed, 0, speed);
        }
        #endregion

        if (activeTube != null)
        {
            float dirX = direction.x;
            float dirY = direction.y;
            Vector3 v3Dir = new(dirX, dirY, 0);

            transform.position += v3Dir * speed * Time.deltaTime;
        }
        else if (currentSideways && useGravity || storedSideways && !useGravity)
        {
            storedSideways = true;
            float dirY = direction.y;
            Vector3 v3Dir = new(0, dirY, 0);

            if (slidy && v3Dir != Vector3.zero) storedDir = v3Dir;

            transform.position += v3Dir * speed * Time.deltaTime;
        }
        else
        {
            storedSideways = false;
            float dirX = direction.x;
            Vector3 v3Dir = new(dirX, 0, 0);
            if (slidy && v3Dir != Vector3.zero) storedDir = v3Dir;

            transform.position += v3Dir * speed * Time.deltaTime;
        }
        if (direction == Vector2.zero)
        {
            transform.position += storedDir * iceSpeed * Time.deltaTime;
        }
    }
    #endregion
    #region Secondary Movement
    private void RotatePlayer()
    {
        Vector3 targetEuler = GameManager.instance.targetRotation.eulerAngles;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, GameManager.instance.targetRotation, Time.deltaTime * rotateSpeed);

        // fixt bug met rotaten tegen sticky surface
        if (Vector3.Distance(transform.localRotation.eulerAngles, targetEuler) < 1)
        {
            freezeStickiness = false;
        }
        else
            freezeStickiness = true;
    }

    public void SetActiveTube(TubeTransparency t)
    {
        activeTube = t;
    }

    public void RemoveActiveTube(TubeTransparency t)
    {
        if (activeTube != null)
        {
            if (activeTube == t)
            {
                activeTube = null;
            }
        }
    }
    #endregion
    #region Inputsystem
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            if (instantiated)
            {
                switch (GameManager.instance.desAbilities)
                {
                    case DesAbilitiesEnum.Jump:
                        Jump();
                        break;
                    case DesAbilitiesEnum.Attack:
                        Debug.Log("Switched to Attack");
                        Attack();
                        break;
                        //case DesAbilitiesEnum.Interact:
                        //    DesInteract();
                        //    break;
                }
            }
        }
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            if (instantiated && canUseAbility)
            {
                StartCoroutine(AbilityCooldown());
                StartCoroutine(AnimatorPlayOnce("Special"));
                GameManager.instance.UseAbility(role);
            }
        }
    }
    private IEnumerator AbilityCooldown()
    {
        canUseAbility = false;
        yield return new WaitForSeconds(abilityCooldown);
        canUseAbility = true;
    }

    public void ClickButton(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            if (instantiated)
            {
                CheckButtonInteract(true);
            }
        }
        if (context.action.WasReleasedThisFrame())
        {
            if (instantiated)
            {
                CheckButtonInteract(false);
            }
        }
    }

    #endregion
    #region Checkpoint and death
    public void Die()
    {
        Debug.Log("'insert roblox oof death sound");
        rb.velocity = Vector3.zero;
        transform.position = lastCheckPoint.position;
    }

    public void UpdateCheckPoint(Transform checkpoint)
    {
        lastCheckPoint = checkpoint;
    }


    #endregion
    private void CheckSpecialState()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit, groundDistance, groundMask))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.tag == "StickyStuff")
            {
                useGravity = false;
            }
            else
            {
                useGravity = true;
            }

            if (hit.transform.tag == "GlidyGoo")
            {
                slidy = true;
            }
            else slidy = false;

            if (hit.transform.tag == "Ballpit")
            {
                ballpit = true;
            }
            else ballpit = false;
        }
        else
        {
            if(!IsGrounded())
                useGravity = true;
            slidy = false;
            ballpit = false;
        }

        // wanneer je in een buis zit, wil je geen gravity toepassen (werkt beetje tegen het idee van losse controls ;-;)
        if (activeTube != null)
        {
            useGravity = false;
        }

        // wanneer je de stickiness check freezed, wil je altijd gravity gebruiken
        if (freezeStickiness) useGravity = true;
    }   //Gravity Negation etc.

    private IEnumerator AnimatorPlayOnce(string type)
    {
        try
        {
            animator.SetBool(type, true);
        }
        catch { }
        yield return new WaitForSeconds(0.69f);
        try
        {
            animator.SetBool(type, false);
        }
        catch { }
    }

    #region jumping
    private void Jump()
    {
        if (onCoyoteTime && !jumping && useGravity && GameManager.instance.desAbilities == DesAbilitiesEnum.Jump)
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
        Debug.DrawRay(transform.position, transform.up * -groundDistance, Color.magenta);
        if (Physics.Raycast(transform.position, transform.up * -1, groundDistance, groundMask))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Attack
    private void Attack()
    {
        if (instantiated)
        {
            animator.SetTrigger("Attack");
            if (GameManager.instance.desAbilities == DesAbilitiesEnum.Attack)    //Check if enum is on Attacking
            {
                Vector3 spawnPos = transform.position;
                if (spriteRenderer.flipX)
                {
                    spawnPos.x -= 1.3f;
                    spawnPos.y += 0.65f;
                }
                else
                {
                    spawnPos.x += 1.3f;
                    spawnPos.y += 0.65f;
                }
                Instantiate(AttackCloudPrefab, spawnPos, Quaternion.identity, null);
            }
        }
    }

    #endregion


    #region InteractSphere
    public TestBotan activeButton;

    public void CheckButtonInteract(bool value)
    {
        if (activeButton != null)
        {
            if (value) { activeButton.PressButton(); }
            else { activeButton.UnPressButton(); }

            buttonController.UpdateButtons();
        }
    }

    #endregion

    public PlayerRole GetRole()
    {
        return role;
    }

}
