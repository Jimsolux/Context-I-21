using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    //public Animator animator;

    //Player stats
    private float playerSpeed;
    public float maxVelocity = 5;
    private float jumpForce;
    private Rigidbody rb;
    private float moveVertical; 
    private float moveHorizontal;
    public Transform groundChecker;
    //flip
    bool facingRight = true;

    [Header("Debug")]
    [SerializeField] private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // animator = GetComponent<Animator>();
        playerSpeed = GameManager.instance.playerSpeed;
        jumpForce = GameManager.instance.jumpHeight;
        maxVelocity = GameManager.instance.playerSpeed;
    }
    private void FixedUpdate()  // Runs before update.
    {
        isGrounded = IsGrounded();  // Uses function to determine if the player is on the floor.
        
    }
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");

        // rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0 );   // Determine velocity and apply to playerObject.
        var locVel = transform.InverseTransformDirection(rb.velocity);

        locVel.x = moveHorizontal * playerSpeed;
        //locVel.y = rb.velocity.y;
        rb.velocity = transform.TransformDirection(locVel);

    /*
       if (Mathf.Abs(locVel.x) < maxVelocity)
        {
            rb.AddForce(transform.right * acceleration * moveHorizontal);
        }
    */
        

        //rb.velocity = Mathf.Clamp(rb.velocity, 0, 3);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)  //if SPACE & player is on ground, makes player jump
        {
            rb.AddForce(gameObject.transform.up * jumpForce, ForceMode.Impulse);
        }
        /*
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
        //Flip animation
        if (moveHorizontal > 0 && facingRight == false)
        {
            Flip();
        }
        if (moveHorizontal < 0 && facingRight == true)
        {
            Flip();
        }
         */
        //Animation automation
        //animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }


    private bool IsGrounded()
    {
        //Debug.DrawRay(groundChecker.position, -Vector3.up * 0.3f , Color.red, 3.0f);   // Visiualizing raycast
        return Physics.Raycast(groundChecker.position, -gameObject.transform.up, 0.3f);    // Raycast down to check if collision is made.
    }


    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

}

