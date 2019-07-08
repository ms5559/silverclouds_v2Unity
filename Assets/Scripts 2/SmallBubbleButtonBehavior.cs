using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallBubbleButtonBehavior : MonoBehaviour {

public float amplitude;
	public float Yspeed;
	public float Xspeed;
	public float originalXspeed;
	public float originalYspeed;
	public float tempY;
  	public float tempX;
  	public float tempZ;
	public Vector3 tempPos;
	public Vector3 originalPos;
	public float startTime;
 	public GameObject previewScreen;
 	public Sprite thisSprite;
 	public Sprite previewSprite;
 	public Vector3 thisButtonPosition;

void Start () {
      
    tempY = transform.position.y;
    tempX = transform.position.x;
    tempPos = this.transform.position;
    amplitude *= Random.Range(-1f,1f);
    Xspeed += Random.Range(1.0f,2.0f);
    Yspeed += Random.Range(-0.3f,0.9f);
    originalXspeed = Xspeed;
    originalYspeed = Yspeed;
    tempPos.z += Random.Range(-0.009f,0.009f);
    thisSprite = GetComponent<Image>().sprite;
    // thisButtonPosition = this.GetComponent<RectTransform>().localPosition;
    // this.GetComponent<RectTransform>().localPosition += new Vector3(0,200,0);

}


void Update () {        
	startTime = Time.time;
	float newTime = Time.time - startTime;
    tempPos.y = tempY + amplitude * Mathf.Sin (Yspeed * startTime);
    tempPos.x += Xspeed;
    this.transform.position = tempPos;    


	}

public void OnBubbleButtonPressed(){

	print("mini bubble pressed!");
	Xspeed = 0f;
	Yspeed = 0f;
	previewScreen.GetComponent<Image>().sprite = thisSprite;
	previewScreen.SetActive(true);

	}

public void OnBubbleButtonRelease(){

	Xspeed = originalXspeed;
	Yspeed = originalYspeed;
	previewScreen.SetActive(false);
	
	}
}
