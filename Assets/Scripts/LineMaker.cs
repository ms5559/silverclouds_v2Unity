using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour {

	public float delay = 1.0F;
	public GameObject tempObject;
	public LineRenderer lineRenderer;

	private List<GameObject> bubbleList;

	void Start () {

		delay = Random.Range(0,20);

		InvokeRepeating("CycleTextures", delay, delay);
		
		bubbleList = new List<GameObject>();

		foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("bubble")) {
 
             bubbleList.Add(fooObj);
         }

        lineRenderer = GetComponent<LineRenderer> ();

        tempObject = bubbleList[Random.Range(0,bubbleList.Count-1)];

        lineRenderer.SetPosition(0, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z));
		
		lineRenderer.SetPosition(1, new Vector3(tempObject.transform.position.x,tempObject.transform.position.y,tempObject.transform.position.z));
	
	}
	void CycleTextures () {

		tempObject = bubbleList[Random.Range(0,bubbleList.Count-1)];

	}

	void Update () {


     	lineRenderer.SetPosition(0, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z));
		
		lineRenderer.SetPosition(1, new Vector3(tempObject.transform.position.x,tempObject.transform.position.y,tempObject.transform.position.z));

	}
}

