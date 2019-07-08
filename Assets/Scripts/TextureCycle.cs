using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCycle : MonoBehaviour {

	public Texture2D[] textures;
	public float delay = 1.0F;
	public int textureCounter = 0;
	public GameObject tempObject;
	//public LineRenderer lineRenderer;

	private List<GameObject> bubbleList;

	public GameObject selector;

	void Start () {

		InvokeRepeating("CycleTextures", delay, delay);
		
		bubbleList = new List<GameObject>();

		foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("bubble")) {
 
             bubbleList.Add(fooObj);
         }

         //lineRenderer = GetComponent<LineRenderer> ();
	
	}
	void CycleTextures () {

		tempObject = bubbleList[Random.Range(0,bubbleList.Count-1)];

    	textureCounter = ++textureCounter % textures.Length;

    	this.GetComponent<Renderer>().material.mainTexture = textures[Random.Range(0,textures.Length-1)];
	}

	void Update () {


  //    	lineRenderer.SetPosition(0, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z));
		
		// lineRenderer.SetPosition(1, new Vector3(tempObject.transform.position.x,tempObject.transform.position.y,tempObject.transform.position.z));

		selector.transform.position = tempObject.transform.position;

	}
}



