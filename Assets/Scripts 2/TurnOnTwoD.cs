using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTwoD : MonoBehaviour {

	Ray ray;
  	RaycastHit hit;

  	public GameObject bubbleCreator;
  	public GameObject triggerInTheSky;
  	public GameObject canvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		ray = Camera.main.ScreenPointToRay(transform.position);

		//indicating if we touched touch detector

		if(Physics.Raycast(ray, out hit))
		{
        	
        	Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        	Debug.DrawRay(transform.position, forward, Color.green);

        	if(hit.collider.gameObject.tag == "floor")
        	{
        		bubbleCreator.SetActive(true);
        		triggerInTheSky.SetActive(true);	
        		canvas.SetActive(true);

     		}
		
		}

		else
     	{	
     		bubbleCreator.SetActive(false);
        	triggerInTheSky.SetActive(false);
        	canvas.SetActive(false);

        }	
}
}
