using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

namespace Rarebyte.REK.Examples {
	
public class BubbleManager : MonoBehaviour {

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


//***********************************

	public Vector3 originalPlace;
	public Transform placeHolder;
	public GameObject originalPlaceHolderObject;

	public GameObject selector;

	public GameObject lastSelected = null;

  	public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    public bool journeying;
    public bool sceneIsFree;
    public bool journeyingBack;
    public bool freeRotation;

	//public GameObject prefab;

	Ray ray;
  	RaycastHit hit;

  	public bool paintingOn;


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

		if(Input.GetKeyDown("l"))
		{
			LetGo();
		}

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

		if (Input.GetMouseButtonDown(0) && sceneIsFree && lastSelected != null){

			sceneIsFree = false;
			originalPlace = lastSelected.transform.position;
			originalPlaceHolderObject.transform.position = originalPlace;

			startTime = Time.time;
        	journeyLength = Vector3.Distance(originalPlace, placeHolder.position);
 			
 			//selector.SetActive(false);
 			journeying = true;
 			
 			
 		}

 		//if ball is coming towards us
 		if (journeying){
 			float distCovered = (Time.time - startTime) * speed;
        	float fracJourney = distCovered / journeyLength;
        	lastSelected.transform.position = Vector3.Lerp(originalPlace, placeHolder.position, fracJourney);
        	sceneIsFree = false;
 		}

 		//if ball is on its way back to original position
 		if (journeyingBack){
 			lastSelected.transform.parent = null;
 			float distCovered = (Time.time - startTime) * speed;
        	float fracJourney = distCovered / journeyLength;
        	lastSelected.transform.position = Vector3.Lerp(placeHolder.position, originalPlaceHolderObject.transform.position, fracJourney);
 		}


 		if(lastSelected != null)
 		{

 		//if ball is back in its original place
 		if (lastSelected.transform.position == originalPlaceHolderObject.transform.position){	
 			//lastSelected.transform.forward = gameObject.transform.forward;
 			journeyingBack = false;
 			sceneIsFree = true;
	

 		}
 		}

 		//if we are in a sceneIsFree state
 		if (!journeying && !journeyingBack && sceneIsFree && lastSelected != null)
 		{
 			selector.SetActive(true);
 			selector.transform.position = lastSelected.transform.position;

 		}


 		//if ball has arrived in front of the camera and in editing mode
 		if(lastSelected != null){

 		if (lastSelected.transform.position == placeHolder.transform.position){
 			lastSelected.transform.parent = placeHolder;
 			lastSelected.GetComponent<BubbleBehavior>().EngageMedia();
 			lastSelected.GetComponent<BubbleBehavior>().enabled = false;
 			journeying = false;
 			sceneIsFree = false;

 		}
 	}

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//indicating if we touched touch detector

		if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
        	
        	Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;

			Debug.DrawRay(transform.position, forward, Color.green);

			bool cubeHit = false;

			RaycastHit raycastHit = new RaycastHit();

        	// if(hit.collider.gameObject.tag == "touchDetector"){

        	// 	paintingOn = true;

        	// }

        	if(hit.collider.gameObject.tag == "bubble"){

        		//hit.transform.gameObject.GetComponent<BubbleBehavior>().EngageMedia();
        		hit.transform.gameObject.GetComponent<Rotate>().enabled=false;
				//hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
				hit.transform.gameObject.GetComponent<FloatingPhysicsCSharp>().enabled = false;
				hit.transform.gameObject.GetComponent<MagnetScript>().enabled = false;
				hit.transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
				hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        		cubeHit = true;
        		//paintingOn = true;
        	}

        	if (cubeHit && sceneIsFree) 
        	Select (hit.collider.gameObject);

        	Deselect (lastSelected);

    	}

	// 	if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
        	
 //        	Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
	// 		Debug.DrawRay(transform.position, forward, Color.green);
	// 		RaycastHit raycastHit = new RaycastHit();


 //        	// if(hit.collider.gameObject.tag == "touchDetector")
 //        	{

 //        	// 	paintingOn = true;

 //        	// }

 //        	if(hit.collider.gameObject.tag == "bubble")
 //        	{
 //        		hit.transform.gameObject.GetComponent<BubbleBehavior>().EngageMedia();
 //        		print("bubble hit");
 //        	}

 //    	}
	// }

	}

	void Select (GameObject g) {

		if(sceneIsFree)
		{
			selector.transform.position = g.transform.position;
			//Handheld.Vibrate();
			//originalPlace = g.transform.position;
		}
		
		lastSelected = g;

	}

	private void Deselect (GameObject g) {

		if (lastSelected != null) 
		{ // if we have already selected object
			 //g.GetComponent<Renderer>().material.color = Color.white;
			 //g.transform.position = originalPlace;
			 //g.transform.localScale += new Vector3(0.1f,0.1f,0.1f);

			//g.GetComponent<RandomRotation> ().enabled = true; // enable its rotation
			//canvasGroup.alpha = 0f; // make sure to hide canvas; we can hit nothing so we want to disable selection
		}
	}

	public void LetGo ( ){

		if(!sceneIsFree){
			journeyingBack = true;
			selector.transform.position = lastSelected.transform.position;
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

    for (int i = 0; i < Input.touchCount; i++)
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
			//GameObject cube = Instantiate (prefab, paintPosition, Random.rotation);
			//cubeList.Add(cube);
			//cubeList[index].GetComponent<Renderer>().material.color = paintColor;
			//index++;
			tempNumber++;

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
