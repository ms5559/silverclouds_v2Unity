using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSize : MonoBehaviour {

	public Vector3 endSize;
	public Vector3 originalSize;
	public float speedRate;
	public bool growing;

	// Use this for initialization
	void OnEnable () {
		
		growing = true;
		//originalSize = transform.localScale;

		StartCoroutine(LerpScale(speedRate));
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	IEnumerator LerpScale(float speed) {

    	float progress = 0;
     
    	while(progress <= 1){
         transform.localScale = Vector3.Lerp(originalSize, endSize, progress);
         progress += Time.deltaTime * speed;
         yield return null;
    	}

     	transform.localScale = endSize;

	}
}
