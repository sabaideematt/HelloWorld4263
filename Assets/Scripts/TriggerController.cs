using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerController : MonoBehaviour
{

    // Trigger Variables
        // Level 1
        private int FinishStatementNum = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Triggers on Player Entry, check by Tag
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(this.gameObject.tag == "PawPoint")
            {
                this.gameObject.SetActive(false);
                other.gameObject.GetComponent<PlayerController>().numPawPoints++;
            }
        }
    }

    // Called when Player uses Interact button
    public void interact(GameObject player)
    {
        if(this.gameObject.tag == "WallTrigger")
        {
            gameObject.transform.Find("Wall").gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if(this.gameObject.tag == "Puzzle1")
        {
            GameObject current = player.GetComponent<PlayerController>().heldObject;
            this.gameObject.SetActive(false);
            player.GetComponent<PlayerController>().heldObject = this.gameObject;
            current.SetActive(true);
        }

        if(this.gameObject.tag == "Puzzle1Finish")
        {
            GameObject current = player.GetComponent<PlayerController>().heldObject;
            
            if(current.name == "SemiColon")
            {
                // Display 'Correct' message, deactivate Wall, add to Stress level
                this.gameObject.transform.Find("Puzzle1Canvas").Find("ErrorVictory").GetComponent<Text>().text = "Correct!";
                this.gameObject.transform.Find("PuzzleWall1").gameObject.SetActive(false);
                player.GetComponent<PlayerController>().stressLevel += 25;
            }
            else
            {
                // Display 'Incorrect' message, clear HeldObject and reactive it in World.
                current.SetActive(true);
                player.GetComponent<PlayerController>().heldObject = null;
                this.gameObject.transform.Find("Puzzle1Canvas").Find("ErrorVictory").GetComponent<Text>().text = "Incorrect Solution.";
            }
        }

        if(this.gameObject.tag == "MikeTip")
        {
            GameObject canvas = this.gameObject.transform.Find("TipCanvas").gameObject;

            if(player.GetComponent<PlayerController>().numPawPoints > 4)
            {
                // Make Mike Not Interactable (Prevent multiple payments)
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                // Reduce Paw Points
                player.GetComponent<PlayerController>().numPawPoints -= 5;
                // Deactivate Other Statements
                canvas.gameObject.transform.Find("TipMessage").gameObject.SetActive(false);
                canvas.gameObject.transform.Find("TipInsufficient").gameObject.SetActive(false);
                // Activate Solution Statement
                canvas.gameObject.transform.Find("TipSolution").gameObject.SetActive(true);
            }
            else
            {
                // Deactivate Other Statements
                canvas.gameObject.transform.Find("TipMessage").gameObject.SetActive(false);
                canvas.gameObject.transform.Find("TipSolution").gameObject.SetActive(false);
                // Activate Solution Statement
                canvas.gameObject.transform.Find("TipInsufficient").gameObject.SetActive(true);
            }
        }

        if(this.gameObject.tag == "MikeFinish")
        {
            GameObject canvas = this.gameObject.transform.Find("FinishCanvas").gameObject;

            if(FinishStatementNum == 0)
            {
                // Display Next Message
                canvas.transform.Find("FinishMessage1").gameObject.SetActive(true);
                // Hide This Message
                canvas.transform.Find("FinishMessage0").gameObject.SetActive(false);
            }
            else if(FinishStatementNum == 1)
            {
                // Display Next Message
                canvas.transform.Find("FinishMessage2").gameObject.SetActive(true);
                // Hide This Message
                canvas.transform.Find("FinishMessage1").gameObject.SetActive(false);
            }
            else if(FinishStatementNum == 2)
            {
                // Display Next Message
                canvas.transform.Find("FinishMessage3").gameObject.SetActive(true);
                // Hide This Message
                canvas.transform.Find("FinishMessage2").gameObject.SetActive(false);
            }
            else if(FinishStatementNum == 3)
            {
                // Load Next Level via Scene Manager
                Debug.Log("Next Level Loading.");
            }

            FinishStatementNum++;
        }
        if (this.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Final")
            {
                if (Input.GetKeyDown(KeyCode.F)) //press f to interact with the boxes that are left.
                {
                    numElements++;
                    Destroy(gameObject);
                }
            }
           
        }
        if(numElements == 3)
        {
            Debug.Log("Next Lebel Loading."); //this is filler, you end it how you wanna after the player gets all 4 of the right elements. 
        }
    }
    }
}
