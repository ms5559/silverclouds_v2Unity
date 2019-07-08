using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour {


	// Use this for initialization
	void Start () {
			}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){

		gameObject.GetComponent<MeshRenderer>().material.color = new Color32(255,105,180,255);
		
	
	}

	void OnTriggerExit(){

		gameObject.GetComponent<MeshRenderer>().material.color = Color.white;

	}

}
