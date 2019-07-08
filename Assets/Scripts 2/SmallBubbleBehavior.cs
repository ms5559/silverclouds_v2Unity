using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBubbleBehavior : MonoBehaviour {

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

void Start () {
      
    tempY = transform.position.y;
    tempX = transform.position.x;
    tempPos = this.transform.position;
    amplitude *= Random.Range(-1f,1f);
    Xspeed += Random.Range(0.0001f,0.00015f);
    originalXspeed = Xspeed;
    originalYspeed = Yspeed;
    Yspeed += Random.Range(-0.3f,0.9f);
    tempPos.z += Random.Range(-0.009f,0.009f);
}
 
void Update () {        
	startTime = Time.time;
	float newTime = Time.time - startTime;
    tempPos.y = tempY + amplitude * Mathf.Sin (Yspeed * startTime);
    tempPos.x += Xspeed;
    this.transform.position = tempPos;    


	}

void OnMouseDown(){

	print("mini bubble pressed!");
	Xspeed = 0f;
	Yspeed = 0f;
	previewScreen.SetActive(true);

	}

void OnMouseUp(){

	Xspeed = originalXspeed;
	Yspeed = originalYspeed;
	previewScreen.SetActive(false);
	
	}
}
