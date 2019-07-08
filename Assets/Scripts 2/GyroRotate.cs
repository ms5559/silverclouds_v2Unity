using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour {

	public float rotationZ;
	public bool callibrationMode;
	private Transform originalTransform;
	// Use this for initialization
	void Start () {

		originalTransform = gameObject.transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//gameObject.transform.Rotate (Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);

		if (callibrationMode){
			gameObject.transform.Rotate (Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
		}
		else{
		LockedRotation();
		}
	}
	void LockedRotation (){

		rotationZ += Input.gyro.rotationRateUnbiased.z;
        rotationZ = Mathf.Clamp(rotationZ, -45.0f, 45.0f);
 
        transform.localEulerAngles = new Vector3(-rotationZ, -rotationZ, -rotationZ);
	}


}
