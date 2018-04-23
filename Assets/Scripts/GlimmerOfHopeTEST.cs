using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlimmerOfHopeTEST : MonoBehaviour
{
    SpriteRenderer goh;
    private bool cooldown = false;
    private int gohCool = 5;
    private int gohStart = 5;

    // Use this for initialization
    void Start()
    {
        goh = this.gameObject.GetComponent<SpriteRenderer>();
        goh.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown)
        {
            if (gohStart + gohCool < (int)Time.time)
            {
                cooldown = false;
            }
        }
        // Input Check
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!cooldown)
            {
                goh.enabled = true;  // player can now see what is missing. 
                gohStart = (int)Time.time;
                cooldown = true;
            }
           
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            goh.enabled = false;
        }
        
    }
}
