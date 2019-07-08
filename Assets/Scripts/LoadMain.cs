using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMain : MonoBehaviour {

	public string LevelName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(){

		Application.LoadLevel(LevelName);
	}
}
