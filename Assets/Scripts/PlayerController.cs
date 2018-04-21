using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController:MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.
    private bool dashing = false;           // Is the player dashing?

    private float horizontalInput = 0;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 6f;             // The fastest the player can travel in the x axis.
    public float dashForce = 20000f;        // Amount of force added to move the player left and right on a dash
    public float jumpForce = 2000f;           // Amount of force added when the player

    public int jumpCount = 0;               // Counter for the double jump
    public bool grounded = false;           // Check if player has hit the ground (or blocks)
    private bool isWalking = false;         // Check if walking (for animations)

    private Animator myAnimator;              // reference to the animations 
    private Rigidbody2D rb;                   // RigidBody on Player

    // DAF Added
    public int dashCooldown = 3; // Length of Dash cooldown in seconds
    private bool onCooldown = false; // If dash is on cooldown
    private int dashStartTime = 0; // Cooldown time tracking

    private LayerMask triggerMask;
    private GameObject canvas;


    void Awake()
    {
        // Setting up references.
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();  // No animations yet so ignore

        // Create LayerMask for Raycast Hit Detection
        triggerMask = LayerMask.GetMask("Triggers");

        // Get UI Canvas Reference
        canvas = GameObject.Find("UICanvas");
    }

    void Update()
    {
        // Jump Checking 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;   // change jump to true for if statement in the FixedUpdate
            jumpCount++;   // add to jumpCount so we can tell if player jumped more thance twice
        }

        // Dashing Checks
            // Cooldown Check
            if (onCooldown)
            {
                if (dashStartTime + dashCooldown < (int)Time.time)
                {
                    onCooldown = false;
                }
            }
            // Input Check
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!onCooldown)
                {
                    dashing = true;  // player is now dashing
                    dashStartTime = (int)Time.time;
                    onCooldown = true;
                }
            }

        // Interaction Section
        //  Check for Interactables in Facing Direction
        Vector2 direction;
            if (facingRight)
                direction = Vector2.right;
            else
                direction = Vector2.left;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 20.0f, triggerMask);

            // Hit Detected
            if (hit.collider)
            {
                GameObject target = hit.collider.gameObject;

                canvas.transform.Find("InteractionText").GetComponent<Text>().text = "Press F to Interact";

                // Do We Want To Interact?
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (target.tag == "DesatTrigger")
                    {
                        target.GetComponent<TriggerController>().Toggle();
                    }
                    else if (target.tag == "SemiTrigger")
                    {
                        Debug.Log("Entered Outline Section");
                        target.gameObject.SetActive(false);
                    }
                }
            }
            // No Hit
            else
            {
                canvas.transform.Find("InteractionText").GetComponent<Text>().text = "";
            }

    }

    // Check if player has collided with the ground objects
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            grounded = true;
            myAnimator.SetBool("jumping", false);
            if (jumpCount > 0)
                jumpCount = 0;
        }

    }

    // Check if the player is no longer colliding with ground objects 
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            grounded = false;
            myAnimator.SetBool("jumping", true);
        }
    }

    void FixedUpdate()
    {

        // Cache the horizontal input.
        horizontalInput = Input.GetAxis("Horizontal");

        // We only want to carry out this section of code if player is not dashing
        if (dashing == false)
        {
            // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            if (horizontalInput * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
                // ... add a force to the player.
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalInput * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }


        // If the input is moving the player right and the player is facing left...
        if (horizontalInput > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalInput < 0 && facingRight)
            // ... flip the player.
            Flip();


        // Jumping Forces
        if (jump)
        {
            if (jumpCount <= 2)
            {
                // Add a vertical force to the player.
                rb.AddForce(Vector2.up * jumpForce);
            }
            else if (jumpCount > 2 && grounded)
            {
                jumpCount = 0;
            }
            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }

        // Dashing Forces
        if (dashing)
        {
            // Add dash force to the player
            rb.AddForce(Vector2.right * horizontalInput * dashForce);
            // Make sure the player can't dash again until the dash conditions from Update are satisfied.
            dashing = false;
        }

        // Animation Updates
            // Walking Check
            if (horizontalInput != 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
            myAnimator.SetBool("walking", isWalking);
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
