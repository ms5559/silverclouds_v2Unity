using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour {

	public GameObject attractor;
	public float forceFactor = 10.0F;
	public bool free;
	public string attactorTagName;

	// Use this for initialization
	void Start () {

		free = true;
		//attractor = GameObject.FindWithTag("sphereAttactor");
		attractor = GameObject.FindWithTag(attactorTagName);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (free)
		this.GetComponent<Rigidbody>().AddForce((attractor.transform.position - transform.position) * forceFactor * Time.smoothDeltaTime);
		
	}

	void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == attactorTagName){

			free = false;
			//gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameObject.transform.parent = attractor.transform;
		}

	}
}
