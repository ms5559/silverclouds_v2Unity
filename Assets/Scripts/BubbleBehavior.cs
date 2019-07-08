using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BubbleBehavior : MonoBehaviour {

	private Mesh meshObjectsMesh;
	public GameObject quad;
	public Texture bubbleImage;
	public bool thisIsAVideoSphere;
	// Use this for initialization
	void Start () {

		bubbleImage = this.GetComponent<Renderer>().material.mainTexture;

		if (thisIsAVideoSphere)
		{

		var thisVid = this.GetComponent<VideoPlayer>();
		thisVid.Play();
		thisVid.Pause();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("space"))
		{	
			this.GetComponent<MeshRenderer>().enabled = false;
			GameObject newShape = Instantiate (quad, this.transform.position, Camera.main.transform.rotation);		
			newShape.GetComponent<Renderer>().material.mainTexture = bubbleImage;

			if(thisIsAVideoSphere)
			{
				var clip = gameObject.GetComponent<VideoPlayer>().clip;
				var videoPlayer = newShape.AddComponent<UnityEngine.Video.VideoPlayer>();
				videoPlayer.clip = clip;
				videoPlayer.playOnAwake = true;
				videoPlayer.isLooping = true;
				videoPlayer.Play();
			}
		}
		
	}

	public void EngageMedia(){

		this.GetComponent<Rotate>().enabled=false;
		this.GetComponent<MeshRenderer>().enabled = false;
		this.GetComponent<FloatingPhysicsCSharp>().enabled = false;
		this.GetComponent<MagnetScript>().enabled = false;
		this.GetComponent<BoxCollider>().isTrigger = true;
		this.GetComponent<Rigidbody>().isKinematic = true;

		GameObject newShape = Instantiate (quad, this.transform.position, Camera.main.transform.rotation);
		gameObject.transform.rotation = Camera.main.transform.rotation;	
		newShape.transform.parent = gameObject.transform;

		//newShape.transform.forward = Camera.main.transform.forward;	
		newShape.GetComponent<Renderer>().material.mainTexture = bubbleImage;

			if(thisIsAVideoSphere)
			{
				var clip = gameObject.GetComponent<VideoPlayer>().clip;
				var videoPlayer = newShape.AddComponent<UnityEngine.Video.VideoPlayer>();
				videoPlayer.clip = clip;
				videoPlayer.playOnAwake = true;
				videoPlayer.isLooping = true;
				videoPlayer.Play();
			}
	}
}
