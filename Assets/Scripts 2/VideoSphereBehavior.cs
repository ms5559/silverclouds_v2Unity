using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSphereBehavior : MonoBehaviour {

	public GameObject videoPlayer;
	Vector3 initialScale;
	bool grow;
	bool on;
	public GameObject MapManager;
	// Use this for initialization
	
	void OnEnable(){

		MapManager = GameObject.FindWithTag("mapManager");

	}	

	void Start () {

		videoPlayer.SetActive(false);

		initialScale = gameObject.transform.localScale;
		
		on = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (grow){

			gameObject.transform.localScale = Vector3.Lerp(initialScale, initialScale * 100, Time.deltaTime * 0.5f);

		}

		if (Input.GetKeyDown("l")){
			
			StartCoroutine(LerpUp());

			MapManager.GetComponent<ActivateVideoPlayer>().DisableAllPlayers();

		}
		
	}

	void OnMouseDown(){

		print("this was touched");
		//gameObject.GetComponent<Renderer>().material.color = Color.red;

		//MapManager.GetComponent<ActivateVideoPlayer>().DisableAllPlayers();

		videoPlayer.SetActive(true);

		StartCoroutine(LerpUp());

	}

	IEnumerator LerpUp(){
     float progress = 0;
     float TimeScale = 0.1f;
     while(progress <= 1){
         transform.localScale = Vector3.Lerp(initialScale, initialScale * 100, progress);
         progress += Time.deltaTime * TimeScale;
         yield return null;
     }
     
 }


}
