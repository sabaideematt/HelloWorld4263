using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberController : MonoBehaviour {
	public Text text;
	ButtonController button;

	void Start ()
	{
		// Set up the reference.
		text   = GetComponentInChildren<Text> ();
		button = GetComponent<ButtonController> ();
		text.text = "" + button.number;
	}

	void Update ()
	{
		text.text = "" + button.number;
	}
}
