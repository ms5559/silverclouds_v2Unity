using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggle : MonoBehaviour {

	public bool mute;
	// Use this for initialization
	void Start () {
		
		mute = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (mute)
		AudioListener.volume = 0;
		if(!mute)
		AudioListener.volume = 1;

		
	}

	public void Mute(){

		mute = !mute;
	}
}
