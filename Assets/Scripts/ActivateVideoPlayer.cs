using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVideoPlayer : MonoBehaviour {

	public List<GameObject> VideoPlayers = new List<GameObject>();
	public List<GameObject> VideoSpheres = new List<GameObject>();

	public bool on = false;

	private int currIndex;

	Vector3 originalScale;

	Ray ray;
  RaycastHit hit;

  
  public void FindSpheresAndPlayers(){

    	on = !on;

    	if (on){

        for(int i = 0; i < VideoSpheres.Count; i++){  
        	VideoSpheres[i].SetActive(true); 
        }

		}


        if (!on){


        for(int i = 0; i < VideoSpheres.Count; i++){  
        	VideoSpheres[i].transform.localScale = originalScale;
        	VideoSpheres[i].SetActive(false); 
        } 

        }
    }

    void Awake(){

    	on = false;

    	//Find both Sphere and VideoPlayer GameObjects
    	foreach(GameObject spheres in GameObject.FindGameObjectsWithTag("sphere")){
			VideoSpheres.Add(spheres);
		}

		foreach(GameObject players in GameObject.FindGameObjectsWithTag("player")){
			VideoPlayers.Add(players);

		}


		//Set all videoplayers to inactive so we don't lose our minds
		for(int i = 0; i < VideoPlayers.Count; i++){  
        	VideoPlayers[i].SetActive(false); 
        } 

       	for(int i = 0; i < VideoSpheres.Count; i++){  
        	VideoSpheres[i].SetActive(false); 
        	originalScale = VideoSpheres[1].transform.localScale;

        } 


    }


	void Update(){

		//If a video sphere is touched, find the corresponding video player and activate it

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
        
        	for(int i = 0; i < VideoSpheres.Count; i++){

                if(hit.collider.gameObject == VideoSpheres[i].gameObject){
                	print("Your cube's index in the array is: " + i.ToString());
                	currIndex = i;
                	DisableAllPlayers();
        			//VideoSpheres[i].SetActive(true);
        			VideoSpheres[i].SetActive(false);

        			VideoSpheres[i].gameObject.SetActive(true);

              }
          }
      }

        if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
        
        	for(int i = 0; i < VideoPlayers.Count; i++){

           //      if(hit.collider.gameObject == VideoSpheres[i].gameObject){
           //      	print("Your cube's index in the array is: " + i.ToString());
           //      	currIndex = i;
           //      	DisableAllPlayers();
        			// VideoPlayers[i].SetActive(true);
           //          }

           //      if(hit.collider.gameObject == VideoPlayers[i].gameObject){
           //      	print("Your cube's index in the array is: " + i.ToString());
           //      	DisableAllPlayers();
           //          }
			}
        }
    }


    public void DisableAllPlayers(){

   		for(int i = 0; i < VideoPlayers.Count; i++){  
        	VideoPlayers[i].SetActive(false);

        }   
    }

}
