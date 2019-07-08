using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

	
public class ShapesShooter : MonoBehaviour {

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

	//public GameObject prefab;

	public Rigidbody projectile;
    public Transform shotPos;
    public float shotForce = 1000f;

	Ray ray;
  	RaycastHit hit;

  	public bool paintingOn;
  	private Vector3 originalPlace;

  	public GameObject selector;

  	//public Text objectText;
  	private GameObject lastSelected = null;

  	Camera cam;

  	public Transform placeHolder;

  	public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    public bool journeying;
    public bool sceneIsFree;
    public bool journeyingBack;
    public bool freeRotation;

    public AudioClip blop;
    public AudioClip aahh;
    public AudioClip boomp;
    AudioSource audioSource;

    public GameObject letGoButton;
    public GameObject colorButtons;
    public GameObject originalPlaceHolderObject;

	void Start(){

		letGoButton.SetActive(false);
 		colorButtons.SetActive(false);
 		freeRotation = true;

		audioSource = GetComponent<AudioSource>();

		//not painting yet, so...
		paintingOn = false;
		//make an array list for cubes we're going to create
		cubeList = new List<GameObject>();
		//center objects represent the center position of the symmetry function
	
		removeList = new List<int>();

		paintColor = colors[0];

		cam = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("l"))
		LetGo();


		if(Input.GetMouseButtonUp (0)){
			
			paintingOn = false;

			paintColor = colors[colorCounter];

			if(colorCounter < colors.Length - 1){
				colorCounter ++;
			}
			else colorCounter = 0;

			//get number of cubes made just on last stroke
			if(tempNumber > 0){
				removeList.Add(tempNumber);
				tempNumber = 0;
				//print("count number: " + (removeList.Count-1) + " recorded: " + removeList[removeList.Count - 1]);
			}
		}

		if (Input.GetMouseButtonDown(0) && sceneIsFree && lastSelected != null){

			sceneIsFree = false;

			audioSource.PlayOneShot(aahh, 0.7F);

			originalPlace = lastSelected.transform.position;
			originalPlaceHolderObject.transform.position = originalPlace;

			startTime = Time.time;
        	journeyLength = Vector3.Distance(originalPlace, placeHolder.position);
 			
 			//lastSelected.transform.position = placeHolder.position;
 			
 			lastSelected.GetComponent<MeshCollider>().enabled = true;
 			lastSelected.GetComponent<BoxCollider>().enabled = false;
 			selector.SetActive(false);
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
 			letGoButton.SetActive(false);
 			colorButtons.SetActive(false);
 			freeRotation = true; 			

 		}
 		}


 		//if we are in a sceneIsFree state
 		if (!journeying && !journeyingBack && sceneIsFree && lastSelected != null)
 		{
 			selector.SetActive(true);
 			selector.transform.position = lastSelected.transform.position;
 			letGoButton.SetActive(false);
 			colorButtons.SetActive(false);

 		}


 		//if ball has arrived in front of the camera and in editing mode
 		if(lastSelected != null){

 		if (lastSelected.transform.position == placeHolder.transform.position){
 			lastSelected.transform.parent = placeHolder;
 			if(freeRotation = true)
 			{
 			//lastSelected.transform.forward = gameObject.transform.forward;
			freeRotation = false; 			
			}
 			journeying = false;
 			sceneIsFree = false;
 			letGoButton.SetActive(true);
 			colorButtons.SetActive(true);
 		}
 	}


		//ray = Camera.main.ScreenPointToRay(Camera.main.transform);
		ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

		//indicating if we touched touch detector

		//if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
		if(Physics.Raycast(ray, out hit)){
        	
        	Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;

			Debug.DrawRay(transform.position, forward, Color.green);

			bool cubeHit = false;

			RaycastHit raycastHit = new RaycastHit();

        	// if(hit.collider.gameObject.tag == "touchDetector"){

        	// 	paintingOn = true;

        	// }

        	if(hit.collider.gameObject.tag == "gridCube"){

        		cubeHit = true;
        		//paintingOn = true;

        	}

        	Deselect (lastSelected);

        	 if (cubeHit && sceneIsFree) 
        	 Select (hit.collider.gameObject);

    	}
	}

	public void LetGo ( ){

		audioSource.PlayOneShot(boomp, 0.7F);


		if(!sceneIsFree){
			journeyingBack = true;
			selector.transform.position = lastSelected.transform.position;
		}
	}

	void Select (GameObject g) {

		if(sceneIsFree){
		audioSource.PlayOneShot(blop, 0.7F);
		selector.transform.position = g.transform.position;
		Handheld.Vibrate();
		//originalPlace = g.transform.position;
		}

		// when we select cube, we disable rotation script to make it stationary
		lastSelected = g;
		//g.GetComponent<RandomRotation> ().enabled = false;
 		//g.GetComponent<Renderer>().material.color = Color.red;

 		


 		//g.transform.localScale += new Vector3(1,1,1);

		// display object's name in Text component, show canvas and move it above selected cube
		//objectText.text = g.name;
		//canvasGroup.alpha = 1f;
		// Vector3 newPos = g.transform.position;
		// newPos.z = -1f;
		// g.transform.position = newPos;
	}
 
	private void Deselect (GameObject g) {
		if (lastSelected != null) { // if we have already selected object
			 //g.GetComponent<Renderer>().material.color = Color.white;
			 //g.transform.position = originalPlace;
			 //g.transform.localScale += new Vector3(0.1f,0.1f,0.1f);

			//g.GetComponent<RandomRotation> ().enabled = true; // enable its rotation
			//canvasGroup.alpha = 0f; // make sure to hide canvas; we can hit nothing so we want to disable selection
		}
	}


	void OnEnable(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
		
	}

	void OnDestroy(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;

	}

	private void ARFrameUpdated(UnityARCamera arCamera){



    // for (int i = 0; i < Input.touchCount; i++)
    // {
           

    //     //if usingEditor, paintPosition is center of the screen, otherwise, use the position of touch
    //         Vector3 touchPos = Input.GetTouch (i).position;
    //   		touchPos.z = 1.2F;
    //         Vector3 p = Camera.main.ScreenToWorldPoint (touchPos);
    //         paintPosition = p;

            
         
        paintPosition = GetCameraPosition(arCamera) + (Camera.main.transform.forward * 1.2f);
      

		// //check distances between previous and current cube positions
		 if (Vector3.Distance (paintPosition, previousPosition) > 0.0015f) {

			if (paintingOn){

			previousPosition = paintPosition;
			//GameObject cube = Instantiate (prefab, paintPosition, Random.rotation);
			//cubeList.Add(cube);
			//cubeList[index].GetComponent<Renderer>().material.color = paintColor;
			//
			tempNumber++;
			Rigidbody shot = Instantiate(projectile, paintPosition, Random.rotation) as Rigidbody;
            shot.AddForce(shotPos.forward * shotForce);
            GameObject cube = shot.gameObject;
            cubeList.Add(cube);
            cubeList[index].GetComponent<Renderer>().material.color = paintColor;
            index++;
			}
		}

	//}


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

