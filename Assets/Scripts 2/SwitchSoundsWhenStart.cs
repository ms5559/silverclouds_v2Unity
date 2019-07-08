using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSoundsWhenStart : MonoBehaviour {

	public AudioSource audio;

	public AudioClip[] clip;

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource>();
		//clip = new AudioClip[3];

		audio.PlayOneShot(clip[Random.Range(0,clip.Length-1)], 0.3F);
	}
	
}
