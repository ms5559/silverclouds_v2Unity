using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rarebyte.REK.Examples {	

	public enum GameState {
      MainMenu,
      Run,
      GameFinished,
    }

public class RecordManager : MonoBehaviour {

	private GameState CurrentGameState { get; set; }

	public Button recordButton;
	public GameObject buttonManager;
	public bool recording;
	public float startTime;
	public bool StartTheRecording;
	public bool timeEnded;
	public TextMesh timeText;
	public GameObject recordTextHolder;
	Ray ray;
  	RaycastHit hit;
  	public GameObject touchDetector;
  	public float originalStartTime;
  	public GameObject logoOverlay;

	// Use this for initialization
	void Start () {
		// UnityReplayKit.Instance.Started += () => Debug.Log("Recording started");
		UnityReplayKit.Instance.Started += () => Debug.Log("HOLY SHIT YEOOO");
		recordTextHolder.SetActive(false);

		originalStartTime = startTime;
	
		logoOverlay.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (StartTheRecording){

		//START TIMER	
			recordTextHolder.SetActive(true);
			timeText.text = Mathf.Round(startTime).ToString();
			startTime -= Time.deltaTime;
			touchDetector.SetActive(false);
		
		//START RECORDING

			if (startTime < 0.5){
				buttonManager.GetComponent<ButtonManager>().DeactivateRecordButton();
				buttonManager.GetComponent<ButtonManager>().DeactivateEveryButton();
				recordButton.image.enabled = false;
				recordTextHolder.SetActive(false);
				CurrentGameState = GameState.Run;
				UnityReplayKit.Instance.StartRecording();
				touchDetector.SetActive(true);
				StartTheRecording = false;
				timeEnded = true;
        		startTime = originalStartTime;
        		logoOverlay.SetActive(true);
			}
		}


		//STOP RECORDING

		if(!recording && timeEnded){
      

        		print("gotcha");
        		buttonManager.GetComponent<ButtonManager>().ActivateEveryButton();
				// UnityReplayKit.Instance.StopRecording();
				// UnityReplayKit.Instance.ShowPreview();
				UnityReplayKit.Instance.StopRecordingAndShowPreview();
				CurrentGameState = GameState.GameFinished;
				recordButton.image.enabled = true;
				StartTheRecording = false;
				timeEnded = false;
				logoOverlay.SetActive(false);


    	}

		
	}	


	public void ToggleRecording(){

		recording = !recording;

		if (recording){
			StartTheRecording = true;
			//recordButton.image.enabled = false;

		}

	}

}


}
