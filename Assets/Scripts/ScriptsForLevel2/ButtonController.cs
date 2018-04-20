using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
	private Animator myAnimator;  				// reference to the button animation
	GameObject player;             				// Reference to the player GameObject.
	public static bool isPressed = false; 	    // a checker to see if the button is pressed
	public int number;


	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		number = 0;
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// If the entering collider is the player...
		if (other.gameObject == player) {
			// ... Press the button.
			isPressed = true;
			myAnimator.SetBool("pressed", isPressed);
			number++;
		} 
	}

	void OnTriggerExit2D (Collider2D other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == player)
		{
			// ... Reset the button
			isPressed = false;
			myAnimator.SetBool("pressed", isPressed);
		} 
	}

	// Update is called once per frame
	void Update () {
	}
}
