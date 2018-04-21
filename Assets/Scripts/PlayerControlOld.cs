using UnityEngine;
using System.Collections;

public class PlayerControlOld:MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed  = 5f;		    // The fastest the player can travel in the x axis.
	public float dashForce = 20000f;        // Amount of force added to move the player left and right on a dash

	/*TODO: Yet to be implemented properly... Or do we want infinite dash?  
	private Time dashCooldown;              // Time between dashes 
	*/

//	private bool dashing   = false;           // Is the player dashing?
//	public AudioClip[] jumpClips;			  // Array of clips for when the player jumps.
	public float jumpForce = 2000f;			  // Amount of force added when the player
	public int jumpCount   = 0;               // Counter for the double jump
	public bool grounded   = false;           // Check if player has hit the ground (or blocks)
    
	private Rigidbody2D rb;                   // Will be a reference to the rigidbody of the player
	private float h;                          // Will be a reference to the horizontal input 
	private Animator myAnimator;              // reference to the animations 
	private bool isWalking = false;
	private bool jump = false;				 // Condition for whether the player should jump.



	void Awake()
	{
		// Setting up references.
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
	}

	void Update(){
		// Check if space (jump) has been pressed 
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true;   // change jump to true for if statement in the FixedUpdate
			jumpCount++;   // add to jumpCount so we can tell if player jumped more thance twice
		}

		// Check if Tab (dash) has been pressed
//		if (Input.GetKeyDown (KeyCode.Tab)) {
//			dashing = true;  // player is now dashing
//		}

		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		if (h != 0) {
			isWalking = true;
		} else {
			isWalking = false;
		}
		// We only want to carry out this section of code if player is not dashing
//		if (dashing == false) {
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			//			anim.SetFloat ("Speed", Mathf.Abs (h));

			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed) {
				// ... add a force to the player.
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);
				myAnimator.SetBool ("walking", isWalking);
			}

			// If the player's horizontal velocity is greater than the maxSpeed...
			if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed) {
				// ... set the player's velocity to the maxSpeed in the x axis.
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}
			if (Input.GetKey (KeyCode.S)) {
				GetComponent<Rigidbody2D> ().AddForce (Vector2.down * moveForce);
			}
//		}
	}

	// Check if player has collided with the ground objects
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
			myAnimator.SetBool("jumping", false);
			if (jumpCount > 0)
				jumpCount = 0;
		}

	}

	// Check if the player is no longer colliding with ground objects 
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = false;
		}
	}

	void FixedUpdate ()
	{
		
//		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");


		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !facingRight) {
			// ... flip the player.
			Flip ();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight) {
			// ... flip the player.
			Flip ();
		}


		// If the player should jump...
		if(jump)
		{
			if (jumpCount <= 2) {
				// Add a vertical force to the player.
				rb.AddForce (Vector2.up * jumpForce);
				myAnimator.SetBool("jumping", true);
			} else if (jumpCount > 2 && grounded) {
				jumpCount = 0;
			}
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
//		if(dashing){
//			// Add dash force to the player
//			rb.AddForce (Vector2.right  * dashForce);
//			// Make sure the player can't dash again until the dash conditions from Update are satisfied.
//			dashing = false;
//		}
	}
	

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
