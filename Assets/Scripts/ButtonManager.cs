using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {


	//public List<GameObject> ColorButtons = new List<GameObject>();
	public GameObject[] ColorButtons;
	public GameObject[] ShapeButtons;
	public Vector3[] ShapeButtonPositions;
	public bool colorOn;
	public bool shapeOn;
	public GameObject colorMenuButton;
	public GameObject undoButton;
	public GameObject crosshair;
	public GameObject recordButton;
	public GameObject activeRecordButton;
	public bool RecordOnState;
	public GameObject sizeSlider;
	public GameObject sizeButton;
	public GameObject symmetryButton;
	public GameObject muteButton;
	public GameObject fartButton;
	public GameObject touchDetector;
	public GameObject clearButton;
	public GameObject squiggleButton;
	public bool size;

	public GameObject calibrationThings;
	public TextMesh[] timerText;
	public float startTime;
	private bool gameStarted;

	public GameObject rainbowFartText;
	public GameObject symmetryText;
	public GameObject sizeText;
	public GameObject shapeText;
	public GameObject colorText;
	public GameObject recordText;
	public GameObject squiggleText;

	//public GameObject Buttons;
	public GameObject chickenObject;
	public AudioClip rainbowFartSound;
	public AudioSource thisAudio;

	

	Ray ray;
  	RaycastHit hit;

	// Use this for initialization
	void Start () {

		DeactivateEveryButton();


		for(int i = 0; i < ShapeButtons.Length; i++){
		 	ShapeButtonPositions[i] = ShapeButtons[i].transform.position;
		}

		recordButton.SetActive(false);

		DeactivateEveryButton();
		recordButton.SetActive(false);

		gameStarted = true;

		touchDetector.SetActive(false);

		thisAudio = GetComponent<AudioSource>();

		
	}
	
	// Update is called once per frame
	void Update () {

		//timer for calibration
		if (gameStarted){
		     
		    startTime -= Time.deltaTime;
//		    timerText.text = Mathf.Round(startTime).ToString();
		    for (int i = 0; i < timerText.Length; i++){
		    	timerText[i].text = Mathf.Round(startTime).ToString();
		    }
     
     		if ( startTime < 0.5F )
     		{
				//print ("done!");
				ActivateEveryButton();
				calibrationThings.SetActive(false);
				sizeSlider.SetActive(false);
				touchDetector.SetActive(true);
				gameStarted = false;
				chickenObject.SetActive(false);
				thisAudio.PlayOneShot(rainbowFartSound, 0.7F);

     		}

		}



    	//BUTTON ACTIVATIONS
		
		if (colorOn) StartCoroutine(ActivateColorButtons());
		if (!colorOn) DeactivateColorButtons();	
		if (shapeOn) StartCoroutine(ActivateShapeButtons());
		if (!shapeOn) DeactivateShapeButtons();


	}

	IEnumerator ActivateColorButtons () {

		for(int i = 0; i < ColorButtons.Length; i++){  
        	ColorButtons[i].SetActive(true); 
        	yield return new WaitForSeconds(0.01f);        
        } 

	}

	void DeactivateColorButtons () {

		for(int i = 0; i < ColorButtons.Length; i++){  
        	ColorButtons[i].SetActive(false); 
        } 

	}

	IEnumerator ActivateShapeButtons() {

		for(int i = 0; i < ShapeButtons.Length; i++){  
        	ShapeButtons[i].SetActive(true); 
			yield return new WaitForSeconds(0.01f);        
		} 
	}

	void DeactivateShapeButtons() {

		if (!RecordOnState){
		
			for(int i = 0; i < ShapeButtons.Length; i++){  
        	ShapeButtons[i].SetActive(false); 
        	ShapeButtons[0].SetActive(true); 

        	}
        }

        else
        {
        	for(int i = 0; i < ShapeButtons.Length; i++){  
        	ShapeButtons[i].SetActive(false); 
        	}
        }
       

	}

	//buttons actually interface with following

	public void ToggleColorButtons (){

		colorOn = !colorOn;
	}

	public void ToggleShapeButtons (GameObject myObject){

		shapeOn = !shapeOn;

		for(int i = 0; i < ShapeButtons.Length; i++){ 

		// whe you select a shape button, that shape button and the one in shape array 0 gets switched
		if (myObject == ShapeButtons[i]){

			Vector3 temp_Pos = myObject.transform.position;
			Vector3 temp_Pos1 = ShapeButtons[0].transform.position;
			myObject.transform.position = temp_Pos1;
			ShapeButtons[0].transform.position = temp_Pos;
			ShapeButtons[i] = ShapeButtons[0];

			GameObject tmp = myObject;
			ShapeButtons[0] = tmp;

		}
		}
	}



	public void DeactivateEveryButton(){

			RecordOnState = true;
			DeactivateColorButtons();
            DeactivateShapeButtons();
			colorMenuButton.SetActive(false);
			undoButton.SetActive(false);
			crosshair.SetActive(false);
			activeRecordButton.SetActive(false);
			recordButton.SetActive(true);
			sizeSlider.SetActive(false);
			sizeButton.SetActive(false);
			symmetryButton.SetActive(false);
			muteButton.SetActive(false);
			fartButton.SetActive(false);
			clearButton.SetActive(false);
			squiggleButton.SetActive(false);
            ShapeButtons[0].SetActive(false);
            ShapeButtons[1].SetActive(false);
            ShapeButtons[2].SetActive(false);
            ShapeButtons[3].SetActive(false);



	}

	public void ActivateEveryButton(){

			RecordOnState = false;
			//menu button for shapes
			ShapeButtons[0].SetActive(true);

			//menu button for color
			colorMenuButton.SetActive(true);

			//undo button
			undoButton.SetActive(true);

			//activeRecordButton
			activeRecordButton.SetActive(true);

			crosshair.SetActive(true);

			//sizeSlider.SetActive(true);
			sizeButton.SetActive(true);
			symmetryButton.SetActive(true);
			muteButton.SetActive(true);
			fartButton.SetActive(true);
			clearButton.SetActive(true);
			squiggleButton.SetActive(true);

	}

	public void DeactivateRecordButton(){

			recordButton.SetActive(false);

	}

	public void ToggleSizeSlider(){

			size = !size;

			if (size) sizeSlider.SetActive(true);				
			if (!size) sizeSlider.SetActive(false);

	}

	public void DeactivateRainbowFartText(){

			rainbowFartText.SetActive(false);

	}

	public void DeactivateSymmetryText(){

			symmetryText.SetActive(false);

	}

	public void DeactivateSizeText(){

			sizeText.SetActive(false);

	}

	public void DeactoviteShapeText(){

			shapeText.SetActive(false);

	}

	public void DeactiveColorText(){

			colorText.SetActive(false);

	}

	public void DeactiveRecordText(){

			recordText.SetActive(false);

	}

	public void DeactivateSquiggleText(){

			squiggleText.SetActive(false);

	}

}
