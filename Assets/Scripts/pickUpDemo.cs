using System;
using UnityEngine;

public class pickUpDemo
{
    int score = 0;

 void OnCollisionEnter2D (Collision2D collide)
    {
//        if(collide.gameObject.tag.Equals("Player"))
//        {
//            if(gameObject.tag.Equals ("Wrong Token")) //here is where I decided to have the wrong code placed in the world. 
//            {                                         
//            Destroy(gameObject);
//            Application.LoadLevel(Applicaiton.loadedlevel); //needs to be replaced with whatever scene you want to restore. This is just a base case!!
//
//                //here is where I decided to have the wrong code placed in the world. 
//                //If it was picked up by accident, it could lead to resarting the level. It worked the both times I tested it. 
//
//            }
//            else if(gameObject.tag.Equals ("Correct Token"))
//            {
//                Destroy(gameObject);
//                //This just deletes the object as a whole and doesn't reset the scene
//                //In the future, we could have it be stored somewhere and then displayed.
//                //Right now the pick up is just being destroyed but nothing is being done with it besides leaving the game environment.
//            }
//
//            else if(gameObject.tag.Equals("Paw Point"))
//            {
//                Destroy(gameObject);
//                score++;
//                Display(score);
//                //This collects the Paw Point and then displays it to the screen somewhere.
//                //If this is the route we are going, I think this is how we could do it.
//                
//            }
//            else
//            {
//                Destroy(gameObject);
//                //Just an ending base case to decide what we want to do later
//                //In due time... We could do something like health?? Other pickups
//                //could also be as feasible if we could think of some for later levels.
//            }
//            
//        }
//    }
	}
}
