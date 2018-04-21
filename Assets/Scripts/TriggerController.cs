using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class TriggerController : MonoBehaviour
{

    private bool activeToggle = false;
    private HelloCamera camScript;

    // Use this for initialization
    void Start()
    {
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HelloCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "DesatTrigger")
            {
                Debug.Log("Player entered Desaturation Zone");
                camScript.filterIntensity = 0.5f;
            }
            else if (this.gameObject.tag == "SatTrigger")
            {
                Debug.Log("Player entered Saturation Zone");
                camScript.filterIntensity = 0.0f;
            }
        }
    }

    public void Toggle()
    {
        if(!activeToggle)
        {
            camScript.filterIntensity = 0.5f;
        }
        else
        {
            camScript.filterIntensity = 0.0f;
        }

        activeToggle = !activeToggle;
        
    }
}
