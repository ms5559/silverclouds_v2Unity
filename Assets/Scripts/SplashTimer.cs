using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTimer : MonoBehaviour {

	public float startTime;

	public bool attractorMode, isRunning;
 	int currentIndex;

 	public GameObject[] objects;

 	public AudioClip rbf;
  	AudioSource audioSource;

  	private bool audioOn;
	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource>();
		audioOn = true;	
	}
	
	// Update is called once per frame
	void Update () {

		startTime -= Time.deltaTime;
	
	    if ( startTime < 0.5F )
     	{
				//print ("done!");
     		Application.LoadLevel("Drawing");
     	}	

     	if (startTime < 4.0F){

     		if(audioOn){
     		audioSource.PlayOneShot(rbf, 1.0F);
     		audioOn = false;
     		}
     	}

     // 	if (startTime < 3.2F && attractorMode && !isRunning){

     // 	StartCoroutine( Cycle() );
     	
     // }
	}


 IEnumerator Cycle() {
   // these lines execute by the end of the frame in which Cycle() was invoked
   isRunning = true;
   objects[currentIndex].SetActive(true);
   yield return new WaitForSeconds(0.02F);
   // execution will not return to the next statement until 20 seconds later    
   //objects[currentIndex].SetActive(false); 
   // increment the index and ensure it loops around at the end
   currentIndex++;
   if (currentIndex >= objects.Length) currentIndex = 0;
   isRunning = false;
 }
}
