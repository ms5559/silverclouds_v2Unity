using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTimer : MonoBehaviour {

	public float startTime;
	public bool start = false;
	private float originalTime;
	// Use this for initialization
	void Awake () {

		originalTime = startTime;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (true){

			startTime -= Time.deltaTime;

			if(startTime < 0.5){
				gameObject.SetActive(false);
			}
		}
		
	}

	void OnEnable(){
		start = true;
		startTime = originalTime;
	}
}
