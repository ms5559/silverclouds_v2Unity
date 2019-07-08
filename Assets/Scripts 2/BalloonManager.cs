using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

namespace Rarebyte.REK.Examples {
	
public class BalloonManager : MonoBehaviour {

	private Color paintColor;
	private List<GameObject> cubeList;
	private List<int> removeList;
	public Color[] colors;
	
	private int index = 0;
	private int removeIndex = 0;

	public int colorCounter;
	public int tempNumber;

	private Vector3 previousPosition;
	private Vector3 touchPosition;
	private Vector3 paintPosition;

	public GameObject touchdetector;

	public GameObject prefab;

	Ray ray;
  	RaycastHit hit;

  	public bool paintingOn;

  	public GameObject instructions;


	void Start(){

		//not painting yet, so...
		paintingOn = false;
		//make an array list for cubes we're going to create
		cubeList = new List<GameObject>();
		//center objects represent the center position of the symmetry function
	
		removeList = new List<int>();



	}

	// Update is called once per frame
	void Update () {

		if (cubeList.Count > 15){

			Destroy(cubeList[0]);
			cubeList.RemoveAt(0);
		}



		if(Input.GetMouseButtonUp (0)){
			
			paintingOn = false;


			//get number of cubes made just on last stroke
			if(tempNumber > 0){
				removeList.Add(tempNumber);
				tempNumber = 0;
				//print("count number: " + (removeList.Count-1) + " recorded: " + removeList[removeList.Count - 1]);
			}

		}

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//indicating if we touched touch detector

		if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
        	
        	Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
			Debug.DrawRay(transform.position, forward, Color.green);

        	if(hit.collider.gameObject.tag == "touchDetector"){

        		paintingOn = true;
        		instructions.SetActive(false);

        	}

    	}
	}


	void OnEnable(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
		
	}

	void OnDestroy(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;

	}

	private void ARFrameUpdated(UnityARCamera arCamera){


	if (paintingOn)
	{

    // for (int i = 0; i < Input.touchCount; i++)
    // {

		for (int i = 0; i < 1; i++)
    	{
           

        //if usingEditor, paintPosition is center of the screen, otherwise, use the position of touch
            Vector3 touchPos = Input.GetTouch (i).position;
      		touchPos.z = 1.2f;
            Vector3 p = Camera.main.ScreenToWorldPoint (touchPos);
            paintPosition = p;
         
         	//paintPosition = GetCameraPosition(arCamera) + (Camera.main.transform.forward * 1.2f);
      

		// //check distances between previous and current cube positions
		 if (Vector3.Distance (paintPosition, previousPosition) > 0.015f) {

			if (paintingOn){

			previousPosition = paintPosition;
			GameObject cube = Instantiate (prefab, paintPosition, Random.rotation);
			cubeList.Add(cube);
			//cubeList[index].GetComponent<Renderer>().material.color = paintColor;
			//index++;
			tempNumber++;

			paintingOn = false;

			}


		}

	}

	}

}	


	private Vector3 GetCameraPosition(UnityARCamera cam) {

		Matrix4x4 matrix = new Matrix4x4 ();
		matrix.SetColumn (3, cam.worldTransform.column3);
		return UnityARMatrixOps.GetPosition (matrix);
	}



	public void ActivateTouchDetector(){

		touchdetector.SetActive(true);
	}

	public void DeactivateTouchDetector(){
		
		touchdetector.SetActive(false);
	}


	public void Undo(){


			//go through and remove shapes from last stroke

			if(removeList.Count - 1 >= 0 && cubeList.Count - 1 >= 0){
				for(int i = 0; i < removeList[removeList.Count - 1]; i++){
					Destroy(cubeList[cubeList.Count - 1]);
					cubeList.Remove(cubeList[cubeList.Count -1]);
				}
					
				removeList.Remove(removeList[removeList.Count -1]);
			}

			//print("cubelist: " + cubeList.Count);
			index = cubeList.Count;

	}

	public void Clear() {

		   	foreach(GameObject cubes in cubeList)
     		{
         		Destroy(cubes);
         		//cubeList.Clear();
         		
     		}

	}

	}

}
