using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

namespace Rarebyte.REK.Examples {
	
public class PaintManager : MonoBehaviour {

	
	public bool usingEditor;

	private Color paintColor;
	private List<GameObject> cubeList;
	private List<GameObject> centerObjects;
	private List<int> removeList;
	
	private int index = 0;
	private int removeIndex = 0;

	public int colorCounter;
	public int shapeCounter;
	public int tempNumber;
	public int rainbowBulletAmount = 10;

	public Slider sizeSlider;
	public float sizeSliderValue;
	public Text sizeText;
	private Vector3 originalSizeSliderHandleSize;


	private Vector3 previousPosition;
	private Vector3 touchPosition;
	private Vector3 paintPosition;

	public GameObject[] introVis;
	public GameObject moveYourPhoneMessage;
	public GameObject[] rainbowBullet;
	public GameObject fartButton;
	public GameObject symmetryButton;
	public GameObject sizeSliderHandle;
	public GameObject touchdetector;
	public GameObject centerScreenPosition;
	public GameObject[] shapes;
	public GameObject centerObj;
	public GameObject clearButton;

	public bool paintingOn;
	public bool symmetryOn;
	public bool fartStateOn = false;
	public bool firstStart = true;
	public bool makeSquiggle; 	

	public Color[] colors;

	private float map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
    float OldRange = (OldMax - OldMin);
    float NewRange = (NewMax - NewMin);
    float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin; 
    return(NewValue);
	
	}

	public bool audioOn;

	Ray ray;
  	RaycastHit hit;

  	public AudioClip pop1;
  	public AudioClip jmup1;
  	public AudioClip clink1;
  	public AudioClip drop1;
  	public AudioClip buzz1;
  	AudioSource audioSource;


  	public GameObject buttonManager;
 

	void Start(){

//		this.routine = StartCoroutine(this.Pulse());


		//for intro messaging to only happen once
		firstStart = true;
		//not painting yet, so...
		paintingOn = false;
		//make an array list for cubes we're going to create
		cubeList = new List<GameObject>();
		//center objects represent the center position of the symmetry function
		centerObjects = new List<GameObject>();
		//list to track created objects for deleting
		removeList = new List<int>();

		//color at start
		paintColor = colors[10];

		sizeSlider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });

		//originalCrossHairSize = crosshair.transform.localScale;
		originalSizeSliderHandleSize = sizeSliderHandle.transform.localScale;

		symmetryOn = false;

		moveYourPhoneMessage.SetActive(false);

		//centerScreenPosition.SetActive(false);

		audioSource = GetComponent<AudioSource>();

		audioOn = true;

		makeSquiggle = true;

	}

	// Update is called once per frame
	void Update () {


		// in case we want to swipe to change colors
		// if (SwipeManager.Instance.IsSwiping(SwipeDirection.Left)){
		// 	UnityReplayKit.Instance.StartRecording();
		// 	ChangeColor(0);
		// }
		// if (SwipeManager.Instance.IsSwiping(SwipeDirection.Right)){
		// 	ChangeColor(1);
		// 	UnityReplayKit.Instance.StopRecording();
		// }
		// if (SwipeManager.Instance.IsSwiping(SwipeDirection.Up)){
		// 	ChangeColor(2);
		// }
		// if (SwipeManager.Instance.IsSwiping(SwipeDirection.Down)){
		// 	ChangeColor(3);
		// }

		//* for testing
		// if(Input.GetMouseButtonDown(0)){
		// 	paintingOn = true;
		// }	


		if(Input.GetMouseButtonUp (0)){
			
			paintingOn = false;


			moveYourPhoneMessage.SetActive(false);

			//get number of cubes made just on last stroke

			if(tempNumber > 0){
				removeList.Add(tempNumber);
				tempNumber = 0;
				//print("count number: " + (removeList.Count-1) + " recorded: " + removeList[removeList.Count - 1]);
			}

			// print("removeList = " + removeList.Count);
		}

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//indicating if we touched touch detector

		if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
        	
        	Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
			Debug.DrawRay(transform.position, forward, Color.green);

        	if(hit.collider.gameObject.tag == "touchDetector"){


        		paintingOn = true;
//        		print("touchdetector detected");
        		
        		//make move instructions appear
        		if (firstStart){
        		foreach(GameObject intros in introVis)
     			{

         		Destroy(intros);
         		moveYourPhoneMessage.SetActive(true);
     			firstStart = false;
     			}
     			}	


        	}

        	            // fart function
        	if(fartStateOn){

        			Vector3 touchPos = Input.GetTouch (0).position;
      				touchPos.z = 1.2F;
            		Vector3 p = Camera.main.ScreenToWorldPoint (touchPos);
            		Vector3 paintPosition = p;

	     			for(int j = 0; j < rainbowBulletAmount; j++){
    	 			GameObject rainbowBullet1 = Instantiate (rainbowBullet[0], paintPosition, Camera.main.transform.rotation);
     				rainbowBullet1.GetComponent<Renderer>().material.color = colors[Random.Range(0,colors.Length)];
     				rainbowBullet1.GetComponent<Rigidbody>().velocity = rainbowBullet1.transform.forward * Random.Range(15,25);
     				Destroy(rainbowBullet1, 3.0f);


     				if(audioOn){
     				audioSource.PlayOneShot(buzz1, 0.7F);
					audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
     				}
     			}
     		}

    	}


    	sizeSliderValue = sizeSlider.value;


    	if (cubeList.Count == null){

    		clearButton.SetActive(false);
    	}
	}


	void OnEnable(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
		
	}

	void OnDestroy(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;

	}

	private void ARFrameUpdated(UnityARCamera arCamera){



		//Vector3 paintPosition = GetCameraPosition(arCamera) + touchPosition + (Camera.main.transform.forward * 0.7f);
		

		//pass in coordinates relative to ARcamera


    for (int i = 0; i < Input.touchCount; i++)
    {
           

        //if usingEditor, paintPosition is center of the screen, otherwise, use the position of touch
            Vector3 touchPos = Input.GetTouch (i).position;
      		touchPos.z = 1.2F;
            Vector3 p = Camera.main.ScreenToWorldPoint (touchPos);
            paintPosition = p;

            //paintPosition = GetCameraPosition(arCamera) + (Camera.main.transform.forward * 1.2f);
            

		//check distances between previous and current cube positions
		if (Vector3.Distance (paintPosition, previousPosition) > 0.015f) {

			if (paintingOn && !symmetryOn && !fartStateOn){

			previousPosition = paintPosition;

			GameObject cube = Instantiate (shapes[shapeCounter], paintPosition, Random.rotation);
			cubeList.Add(cube);
			cubeList[index].GetComponent<Renderer>().material.color = paintColor;
			cubeList[index].transform.localScale *= sizeSliderValue;
			cubeList[index].GetComponent<SizeUpDown>().squiggle = makeSquiggle;
			index++;
			tempNumber++;

			//put sound on when drawing
			if (audioOn && shapeCounter == 0){
			audioSource.PlayOneShot(pop1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}
			if (audioOn && shapeCounter == 1){
			audioSource.PlayOneShot(clink1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}
			if (audioOn && shapeCounter == 2){
			audioSource.PlayOneShot(jmup1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}
			if (audioOn && shapeCounter == 3){
			audioSource.PlayOneShot(drop1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}

			}


			//paint in SYMMETRY by doubling the cube arraylist and inversing the x axis
			// the trick to this is parenting both gameObjects, cube and cube2 to the centerObject, inversing the x of cube for the cube2 x position, and then unparenting both at the end 

			if (paintingOn && symmetryOn && !fartStateOn){


			previousPosition = paintPosition;

			//for symmetry to work, create central gameobject (referencing the last one in the arraylist)
			GameObject centerObject = centerObjects[centerObjects.Count -1];

			//instantiate the drawing cube like we did before
			GameObject cube = Instantiate (shapes[shapeCounter], paintPosition, Random.rotation);

			//parent that newly created cube to the centerObject
			cube.transform.parent = centerObject.transform;

			//when craeting cube2, parent it to the center object and position its local position x inverse of cube local position x relative to center object
			GameObject cube2 = Instantiate(shapes[shapeCounter]) as GameObject; 
			cube2.name = "cube2"; 
			cube2.transform.rotation = Random.rotation;
			cube2.transform.parent = centerObject.transform;

			//here is the inversion of cube2 of position occurs by inversing the LOCAL x position of cube
			cube2.transform.localPosition = new Vector3(-cube.transform.localPosition.x, cube.transform.localPosition.y, cube.transform.localPosition.z); // Setting the position of the prefab to "arrowPosition"

			//now unparent both cube and cube2 from center object so that when centerobject gets deleted from arraylist, they don't get deleted as children
			cube.transform.parent = null;
			cube2.transform.parent = null;

			//now to add the cube and cube 2 to arrays so they can be deleted later

			//add original cube to array
			cubeList.Add(cube);
			cubeList[index].GetComponent<Renderer>().material.color = paintColor;
			cubeList[index].transform.localScale *= sizeSliderValue;
			cubeList[index].GetComponent<SizeUpDown>().squiggle = makeSquiggle;
			//and then add the second symmetry cube to the same array
			index++;

			cubeList.Add(cube2);
			cubeList[index].GetComponent<Renderer>().material.color = paintColor;
			cubeList[index].transform.localScale *= sizeSliderValue;
			cubeList[index].GetComponent<SizeUpDown>().squiggle = makeSquiggle;
			index++;
			tempNumber+=2;


						//put sound on when drawing
			if (audioOn && shapeCounter == 0){
			audioSource.PlayOneShot(pop1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}
			if (audioOn && shapeCounter == 1){
			audioSource.PlayOneShot(clink1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}
			if (audioOn && shapeCounter == 2){
			audioSource.PlayOneShot(jmup1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
			}
			if (audioOn && shapeCounter == 3){
			audioSource.PlayOneShot(drop1, 0.7F);
			audioSource.pitch = map(-10F, 10F, -1F, 3F, paintPosition.y);
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

	public void Paint(){

		paintingOn = true;
		//print("paint!");
		
	}

	public void StopPaint(){

		paintingOn = false;
		//print("stoppaint!");
	}

	public void ActivateTouchDetector(){

		touchdetector.SetActive(true);
	}

	public void DeactivateTouchDetector(){
		
		touchdetector.SetActive(false);
	}

	//change color of cubes in cubeList array through "paintColor"
	public void ChangeColor(int temp_colorNumber){

			colorCounter = temp_colorNumber;
			paintColor = colors [colorCounter];

	}

	public void ChangeShape(int temp_shapeNumber){

			shapeCounter = temp_shapeNumber;

	}

	public void Undo(){


			//go through and remove shapes from last stroke


			if(removeList.Count - 1 >= 0 && cubeList.Count - 1 >= 0){
				for(int i = 0; i < removeList[removeList.Count - 1]; i++){
					Destroy(cubeList[cubeList.Count - 1]);
					cubeList.Remove(cubeList[cubeList.Count -1]);
				}
					
				//print("count number: " + (removeList.Count-1) + " removed: " + removeList[removeList.Count - 1]);

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

     		//removeList.Clear();
     		//index = cubeList.Count;
	}
	


	public void ValueChangeCheck(){

		float hX = originalSizeSliderHandleSize.x;
		float hY = originalSizeSliderHandleSize.y;
		float hZ = originalSizeSliderHandleSize.z;

		//for changing crosshair (which we currently don't need)
		// float cX = originalCrossHairSize.x;
		// float cY = originalCrossHairSize.y;
		// float cZ = originalCrossHairSize.z;

		float hSizeMultiplier = map(0.0F, 3.0F, 0.4F, 2.0F, sizeSliderValue);
		//float cSizeMultiplier = map(0.0F, 3.0F, cX, 2.0F, sizeSliderValue);

		sizeSliderHandle.transform.localScale = new Vector3(hX * hSizeMultiplier , hY * hSizeMultiplier , hZ * hSizeMultiplier);	
		//crosshair.transform.localScale = new Vector3(cX * cSizeMultiplier, cY * cSizeMultiplier, cZ * cSizeMultiplier);

		sizeText.text = Mathf.Round(sizeSliderValue).ToString() + "x";
		

    }

    public void ToggleSymmetry(){

    	symmetryOn = !symmetryOn;

    	// clear all center objects
    	if (symmetryOn) {
    		
    		// make new center object
    		GameObject centerObject = Instantiate (centerObj, centerScreenPosition.transform.position, Quaternion.identity);
    		centerObject.transform.forward = Camera.main.transform.forward;
    		centerObjects.Add(centerObject);

    		print("center is " + centerObject.transform.position);
    	}

    	if (!symmetryOn) {

    		foreach(GameObject go in centerObjects)
     		{
         		Destroy(go);
         		//centerObjects.Remove(centerObjects[centerObjects.Count -1]);

     		}
    	}
    }


    public void ToggleFart(){

    	fartStateOn = !fartStateOn;

    }

    public void DeactivateFart(){

    	//crosshair.SetActive(false);
    	fartButton.GetComponent<Toggle>().isOn = false;
    }

    public void DeactivateSymmetry(){

    	//crosshair.SetActive(false);
    	symmetryButton.GetComponent<Toggle>().isOn = false;
    }


    public void ToggleAudio(){

    	audioOn = !audioOn;
    }

    public void ToggleSquiggle(){

    	makeSquiggle = !makeSquiggle;
    }
	// shape palette!
	// functions for shape buttons



}}
