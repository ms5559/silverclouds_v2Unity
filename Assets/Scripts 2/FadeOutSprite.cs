using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeOutSprite : MonoBehaviour {

       public float minimum = 0.0f;
       public float maximum = 1f;
       public float duration = 5.0f;
       private float startTime;
       public Image sprite;
       //public GameObject recordButton;
       private bool going;

       void Start() {

            going = true;

            startTime = Time.time;

            // recordButton.SetActive(false);

       }
         
       void Update() {

            if (going)
            {
             
            float t = (Time.time - startTime) / duration;
            sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(maximum, minimum, t));        
            //print(t);

            // if (t >= 1){

            //       recordButton.SetActive(true);
            //       going = false;
            // }
            }

	}

      // public void turnOffRecordButton () {

      // recordButton.SetActive(false);	

      // }     

 }
	

