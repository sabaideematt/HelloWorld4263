using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPawPoint : MonoBehaviour {

    private GameObject canvas;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("ParadeCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collision with Player
        if (other.gameObject.tag == "Player")
        {
            canvas.transform.Find("Message1").gameObject.SetActive(false);
            canvas.transform.Find("Message2").gameObject.SetActive(true);
        }
    }
}
