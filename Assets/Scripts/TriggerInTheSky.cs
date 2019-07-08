using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInTheSky : MonoBehaviour {

	public BubbleCreator bubbleCreator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("p"))
		{
			SignalBubbleCreator(0);
		}
		if(Input.GetKeyDown("o"))
		{
			SignalBubbleCreator(1);
		}
		if(Input.GetKeyDown("i"))
		{
			SignalBubbleCreator(2);
		}
		if(Input.GetKeyDown("u"))
		{
			SignalBubbleCreator(3);
		}
		
	}

	void OnTriggerEnter(Collider other){

		// if(other.transform.tag == "image")
		// {
		// 	SignalBubbleCreator(0);
		// }
		// if(other.transform.tag == "video")
		// {
		// 	SignalBubbleCreator(1);
		// }
		// if(other.transform.tag == "drawing")
		// {
		// 	SignalBubbleCreator(2);
		// }
		// if(other.transform.tag == "words")
		// {
		// 	SignalBubbleCreator(3);
		// }

		SignalBubbleCreator(Random.Range(0,4));

	}


	public void SignalBubbleCreator(int temp_Number){

		bubbleCreator.CreateBubble(temp_Number);

	}
}
