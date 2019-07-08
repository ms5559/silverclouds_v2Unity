using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BubbleCreator : MonoBehaviour {

	public GameObject[] bubbles;
	public Sprite[] sprites;
	public GameObject bubbleButtonCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public void CreateBubble(int temp_bubbleNumber){

		GameObject bubble = Instantiate(bubbles[temp_bubbleNumber], transform.position, transform.rotation);
		Vector3 bubblePosition = bubble.transform.position;
		bubblePosition.y += Random.Range(0.01f,0.04f);
		bubble.transform.position = bubblePosition;
		bubble.GetComponent<Image>().sprite = sprites[Random.Range(0,sprites.Length-1)];
		//bubble.transform.SetParent(bubbleButtonCanvas.transform, false);
		bubble.transform.position = bubbleButtonCanvas.transform.position + new Vector3(-600,Random.Range(-800,0), 0);
		bubble.transform.rotation = bubbleButtonCanvas.transform.rotation;
		bubble.transform.parent = bubbleButtonCanvas.transform;
		bubble.transform.SetParent(bubbleButtonCanvas.transform, false);

		Destroy(bubble,12);

	}
}
