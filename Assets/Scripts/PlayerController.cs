using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController:MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // Directional Facing
    [HideInInspector]
    public bool jump = false;               // Jump Input
    private bool dashing = false;           // Dash Input
    private float horizontalInput = 0;      // Horizontal Input Vectors

    // Gameplay Ability Flags
    public bool hasDoubleJump = false;
    public bool hasDash = false;
    public bool hasGlimmer = false;

    // Gameplay Physics Variables
    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 6f;             // The fastest the player can travel in the x axis.
    public float dashForce = 20000f;        // Amount of force added to move the player left and right on a dash
    public float jumpForce = 2000f;         // Amount of force added when the player

    // Player Conditionals
    public int jumpCount = 0;               // Counter for the double jump
    private LayerMask triggerMask;          // Mask for Interactable Raycast Hitscan
    public bool grounded = false;           // Check if player has hit the ground (or blocks)
    private bool isWalking = false;         // Check if walking (for animations)
    private bool onCooldown = false;        // If dash is on cooldown
        private int dashCooldown = 3;       // Length of Dash cooldown in seconds
        private int dashStartTime = 0;      // Cooldown time tracking

    // Animation Variables
    private Animator myAnimator;            // reference to the animations 
    private Rigidbody2D rb;                 // RigidBody on Player
    
    // Player Collection Tracking
    public int numPawPoints = 0;            // Track number of Paw Points collected
    public int stressLevel = 0;             // Current Stress Level
    public GameObject heldObject;           // For Tracking Puzzle 1 Objects

    // UI Elements
    private GameObject canvas;              // Main Canvas
    private Text PawPointText;              // PawPoints Indicator
    private Text StressText;                // Stress Indicator
    private HelloCamera playerCam;               // Camera Object (For Stress Filter)


    void Awake()
    {
        // Setting up references.
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();  // No animations yet so ignore

        // Create LayerMask for Raycast Hit Detection
        triggerMask = LayerMask.GetMask("Interactable");

        // Get UI Canvas Reference
        canvas = GameObject.Find("UICanvas");
        PawPointText = canvas.gameObject.transform.Find("PawPointText").GetComponent<Text>();
        StressText = canvas.gameObject.transform.Find("StressText").GetComponent<Text>();
        playerCam = this.gameObject.transform.Find("Main Camera").gameObject.GetComponent<HelloCamera>();
    }

    void Update()
    {
        // Update UI Elements
        PawPointText.text = "Paw Points: " + numPawPoints;
        StressText.text = "Stress: " + stressLevel;
        playerCam.filterIntensity = stressLevel;

        // Jump Checking 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;   // change jump to true for if statement in the FixedUpdate
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2.0f, triggerMask);

            // Hit Detected
            if (hit.collider)
            {
                GameObject target = hit.collider.gameObject;

                canvas.transform.Find("InteractionText").GetComponent<Text>().text = "Press F to Interact";

                // Do We Want To Interact?
                if (Input.GetKeyDown(KeyCode.F))
                {
                    target.GetComponent<TriggerController>().interact(this.gameObject);
                }
            }
            // No Hit
            else
            {
                canvas.transform.Find("InteractionText").GetComponent<Text>().text = "";
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
        // Check if grounded
        if (grounded)
        {
            jumpCount = 0;
        }
        // Double Jump Section
        if (jump && hasDoubleJump)
        {
            if (jumpCount <= 1)
            {
                // Force Not Grounded
                grounded = false;
                // Increment Counter upon successful jump
                jumpCount++;
                // Add a vertical force to the player.
                rb.AddForce(Vector2.up * jumpForce);
                // Play Jump Animation
                myAnimator.SetBool("jumping", true);
            }
            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
        // Single Jump Section
        else if (jump)
        {
            if (jumpCount < 1)
            {
                // Force Not Grounded
                grounded = false;
                // Increment Counter upon successful jump
                jumpCount++;
                // Add a vertical force to the player.
                rb.AddForce(Vector2.up * jumpForce);
                // Play Jump Animation
                myAnimator.SetBool("jumping", true);
            }
            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }

        // Dashing Forces
        if (dashing && hasDash)
        {
            // Add dash force to the player
            rb.AddForce(Vector2.right * horizontalInput * dashForce);
            // Make sure the player can't dash again until the dash conditions from Update are satisfied.
            dashing = false;
        }
        else if (dashing)
        {
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

    // Check if player has collided with the ground objects
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            grounded = true;
            myAnimator.SetBool("jumping", false);
        }

    }

    // Check if the player is no longer colliding with ground objects 
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            grounded = false;
        }
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
