using UnityEngine;
using System.Collections;

public class DrawingSymmetry : MonoBehaviour {

	public GameObject prefab1;
	public GameObject centerObject;
	
	void Start(){
	}

	void Update(){

		if(Input.GetMouseButtonDown(0)){

		Vector3 p=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.2f));
				
		GameObject cube = Instantiate (prefab1, p, Random.rotation);

		cube.transform.parent = centerObject.transform;

		Vector3 newCenterPosition = centerObject.transform.position;
		Vector3 difference = cube.transform.localPosition - newCenterPosition;

		GameObject cube2 = Instantiate(prefab1) as GameObject; // creating a local game object to store the    instantiated object and then casting it to a Gamebject
  		cube2.name = "cube2"; // Setting prefab name in hierarchy
		cube2.transform.parent = centerObject.transform;
  		cube2.transform.localPosition = new Vector3(centerObject.transform.position.x - difference.x, cube.transform.localPosition.y, centerObject.transform.position.z + difference.z); // Setting the position of the prefab to "arrowPosition"

		cube.transform.parent = null;
		cube2.transform.parent = null;
		//GameObject cube2 = Instantiate (prefab1, cube2position, Random.rotation);

		}


													if (Input.GetKeyDown("w"))
													{
    												 transform.position += 
    												 Vector3.forward * Time.deltaTime * 5;

													}	
													if (Input.GetKeyDown("c"))
													{
    												 transform.position -=
    												 Vector3.forward * Time.deltaTime * 5;
													}
	}



}



