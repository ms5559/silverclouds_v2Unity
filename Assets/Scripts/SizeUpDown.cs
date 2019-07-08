using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUpDown : MonoBehaviour {

	  	     // Grow parameters
     public float approachSpeed = 0.02f;
 
     // And something to do the manipulating
     private Coroutine routine;
     private bool keepGoing = true;
     private bool closeEnough = false;

     private float currentRatio;
     private float growthBound;
     private float shrinkBound;

     public bool squiggle;

	// Use this for initialization
	void Start () {

		    float x = transform.localScale.x;

            currentRatio = x;
         	growthBound = x * 1.5F;
         	shrinkBound = x / 1.5F;

         	if(squiggle){

         	this.routine = StartCoroutine(this.Pulse());

         	}
		
	}
	
	// Update is called once per frame
	void Update () {

		
		
	}


	IEnumerator Pulse(){

		while (keepGoing)
        {

		while (currentRatio != growthBound)
        {
                 // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards( currentRatio, growthBound, approachSpeed);
 
                 // Update our text element
                transform.localScale = Vector3.one * currentRatio;
 
                yield return new WaitForEndOfFrame();
             }
 
             // Shrink for a few seconds
        while (currentRatio != shrinkBound)
        {
                 // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards( currentRatio, shrinkBound, approachSpeed);
 
                 // Update our text element
                transform.localScale = Vector3.one * currentRatio;
 
                yield return new WaitForEndOfFrame();
        }



	}

}
}





             // Get bigger for a few seconds
             