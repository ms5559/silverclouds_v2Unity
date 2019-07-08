using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRainbowColors : MonoBehaviour {

	private bool ChangeColors;
	public Color[] colors;
	// Use this for initialization
	void Start () {

		ChangeColors = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(ChangeColors){

    		gameObject.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

		}
		
	}

	void OnCollisionEnter(){
		
			ChangeColors = true;

	}
}
